using SampleProject.Models.Auth;

namespace SampleProject.Models
{
    public interface IUserRepository
    {
        /// <summary>
        /// Save the changes to the database.
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Gets the User by UserId.
        /// </summary>
        /// <param name="id">UserId of the user.</param>
        /// <returns>User with a specific UserId.</returns>
        User GetUser(int id);

        /// <summary>
        /// Gets the user with the specific identifier.
        /// </summary>
        /// <param name="identifier">Identifier of the user.</param>
        /// <returns>User that is assocciated with the specific identifier. If the identifier is not found in the database, return null.</returns>
        User GetUser(string identifier);

        /// <summary>
        /// Removes user from the database.
        /// </summary>
        /// <param name="user">User to be removed.</param>
        /// <remarks>User deletion is cascading which means that all of user's OpenIDs will get deleted from the database when the user is deleted.</remarks>
        void RemoveUser(User user);

        /// <summary>
        /// Gets OpenId from the OpenID identifier.
        /// </summary>
        /// <param name="identifier">OpenID identifier.</param>
        /// <returns></returns>
        OpenId GetOpenId(string identifier);

        /// <summary>
        /// Adds a new OpenId to the database.
        /// </summary>
        /// <param name="openid">OpenId to be added.</param>
        void AddOpenId(OpenId openid);

        /// <summary>
        /// Removes the OpenId from the database.
        /// </summary>
        /// <param name="identifier">Identifier to be removed.</param>
        void RemoveOpenId(string identifier);
    }
}