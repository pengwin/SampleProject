using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DotNetOpenAuth.OpenId.RelyingParty;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;

using SampleProject.ViewModels.User;

namespace SampleProject.Controllers
{
    public class UserController : Controller
    {
        #region Private field
        private OpenIdRelyingParty _openId;
        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        public UserController()
        {
            _openId = new OpenIdRelyingParty();
        }

        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }

        #region Login

        /// <summary>
        /// Shows the login page with a login form.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
        {
            var response = _openId.GetResponse();


            if (response != null)
            {
                switch (response.Status)
                {
                    case AuthenticationStatus.Authenticated:
                        /*FormsAuthentication.RedirectFromLoginPage(
                            response.ClaimedIdentifier, false);*/
                        return View("Index", new UserViewData
                                                 {
                                                     OpenIdUrl = response.ClaimedIdentifier
                                                 });
                        break;
                    case AuthenticationStatus.Canceled:
                        ModelState.AddModelError("OpenIdUrl",
                            "Login was cancelled at the provider");
                        break;
                    case AuthenticationStatus.Failed:
                        ModelState.AddModelError("OpenIdUrl",
                            "Login failed using the provided OpenID identifier");
                        break;
                }
            }

            return View();
        }

        /// <summary>
        /// Gets the openId url from the view form.
        /// Performs request to the openId provider.
        /// </summary>
        /// <param name="loginData"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(LoginViewData loginData)
        {
            Identifier id;
            // validate the openid url
            if (!Identifier.TryParse(loginData.OpenIdUrl, out id))
            {
                ModelState.AddModelError("OpenIdUrl","The specified login identifier is invalid");
                return View();
            }

            try
            {
                var openid = new OpenIdRelyingParty();
                IAuthenticationRequest request = openid.CreateRequest(id);

                // request some additional data
                request.AddExtension(new ClaimsRequest
                                         {
                                             Nickname = DemandLevel.NoRequest,
                                             Email = DemandLevel.Require,
                                             FullName = DemandLevel.Require
                                         });
                // make request
                return request.RedirectingResponse.AsActionResult();
            }
            catch(ProtocolException e)
            {
                    ModelState.AddModelError("OpenIdUrl", e.Message);
            }
            return View();
        }


        #endregion

    }
}
