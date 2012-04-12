using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SampleProject.Models;
using SampleProject.ViewModels.Blueprints;

namespace SampleProject.Controllers
{

    

    public class BlueprintsController : Controller
    {

        #region Private fields
        private readonly IBlueprintRepository _blueprints;
        #endregion

        public BlueprintsController(IBlueprintRepository blueprints)
        {
            _blueprints = blueprints;
        }

        //
        // GET: /Blueprint/
        
        public ActionResult Index()
        {
            return View();
        }


        

        //
        // GET: /Blueprint/
        [HttpPost]
        public ActionResult Find(FindViewModel model)
        {
            BlueprintSearchService service = new BlueprintSearchService(new UserContext());
            var search = service.SearchBlueprintsParams(model.BlueprintName, model.BlueprintDescription, model.BlueprintAuthor);

            return View("SearchResult", search);
        }

    }
}
