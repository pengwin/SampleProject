using System.Security.Principal;
using System.Web.Security;

namespace SampleProject.Authentication.Models
{
    /// <summary>
    /// IIdentity implementation of OpenId logins.
    /// </summary>
    public class OpenIdIdentity : IIdentity
    {
        private FormsAuthenticationTicket _ticket;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="ticket">Ticket to be used to construct the IIdentity.</param>
        public OpenIdIdentity(FormsAuthenticationTicket ticket)
        {
            _ticket = ticket;
            UserInfo = UserInfo.FromString(_ticket.UserData);
        }

        /// <summary>
        /// Authentication Type.
        /// </summary>
        public string AuthenticationType
        {
            get { return "OpenIdAuth"; }
        }

        /// <summary>
        /// Return if the user is authenticated.
        /// </summary>
        /// <remarks>This always returns true as User.Identity in controllers only be castable to OpenIdIdentity if we actually set it in the Global.asax.cs. See the MvcApplication_PostAuthenticateRequest method.</remarks>
        public bool IsAuthenticated
        {
            get { return true; }
        }

        /// <summary>
        /// Username of the user.
        /// </summary>
        public string Name
        {
            get { return _ticket.Name; }
        }

        /// <summary>
        /// User info of the user that is logged in.
        /// </summary>
        public UserInfo UserInfo { get; private set; }
 
    }
}