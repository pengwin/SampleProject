using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SampleProject.Models;

namespace SampleProject.Infrastructure
{
    /// <summary>
    /// Service for managing user roles
    /// </summary>
    public interface IRoleService
    {
        bool AdminExists();
        void AddUsersToRoles(User[] users, UserRole[] roles);
        void RemoveUsersFromRoles(User[] users, UserRole[] roles);
        void AddRole(UserRole role);
    }
}