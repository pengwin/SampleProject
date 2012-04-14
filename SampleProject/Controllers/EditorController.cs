using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SampleProject.Authentication;
using SampleProject.ViewModels.Editor;

namespace SampleProject.Controllers
{
    public class JsonTest
    {
        public string Test { get; set; }
        public List<int> TempList { get; set; }
        public string Message { get; set; }
    }

    public class EditorController : BaseController
    {

        #region Private fileds

        private readonly IApiKeyStore _store;

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="authService"></param>
        /// <param name="store"></param>
        public EditorController(IUserAuthService authService,IApiKeyStore store):base(authService)
        {
            _store = store;
        }

        private bool IsApiKeyValid()
        {
            var apiKey = Request.Headers["apiKey"];
            return _store.IsValidApiKey(apiKey);
        }

        public ActionResult Http403()
        {
            Response.StatusCode = 403;
            return Content("Unknown API key.", "text/plain");
        }
        
        //
        // GET: /Editor/{id}
        [ActionName("Index")]
        [HttpGet]
        public ActionResult Editor([DefaultValue(0)]int id)
        {
            var model = new IndexViewModel();
            if (Request.IsAuthenticated)
            {
                if (id != 0)
                {
                    model.BlueprintId = id;
                }
                model.ApiKey = UserInfo.ApiKey;
            }
            
            return View(model);
        }

      
        //
        // POST: /Editor/
        [HttpPost]
        public ActionResult Index(JsonTest model)
        {
            if (!IsApiKeyValid()) return Http403();

            if (ModelState.IsValid)
            {
                return Json(new JsonTest { Message = "OK" });
            }
            string errorMessage = "";
            foreach (var key in ModelState.Keys)
            {
                var error = ModelState[key].Errors.FirstOrDefault();
                if (error != null)
                {
                    errorMessage += " " + error.ErrorMessage + " ";
                }
            }
            return Json(new JsonTest { Message = errorMessage });
        }

    }
}
