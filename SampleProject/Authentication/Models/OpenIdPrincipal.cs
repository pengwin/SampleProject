using System.Linq;
using System.Security.Principal;

namespace SampleProject.Authentication.Models
{
    /// <summary>
    /// Custom realization of openid principal.
    /// </summary>
    public class OpenIdPrincipal : IPrincipal
    {

        #region Private fields

        private readonly OpenIdIdentity _id;

        #endregion

        public OpenIdPrincipal(OpenIdIdentity id)
        {
            _id = id;
        }

        public bool IsInRole(string role)
        {
            return _id.UserInfo.Roles.Contains(role);
        }

        public IIdentity Identity
        {
            get { return _id; }
        }
    }
}