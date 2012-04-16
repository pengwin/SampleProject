using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SampleProject.Authentication;
using SampleProject.Common;
using SampleProject.Models.BlueprintModels;
using SampleProject.Models.Repositories;
using SampleProject.ViewModels.BlueprintAjax;
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
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="authService"> </param>
        /// <param name="logger"> </param>
        public EditorController(IUserAuthService authService,ILogger logger) : base(authService,logger)
        {

        }

        #region Index page

        //
        // GET: /Editor/{id}
        [HttpGet]
        public ActionResult Index()
        {
            var model = new IndexViewModel();
            if (Request.IsAuthenticated)
            {
                model.ApiKey = UserInfo.ApiKey;
            }

            return View(model);
        }

        #endregion

        
    }

       
}


