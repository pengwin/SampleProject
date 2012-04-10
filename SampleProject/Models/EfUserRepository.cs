﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using SampleProject.Models.Auth;

namespace SampleProject.Models
{
    /// <summary>
    /// User and OpenId entities repository.
    /// Uses EnityFramework.
    /// </summary>
    public class EfUserRepository : IUserRepository
    {
        #region Private fields 

        private readonly UserContext _userDb;

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        public EfUserRepository()
            : this(null)
        { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="userDb">UserContext.</param>
        public EfUserRepository(UserContext userDb)
        {
            _userDb = userDb ?? new UserContext();
        }

        /// <summary>
        /// Save the changes to the database.
        /// </summary>
        public void SaveChanges()
        {
            _userDb.SaveChanges();
        }

        /// <summary>
        /// Gets the User by UserId.
        /// </summary>
        /// <param name="id">UserId of the user.</param>
        /// <returns>User with a specific UserId.</returns>
        public User GetUser(int id)
        {
            return (_userDb.Users.SingleOrDefault(u => u.UserId == id));
        }

        /// <summary>
        /// Gets the user with the specific identifier.
        /// </summary>
        /// <param name="identifier">Identifier of the user.</param>
        /// <returns>User that is assocciated with the specific identifier. If the identifier is not found in the database, return null.</returns>
        public User GetUser(string identifier)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(identifier));

            var openid = _userDb.OpenIds.SingleOrDefault(o => o.OpenIdUrl == identifier);
            if (openid != null)
                return openid.User;

            return null;
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

        /// <summary>
        /// Gets OpenId from the OpenID identifier.
        /// </summary>
        /// <param name="identifier">OpenID identifier.</param>
        /// <returns></returns>
        public OpenId GetOpenId(string identifier)
        {
            return _userDb.OpenIds.SingleOrDefault(o => o.OpenIdUrl == identifier);
        }

        /// <summary>
        /// Adds a new OpenId to the database.
        /// </summary>
        /// <param name="openid">OpenId to be added.</param>
        public void AddOpenId(OpenId openid)
        {
            Contract.Requires<ArgumentNullException>(openid != null);

            _userDb.OpenIds.Add(openid); //EFCodeFirst
        }

        /// <summary>
        /// Removes the OpenId from the database.
        /// </summary>
        /// <param name="identifier">Identifier to be removed.</param>
        public void RemoveOpenId(string identifier)
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