using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SampleProject.Areas.Admin.ViewModels.AdminSetup;
using SampleProject.Authentication;
using SampleProject.Models;
using SampleProject.Models.UserModels;

namespace SampleProject.Areas.Admin.Controllers
{
    public class AdminSetupController : Controller
    {
        #region Private fileds

        private readonly IRoleRepository _roles;
        private readonly IUserRepository _users;

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="roles"></param>
        /// <param name="users"></param>
        public AdminSetupController(IRoleRepository roles, IUserRepository users)
        {
            _roles = roles;
            _users = users;
        }

        private bool AdminExists()
        {
            var adminRole = _roles.GetRoleByName("Admin");
            if (adminRole == null) return false;
            return adminRole.Users.Any();
        }


        /// <summary>
        /// GET: /Admin/AdminSetup/
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            if (AdminExists())
            {
                return RedirectToAction("Index", "Home");
            }

            var users = _users.GetAllUsers();

            var viewModels = new List<IndexViewModel>();

            foreach (var user in users)
            {
                var viewModel = new IndexViewModel
                                    {
                                        UserId = user.UserId,Username = user.Username,
                                        OpenId = user.OpenIds.First().OpenIdUrl
                                    };
                viewModels.Add(viewModel);
            }

            return View(viewModels);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Promote(int userId)
        {
            if (AdminExists())
            {
                return RedirectToAction("Index", "Home");
            }

            _roles.AddRoleToUser("Admin",userId);
            _roles.SaveChanges();

            var user = _users.GetUserById(userId);

            return View(user);
        }

    }
}
