using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SampleProject.Models;
using SampleProject.Models.Auth;

namespace SampleProject.Controllers
{
    public abstract class BaseController : Controller
    {

        protected UserInfo UserInfo;

        #region Initialize
        /// <summary>
        /// Controller initialization.
        /// </summary>
        /// <param name="requestContext">Request Context</param>
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            // If the user is authentifated let's set the userInfo so we don't have to cast User.Identity every time.
            if (Request.IsAuthenticated)
                UserInfo = ((OpenIdIdentity)User.Identity).UserInfo;
        }
        #endregion

    }
}
