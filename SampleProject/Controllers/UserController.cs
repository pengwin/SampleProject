using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using DotNetOpenAuth.OpenId.RelyingParty;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using Ninject;
using SampleProject.Authentication;
using SampleProject.Models;
using SampleProject.Models.UserModels;
using SampleProject.ViewModels.User;

using SampleProject.Common;

namespace SampleProject.Controllers
{
    public class UserController : BaseController
    {
        #region Consts

        public const string GoogleOpenId = "https://www.google.com/accounts/o8/id";
        public const string YandexOpenId = "http://openid.yandex.ru/";

        #endregion

        #region Private fields

        private readonly IUserRepository _users;
        private readonly OpenIdRelyingParty _openId;
        private readonly ILogger _logger;

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        public UserController(ILogger logger, IUserRepository users, IUserAuthService userAuth)
            : base(userAuth)
        {
            _openId = new OpenIdRelyingParty();
            _logger = logger;
            _users = users;
        }

        #region Index

        /// <summary>
        /// GET: /User/
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {

            //var tt = HttpContext.User.IsInRole("tt");
            ViewData["Username"] = UserInfo.Username;
            return View();
        }

        /// <summary>
        /// GET: /User/
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Test()
        {
            return View();
        }

        #endregion

        #region Login

        /// <summary>
        /// GET: /User/Login
        /// Shows the login page with a login form.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            return View(new LoginViewData { ReturnUrl = returnUrl });
        }

        /// <summary>
        /// POST: /User/Login
        /// Gets the openId url from the view form.
        /// Performs request to the openId provider.
        /// </summary>
        /// <param name="loginData"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(LoginViewData loginData)
        {
            var returnUrl = loginData.ReturnUrl ?? Url.Action("Index");
            switch (loginData.LoginMethod)
            {
                case LoginMethod.Google:
                    return RedirectToAction("OpenIdLogin", "User", new { openIdUrl = GoogleOpenId, returnUrl });
                case LoginMethod.Yandex:
                    return RedirectToAction("OpenIdLogin", "User", new { openIdUrl = YandexOpenId, returnUrl });
                case LoginMethod.OpenId:
                    return RedirectToAction("OpenIdLogin", "User", new { openIdUrl = loginData.OpenIdUrl, returnUrl });
            }
            return View();
        }

        #endregion

        /// <summary>
        /// GET: /User/Logout
        /// Logout the current user.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Logout()
        {
            UserAuthService.Logout();
            return RedirectToAction("Index", "Home");
        }

        #region OpenIdLogin

        /// <summary>
        /// GET: /User/OpenId
        /// Performs openId authorization operations.
        /// Can be called in 3 cases:
        /// 1) user manually navigated to /User/OpenId
        /// 2) redirect from login action
        /// 3) callback from openId provider
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OpenIdLogin(string openIdUrl, string returnUrl)
        {
            // get the openId response
            var response = _openId.GetResponse();

            // case #1
            // user manually navigated to /User/OpenId
            if (string.IsNullOrEmpty(openIdUrl) && (response == null))
            {
                // return to the login action
                return RedirectToAction("Login", "User");
            }

            // case #2
            // redirect from login action
            if (!string.IsNullOrEmpty(openIdUrl) && (response == null))
            {
                // make openId request
                return SendOpenIdRequest(openIdUrl);
            }

            // case #3
            // callback from openId provider
            if (response != null)
            {
                var tt = returnUrl;
                // process the response
                return ProcessOpenIdResponse(response, returnUrl);
            }

            return View();
        }

        /// <summary>
        /// Sends auth request to openId provider.
        /// </summary>
        /// <param name="openIdUrl">openId identifier</param>
        /// <returns></returns>
        private ActionResult SendOpenIdRequest(string openIdUrl)
        {
            Identifier id;
            // validate the openID url
            if (!Identifier.TryParse(openIdUrl, out id))
            {
                _logger.Info("Invalid openId url: " + openIdUrl);
                ModelState.AddModelError("OpenIdUrl", "The specified login identifier is invalid");
                return View("Login");
            }

            try
            {
                IAuthenticationRequest request = _openId.CreateRequest(id);

                // request some additional data
                request.AddExtension(new ClaimsRequest
                {
                    Nickname = DemandLevel.Require,
                    Email = DemandLevel.Require,
                    FullName = DemandLevel.Require
                });
                // make request
                return request.RedirectingResponse.AsActionResult();
            }
            catch (ProtocolException e)
            {
                _logger.Error("Open Id exception: " + e);
                ModelState.AddModelError("OpenIdUrl", "OpenID Exception:" + e.Message);
            }
            return View("Login");
        }

        /// <summary>
        /// Processes the response from OpenId server.
        /// </summary>
        /// <param name="response"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        private ActionResult ProcessOpenIdResponse(IAuthenticationResponse response, string returnUrl)
        {
            switch (response.Status)
            {
                case AuthenticationStatus.Authenticated:

                    var identifier = response.ClaimedIdentifier;

                    // try to load openid from database
                    var user = _users.GetUserByOpenId(identifier);

                    if (user == null)
                    {
                        // get requested fields
                        var claimsResponse = response.GetExtension<ClaimsResponse>();

                        // create a register view model
                        var registerViewModel = new RegisterViewModel { OpenIdIdentifier = identifier, ReturnUrl = returnUrl};
                        // get fancy name for the openId provider
                        registerViewModel.OpenIdProviderName = Models.UserModels.OpenId.GuessOpenIdFancyName(identifier);

                        if (claimsResponse != null)
                        {
                            if (!string.IsNullOrEmpty(claimsResponse.Nickname)) registerViewModel.Username = claimsResponse.Nickname;
                            if (!string.IsNullOrEmpty(claimsResponse.Email)) registerViewModel.Email = claimsResponse.Email;
                            if (!string.IsNullOrEmpty(claimsResponse.FullName)) registerViewModel.FullName = claimsResponse.FullName;
                        }

                        // show the register form
                        return View("Register", registerViewModel);  
                    }


                    // set auth cookies
                    UserAuthService.LoginUser(user);
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");

                case AuthenticationStatus.Canceled:
                    _logger.Error("OpenId was canceled at provider. OpenId: " + response.ClaimedIdentifier);
                    ModelState.AddModelError("OpenIdUrl","Login was cancelled at the provider");
                    break;
                case AuthenticationStatus.Failed:
                    _logger.Error("Login failed using the provided OpenID identifier: " + response.ClaimedIdentifier);
                    ModelState.AddModelError("OpenIdUrl","Login failed using the provided OpenID identifier");
                    break;
            }
            return View("Login");
        }

        #endregion

        #region Register

        /// <summary>
        /// POST: /User/Register/
        /// Gets response from registration form.
        /// Creates users and ends auth process.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // create user
            var user = new User {Username = model.Username, FullName = model.FullName, Email = model.Email};
            try
            {
                _users.CreateUserWithOpenId(model.OpenIdIdentifier, user);
                _users.SaveChanges();
            }
            catch (CreateUserException ex)
            {
                ModelState.AddModelError("OpenIdIdentifier",ex.Message);
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // set auth cookies
            UserAuthService.LoginUser(user);
            
            if (!string.IsNullOrEmpty(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
            }
            return View(model);
        }

        #endregion

    }
}
