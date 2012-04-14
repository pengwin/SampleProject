using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SampleProject.Models.Repositories;

namespace SampleProject.Areas.Admin.Controllers
{
    public class HomeController : Controller
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
        public HomeController(IRoleRepository roles, IUserRepository users)
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
        //
        // GET: /Admin/Home/

        [Authorize]
        public ActionResult Index()
        {
            if (!AdminExists())
            {
                return RedirectToAction("Index", "AdminSetup");
            }
            return View();
        }

    }
}
