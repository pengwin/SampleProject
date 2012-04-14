using System;
using System.Collections.Generic;
using SampleProject.Models.UserModels;

namespace SampleProject.Models.Repositories
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
        User GetUserById(int id);

        /// <summary>
        /// Gets the user with the specific OpenID identifier.
        /// </summary>
        /// <param name="openId">Identifier of the user.</param>
        /// <returns>User that is assocciated with the specific identifier. If the identifier is not found in the database, return null.</returns>
        User GetUserByOpenId(string openId);

        /// <summary>
        /// Gets the user with the specific API key.
        /// </summary>
        /// <param name="apiKey">Identifier of the user.</param>
        /// <returns>User that is associated with the specific api key. If the identifier is not found in the database, return null.</returns>
        User GetUserByApiKey(Guid apiKey);

        /// <summary>
        /// Gets all existing users.
        /// </summary>
        /// <returns>the list of users</returns>
        IList<User> GetAllUsers();

        /// <summary>
        /// Gets users quantity.
        /// </summary>
        /// <returns></returns>
        int GetUsersCount();


        void CreateUserWithOpenId(string openId,User user);

        /// <summary>
        /// Removes user from the database.
        /// </summary>
        /// <param name="user">User to be removed.</param>
        /// <remarks>User deletion is cascading which means that all of user's OpenIDs will get deleted from the database when the user is deleted.</remarks>
        void RemoveUser(User user);
    }
}