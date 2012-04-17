using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SampleProject.Models;
using SampleProject.Models.BlueprintSearch;
using SampleProject.Models.Repositories;
using SampleProject.ViewModels;
using SampleProject.ViewModels.Browser;

namespace SampleProject.Controllers
{
    /// <summary>
    /// Provides blueprints search.
    /// </summary>
    public class BrowserController : Controller
    {

        #region Private fields
        private readonly IBlueprintRepository _blueprints;
        private readonly IBlueprintSearchService _search;
        #endregion

        public BrowserController(IBlueprintRepository blueprints,IBlueprintSearchService search)
        {
            _blueprints = blueprints;
            _search = search;
        }

        //
        // GET: /BlueprintsBrowser/
        
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /BlueprintsBrowser/Find
        [HttpPost]
        public ActionResult Find(FindViewModel model)
        {
            var searchResult = _search.SearchBlueprintsParams(model.BlueprintName, model.BlueprintDescription, model.BlueprintAuthor);
            return View("SearchResult", searchResult);
        }

    }
}
