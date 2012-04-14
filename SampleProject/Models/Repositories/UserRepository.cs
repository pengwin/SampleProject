using System;
using System.Collections.Generic;
using System.Linq;
using SampleProject.Models.UserModels;

namespace SampleProject.Models.Repositories
{

    public class CreateUserException : Exception
    {
        public CreateUserException(string message) : base(message)
        {
           
        }
    }

    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string message)
            : base(message)
        {

        }
    }

    /// <summary>
    /// User entities repository.
    /// Uses EnityFramework.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        #region Private fields

        private readonly DatabaseContext _userDb;

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="userDb">UserContext.</param>
        public UserRepository(DatabaseContext userDb)
        {
            _userDb = userDb;
        }

        /// <summary>
        /// Gets OpenId from the OpenID identifier.
        /// </summary>
        /// <param name="identifier">OpenID identifier.</param>
        /// <returns></returns>
        private OpenId GetOpenId(string identifier)
        {
            return _userDb.OpenIds.SingleOrDefault(o => o.OpenIdUrl == identifier);
        }

        
        /// <summary>
        /// Save the changes to the database.
        /// </summary>
        public void SaveChanges()
        {
            _userDb.SaveChanges();
        }

        #region GetUser

        /// <summary>
        /// Gets the User by UserId.
        /// </summary>
        /// <param name="id">UserId of the user.</param>
        /// <returns>User with a specific UserId.</returns>
        public User GetUserById(int id)
        {
            return (_userDb.Users.SingleOrDefault(u => u.UserId == id));
        }

        /// <summary>
        /// Gets the user with the specific OpenID identifier.
        /// </summary>
        /// <param name="openId">Identifier of the user.</param>
        /// <returns>User that is assocciated with the specific identifier. If the identifier is not found in the database, return null.</returns>
        public User GetUserByOpenId(string openId)
        {
            var openid = GetOpenId(openId);
            if (openid != null)
                return openid.User;

            return null;
        }

        /// <summary>
        /// Gets the user with the specific API key.
        /// </summary>
        /// <param name="apiKey">Identifier of the user.</param>
        /// <returns>User that is associated with the specific api key. If the identifier is not found in the database, return null.</returns>
        public User GetUserByApiKey(Guid apiKey)
        {
            return (_userDb.Users.SingleOrDefault(u => u.ApiKey == apiKey));
        }

        /// <summary>
        /// Gets all existing users.
        /// </summary>
        /// <returns>the list of users</returns>
        public IList<User> GetAllUsers()
        {
            return _userDb.Users.ToList();
        }

        /// <summary>
        /// Gets users quantity.
        /// </summary>
        /// <returns></returns>
        public int GetUsersCount()
        {
            return _userDb.Users.Count();
        }

        #endregion

        #region Create/Remove

        /// <summary>
        /// Creates a user in database with the specific OpenID.
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="user"></param>
        public void CreateUserWithOpenId(string openId, User user)
        {
            var openid = GetOpenId(openId);
            if (openid != null && openid.User == null)
                throw new CreateUserException(string.Format("User with {0} OpenID already exists.", openId));
            // create openId and bind the user to the openid
            openid = new OpenId {OpenIdUrl = openId, User = user};

            user.ApiKey = Guid.NewGuid();

            // save openid
            _userDb.OpenIds.Add(openid); //EFCodeFirst
        }

        /// <summary>
        /// Removes user from the database.
        /// </summary>
        /// <param name="user">User to be removed.</param>
        /// <remarks>User deletion is cascading which means that all of user's OpenIDs will get deleted from the database when the user is deleted.</remarks>
        public void RemoveUser(User user)
        {
            _userDb.Users.Remove(user); //EFCodeFirst
        }

        #endregion

    }
}