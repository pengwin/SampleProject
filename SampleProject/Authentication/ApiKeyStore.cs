using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SampleProject.Models.Repositories;
using SampleProject.Models.UserModels;

namespace SampleProject.Authentication
{
    /// <summary>
    /// Simple ApiKey store.
    /// Store keys in the ASP.net cache.
    /// Validates api keys.
    /// </summary>
    public class ApiKeyStore : IApiKeyStore
    {
        #region Private fields

        private const string ApiKeyList = "ApiStore";
        private const string UsersDict = "UsersStore";
        private readonly IUserRepository _users;

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="users"></param>
        public ApiKeyStore(IUserRepository users)
        {
            _users = users;
        }
        
        /// <summary>
        /// Api keys cache.
        /// </summary>
        private List<Guid> APIKeys
        {
            get
            {
                // Get from the asp.net cache
                return HttpContext.Current.Cache[ApiKeyList] as List<Guid> ?? new List<Guid>();
            }
            set
            {
                // Put the value to the asp.net cache
                HttpContext.Current.Cache[ApiKeyList] = value;
            }
        }

        /// <summary>
        /// Users cache.
        /// </summary>
        private Dictionary<Guid,User> UsersCache
        {
            get
            {
                // Get from the asp.net cache
                return HttpContext.Current.Cache[UsersDict] as Dictionary<Guid, User>;
            }
            set
            {
                // Put the value to the asp.net cache
                HttpContext.Current.Cache[UsersDict] = value;
            }
        }

        /// <summary>
        /// Validates the specified api key.
        /// </summary>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        public bool IsValidApiKey(string apiKey)
        {
            return GetApiKeyOwner(apiKey) != null;
        }

        /// <summary>
        /// Gets user who has this api key.
        /// </summary>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        public User GetApiKeyOwner(string apiKey)
        {
            Guid guidKey;

            // Convert the string into a Guid and validate it
            if (!Guid.TryParse(apiKey, out guidKey))
            {
                return null;
            }

            if (UsersCache == null)
            {
                UsersCache = new Dictionary<Guid, User>();
            }

            // if key is in the cache
            if (UsersCache.ContainsKey(guidKey))
            {
                // key is valid
                return UsersCache[guidKey];
            }
            // try to get related user from the database
            var user = _users.GetUserByApiKey(guidKey);
            if (user != null)
            {
                // user with this key exists
                // put key into the cache
                UsersCache.Add(guidKey, user);
                return user;
            }
            return null;
        }
    }
}