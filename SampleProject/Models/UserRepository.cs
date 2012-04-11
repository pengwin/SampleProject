using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using SampleProject.Models.UserModels;

namespace SampleProject.Models
{

    public class CreateUserException : Exception
    {
        public CreateUserException(string message) : base(message)
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

        private readonly UserContext _userDb;

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        public UserRepository()
            : this(null)
        { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="userDb">UserContext.</param>
        public UserRepository(UserContext userDb)
        {
            _userDb = userDb ?? new UserContext();
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
        public void CreateUserWithOpenId(string openId,User user)
        {
            var openid = GetOpenId(openId);
            if (openid != null && openid.User == null) throw new CreateUserException(string.Format("User with {0} OpenID already exists.",openId));
            // create openId and bind the user to the openid
            openid = new OpenId {OpenIdUrl = openId, User = user};

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

        /// <summary>
        /// Removes the OpenId from the database.
        /// </summary>
        /// <param name="identifier">Identifier to be removed.</param>
        private void RemoveOpenId(string identifier)
        {
            var openid = _userDb.OpenIds.SingleOrDefault(o => o.OpenIdUrl == identifier);

            if (openid == null)
            {
                throw new NullReferenceException("openid is null");
            }

            if (openid.User.OpenIds.Count > 1)
            {
                _userDb.OpenIds.Remove(openid); //EFCodeFirst
            }
            else
                throw new Exception("Cannot delete the last OpenID identifier. Every user account has to be associated with at least on OpenID.");
        }



    }
}