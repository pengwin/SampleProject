using System;
using System.Linq;
using SampleProject.Models.UserModels;

namespace SampleProject.Models.Repositories
{

    public class AddRoleException : Exception
    {
        public AddRoleException(string message)
            : base(message)
        {

        }
    }

    /// <summary>
    /// UserRole entities repository.
    /// Uses EnityFramework.
    /// </summary>
    public class RoleRepository : IRoleRepository
    {
        #region Private fields

        private readonly DatabaseContext _userDb;

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="userDb">UserContext.</param>
        public RoleRepository(DatabaseContext userDb)
        {
            _userDb = userDb;
        }

        /// <summary>
        /// Save the changes to the database.
        /// </summary>
        public void SaveChanges()
        {
            _userDb.SaveChanges();
        }

        #region GetRole

        /// <summary>
        /// Gets the Role by RoleId.
        /// </summary>
        /// <param name="id">RoleId of the role.</param>
        /// <returns>Role with a specific RoleId.</returns>
        public Role GetRoleById(int id)
        {
            return (_userDb.Roles.SingleOrDefault(r => r.RoleId == id));
        }

        /// <summary>
        /// Gets the role with the specific name.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <returns>Role that is assocciated with the specific identifier. If the identifier is not found in the database, return null.</returns>
        public Role GetRoleByName(string roleName)
        {
            return (_userDb.Roles.SingleOrDefault(r => r.RoleName == roleName));
        }

        #endregion

        #region Add/Remove

        /// <summary>
        /// Adds a new role to the database.
        /// </summary>
        /// <param name="roleName">Name of role to be added.</param>
        public void AddRoleWidthName(string roleName)
        {
            var role = new Role { RoleName = roleName };
            AddRole(role);
        }

        /// <summary>
        /// Adds a new role to the database.
        /// </summary>
        /// <param name="role">Role to be added.</param>
        public void AddRole(Role role)
        {
            if (GetRoleByName(role.RoleName) != null) throw new AddRoleException(string.Format("User with name {0} already exists.", role.RoleName));
            _userDb.Roles.Add(role); //EFCodeFirst
        }

        /// <summary>
        /// Adds a role with specific name to the user with usedId.
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="userId"></param>
        public void AddRoleToUser(string roleName, int userId)
        {
            var user = _userDb.Users.SingleOrDefault(u => u.UserId == userId);
            var role = GetRoleByName(roleName);
            if (role == null)
            {
                role = new Role {RoleName = roleName};
                AddRole(role);
            }
            role.Users.Add(user);            
        }

        /// <summary>
        /// Removes the role from the database.
        /// </summary>
        /// <param name="role">Role to be removed.</param>
        public void RemoveRole(Role role)
        {
            _userDb.Roles.Remove(role); //EFCodeFirst
        }

        #endregion

    }
}