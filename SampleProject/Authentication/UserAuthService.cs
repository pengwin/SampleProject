using System;
using System.Web;
using System.Web.Security;
using SampleProject.Authentication.Models;
using SampleProject.Models.UserModels;

namespace SampleProject.Authentication
{
    /// <summary>
    /// Provides user authorization operations.
    /// Encapsulates operations with HttpContext and cookies.
    /// </summary>
    public class UserAuthService : IUserAuthService
    {
        /// <summary>
        /// Gets current user info from HttpContext.Current.User
        /// </summary>
        /// <returns>null if user doesn't exist</returns>
        public UserInfo GetCurrentUserInfo()
        {
            var principal = HttpContext.Current.User as OpenIdPrincipal;
            if (principal == null) return null;
            var identity = principal.Identity as OpenIdIdentity;
            if (identity == null) return null;
            return identity.UserInfo;
        }

        /// <summary>
        /// Sets current user info in HttpContext.Current.User
        /// </summary>
        /// <returns></returns>
        public void SetCurrentUserInfo()
        {
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                string encTicket = authCookie.Value;
                if (!String.IsNullOrEmpty(encTicket))
                {
                    var ticket = FormsAuthentication.Decrypt(encTicket);
                    var id = new OpenIdIdentity(ticket);
                    var principal = new OpenIdPrincipal(id);
                    HttpContext.Current.User = principal;
                }
            }
        }

        /// <summary>
        /// Sets authentication cookie with auth ticket for the specific user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public void LoginUser(User user)
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
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            HttpContext.Current.Response.Cookies.Add(authCookie);
        }

        /// <summary>
        /// Logout the current user.
        /// Deletes authentication cookies.
        /// </summary>
        public void Logout()
        {
            HttpContext.Current.Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
        }
    }

    
}