using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SampleProject.Authentication;
using SampleProject.Common;
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

        public BlueprintController(IBlueprintRepository blueprints,IUserAuthService authService,ILogger logger) : base(authService,logger)
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
            if (ModelState.IsValid)
            {
                var blueprint = new Blueprint
                                    {Name = model.Name, Description = model.Description, Changed = DateTime.Now};
                _blueprints.CreateBlueprintForUser(UserInfo.UserId, blueprint);
                _blueprints.SaveChanges();
                return Redirect(String.Format("{0}/#{1}",Url.Action("Index", "Editor"),blueprint.BlueprintId));
            }
            return View(model);
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
