using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SampleProject.Authentication;
using SampleProject.Models.BlueprintModels;
using SampleProject.Models.Repositories;
using SampleProject.ViewModels.Blueprint;

namespace SampleProject.Controllers
{
    /// <summary>
    /// Provides management of the current user blueprints.
    /// </summary>
    public class BlueprintController : BaseController
    {
        private readonly IBlueprintRepository _blueprints;

        public BlueprintController(IBlueprintRepository blueprints,IUserAuthService authService) : base(authService)
        {
            _blueprints = blueprints;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateViewModel model)
        {
            var blueprint = new Blueprint {Name = model.Name, Description = model.Description, Changed = DateTime.Now};
            _blueprints.CreateBlueprintForUser(UserInfo.UserId,blueprint);
            _blueprints.SaveChanges();
            return RedirectToAction("Index", "User");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var blueprint = _blueprints.GetBlueprintById(id);
            _blueprints.RemoveBlueprint(blueprint);
            _blueprints.SaveChanges();
            return View();
        }

    }
}
