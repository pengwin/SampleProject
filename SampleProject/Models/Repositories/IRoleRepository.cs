using SampleProject.Models.UserModels;

namespace SampleProject.Models.Repositories
{
    public interface IRoleRepository
    {
        /// <summary>
        /// Save the changes to the database.
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Gets the Role by RoleId.
        /// </summary>
        /// <param name="id">RoleId of the role.</param>
        /// <returns>Role with a specific RoleId.</returns>
        Role GetRoleById(int id);

        /// <summary>
        /// Gets the role with the specific name.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <returns>Role that is assocciated with the specific identifier. If the identifier is not found in the database, return null.</returns>
        Role GetRoleByName(string roleName);

        /// <summary>
        /// Adds a new role to the database.
        /// </summary>
        /// <param name="roleName">Name of role to be added.</param>
        void AddRoleWidthName(string roleName);

        /// <summary>
        /// Adds a new role to the database.
        /// </summary>
        /// <param name="role">Role to be added.</param>
        void AddRole(Role role);

        /// <summary>
        /// Adds a role with specific name to the user specified by userId.
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="userId"></param>
        void AddRoleToUser(string roleName,int userId);

        /// <summary>
        /// Removes the role from the database.
        /// </summary>
        /// <param name="role">Role to be removed.</param>
        void RemoveRole(Role role);
    }
}