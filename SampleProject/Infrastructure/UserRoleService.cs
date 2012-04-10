using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using SampleProject.Models;

namespace SampleProject.Infrastructure
{
    /// <summary>
    /// Manages user roles
    /// </summary>
    public class UserRoleService : IRoleService
    {
        #region Private fields

        private readonly RoleProvider _provider;

        #endregion

        #region IRoleService implementation

        public UserRoleService(RoleProvider provider)
        {
            _provider = provider;
        }

        public bool AdminExists()
        {
            var users = _provider.GetUsersInRole("Admin");

            if (users.Count() == 0)
                return false;

            return true;
        }

        public void AddUsersToRoles(User[] users, UserRole[] roles)
        {
            //_provider.AddUsersToRoles(usernames, rolenames);
        }

        public void RemoveUsersFromRoles(User[] users, UserRole[] roles)
        {
            //_provider.RemoveUsersFromRoles(usernames, rolenames);
        }

        public void AddRole(UserRole role)
        {
            //_provider.CreateRole(roleName);
        }

        #endregion
    }
}