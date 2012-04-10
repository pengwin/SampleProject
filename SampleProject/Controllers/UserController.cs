﻿using System;
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
using SampleProject.Models;
using SampleProject.Models.Auth;
using SampleProject.ViewModels.User;

namespace SampleProject.Controllers
{
    public class UserController : Controller
    {
        #region Consts

        public const string GoogleOpenId = "https://www.google.com/accounts/o8/id";
        public const string YandexOpenId = "http://openid.yandex.ru/";

        #endregion

        #region Private field

        private readonly OpenIdRelyingParty _openId;

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        public UserController()
        {
            _openId = new OpenIdRelyingParty();
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
                    return RedirectToAction("OpenId", "User", new { openIdUrl = GoogleOpenId, returnUrl });
                case LoginMethod.Yandex:
                    return RedirectToAction("OpenId", "User", new { openIdUrl = YandexOpenId, returnUrl });
                case LoginMethod.OpenId:
                    return RedirectToAction("OpenId", "User", new { openIdUrl = loginData.OpenIdUrl, returnUrl });
            }
            return View();
        }

        #endregion

        #region OpenId

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
        public ActionResult OpenId(string openIdUrl,string returnUrl)
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
                return ProcessOpenIdResponse(response,returnUrl);
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
                ModelState.AddModelError("OpenIdUrl", "The specified login identifier is invalid");
                return View("Login");
            }

            try
            {
                IAuthenticationRequest request = _openId.CreateRequest(id);

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
            catch (ProtocolException e)
            {
                ModelState.AddModelError("OpenIdUrl", "OpenID Exception:" + e.Message);
            }
            return View("Login");
        }

        private ActionResult ProcessOpenIdResponse(IAuthenticationResponse response,string returnUrl)
        {
            switch (response.Status)
            {
                case AuthenticationStatus.Authenticated:
                    // get requested fields
                    var claimsResponse = response.GetExtension<ClaimsResponse>();

                    var user = new User()
                    {
                        Username = response.ClaimedIdentifier,
                        OpenIds = new List<OpenId>
                                                         {
                                                             new OpenId
                                                                 {OpenIdUrl = response.ClaimedIdentifier}
                                                         }
                    };
                    // set auth cookies
                    IssueFormsAuthenticationTicket(user);
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index","Home");
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
            return View("Login");
        }

        #endregion

        #region Register

        public ActionResult Register()
        {
            return View();
        }

        #endregion

        #region IssueFormsAuthenticationTicket
        /// <summary>
        /// Issues the FormsAuthenticationTicket to let ASP.NET know that a user is logged in.
        /// </summary>
        /// <param name="user">User that has logged in.</param>
        private void IssueFormsAuthenticationTicket(User user)
        {
            // We need to make a FormsAuthenticationTicket.
            // To store UserInfo data in it we use the 2nd overload.
            var ticket = new FormsAuthenticationTicket(1,
                user.Username,
                DateTime.Now,
                DateTime.Now.AddDays(14),
                true,
                UserInfo.FromUser(user).ToString(),
                FormsAuthentication.FormsCookiePath);

            // Now we encrypt the ticket so no one can read it...
            string encTicket = FormsAuthentication.Encrypt(ticket);

            // ...make a cookie and add it. ASP.NET will now know that our user is logged in.
            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
        }
        #endregion

    }
}
