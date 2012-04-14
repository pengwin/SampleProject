using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SampleProject.Authentication;
using SampleProject.Authentication.Models;
using SampleProject.Common;
using SampleProject.Models;

namespace SampleProject.Controllers
{
    public abstract class BaseController : Controller
    {
        #region Protected fields

        protected IUserAuthService UserAuthService;
        protected UserInfo UserInfo;
        protected ILogger Logger;

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="userAuthService"></param>
        protected BaseController(IUserAuthService userAuthService,ILogger logger)
        {
            UserAuthService = userAuthService;
            Logger = logger;
        }

        #region Initialize
        /// <summary>
        /// Controller initialization.
        /// </summary>
        /// <param name="requestContext">Request Context</param>
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            // If the user is authentifated let's set the userInfo so we don't have to cast User.Identity every time.
            if (Request.IsAuthenticated)
                UserInfo = UserAuthService.GetCurrentUserInfo();
        }
        #endregion

    }
}
