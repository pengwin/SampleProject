using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SampleProject.Authentication;
using SampleProject.Common;
using SampleProject.Models.BlueprintModels;
using SampleProject.Models.Repositories;

using SampleProject.ViewModels.BlueprintAjax;

namespace SampleProject.Controllers
{
    public class BlueprintAjaxController : Controller
    {
        #region Private fileds

        private readonly IApiKeyStore _store;
        private readonly IBlueprintRepository _blueprints;
        private readonly ILogger _logger;

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="store"></param>
        /// <param name="logger"> </param>
        /// <param name="blueprints"> </param>
        public BlueprintAjaxController(IApiKeyStore store, ILogger logger, IBlueprintRepository blueprints)
        {
            _store = store;
            _blueprints = blueprints;
            _logger = logger;
        }

        #region Private shortcuts

        /// <summary>
        /// Gets api key from http header.
        /// </summary>
        /// <returns></returns>
        private string GetApiKey()
        {
            return Request.Headers["apiKey"];
        }

        /// <summary>
        /// Validates the user's api key.
        /// </summary>
        /// <returns></returns>
        private bool IsApiKeyValid()
        {
            var apiKey = GetApiKey();
            return _store.IsValidApiKey(apiKey);
        }

        private string FetchModelError()
        {
            string errorMessage = "";
            foreach (var key in ModelState.Keys)
            {
                var error = ModelState[key].Errors.FirstOrDefault();
                if (error != null)
                {
                    errorMessage += " " + error.ErrorMessage + " ";
                }
            }
            return errorMessage;
        }

        private bool IsModelValid()
        {
            //check if model is valid
            if (!ModelState.IsValid)
            {
                var errors = FetchModelError();
                _logger.Error("Blueprint create model errors: " + errors);
                return false;
            }
            return true;
        }

        #endregion

        #region Http coded responses

        /// <summary>
        /// Http: OK
        /// </summary>
        /// <returns></returns>
        public ActionResult Http200()
        {
            Response.StatusCode = 200;
            return Content("OK.", "text/plain");
        }

        /// <summary>
        /// Http: Forbidden
        /// </summary>
        /// <returns></returns>
        public ActionResult Http403()
        {
            Response.StatusCode = 403;
            return Content("Forbidden.", "text/plain");
        }

        /// <summary>
        /// Http: Bad request
        /// </summary>
        /// <returns></returns>
        public ActionResult Http400()
        {
            Response.StatusCode = 400;
            return Content("Bad request", "text/plain");
        }

        /// <summary>
        /// Http: Not found
        /// </summary>
        /// <returns></returns>
        public ActionResult Http404()
        {
            Response.StatusCode = 404;
            return Content("Not found", "text/plain");
        }

        #endregion


        #region Ajax api

        //
        // GET: /ajax/blueprint/
        [HttpGet]
        public ActionResult Index()
        {
            return new EmptyResult();
        }


        /// <summary>
        /// Creates blueprint.
        /// POST: /ajax/blueprint/
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(BlueprintJsonModel model)
        {
            //check if model is valid
            if (!IsModelValid()) return Http400();

            var apiKey = GetApiKey();
            var user = _store.GetApiKeyOwner(apiKey);
            if (user == null)
            {
                _logger.Error("There is no owner of the key: " + apiKey);
                return Http400();
            }
            // map data from the view model to the db model
            var blueprint = new Blueprint { Name = model.Name, Description = model.Description, JsonData = model.JsonData, Changed = DateTime.Now };

            // put the blueprint to the db
            _blueprints.CreateBlueprintForUser(user.UserId, blueprint);
            _blueprints.SaveChanges();

            // add id of the blueprint
            model.id = blueprint.BlueprintId;
            // return with id the same model
            return Json(model);
        }

        /// <summary>
        /// Gets blueprint.
        /// GET: /ajax/blueprint/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Read(int id)
        {
            // check api key
            //if (!IsApiKeyValid()) return Http403();

            var blueprint = _blueprints.GetBlueprintById(id);
            if (blueprint == null) return Http404();

            var viewModel = new BlueprintJsonModel
            {
                id = id,
                Name = blueprint.Name,
                Description = blueprint.Description,
                JsonData = blueprint.JsonData
            };

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        //
        // PUT: /ajax/blueprint/{id}
        [HttpPut]
        public ActionResult Update(int id, BlueprintJsonModel model)
        {
            var user = _store.GetApiKeyOwner(GetApiKey());

            if (user == null) return Http403();

            var blueprint = _blueprints.GetBlueprintById(id);
            if (blueprint == null) return Http404();

            // updating model isn't allowed for this api key
            if (user.UserId != blueprint.UserId) return Http403();

            blueprint.Name = model.Name;
            blueprint.Description = model.Description;
            blueprint.JsonData = model.JsonData;
            blueprint.Changed = DateTime.Now;

            _blueprints.SaveChanges();

            return Http200();
        }

        //
        // DELETE: /ajax/blueprint/{id}
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            return new EmptyResult();
        }

        #endregion
    }
}
