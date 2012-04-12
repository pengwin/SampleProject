using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using SampleProject.Models.BlueprintModels;
using SampleProject.Models.UserModels;

namespace SampleProject.Models
{

    /// <summary>
    /// Blueprint entities repository.
    /// Uses EnityFramework.
    /// </summary>
    public class BlueprintRepository : IBlueprintRepository
    {
        #region Private fields

        private readonly UserContext _userDb;

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="userDb">UserContext.</param>
        public BlueprintRepository(UserContext userDb)
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

        #region GetBlueprint

        /// <summary>
        /// Gets the blueprint by id.
        /// </summary>
        /// <param name="id">id of the blueprint.</param>
        /// <returns>blueprint with a specific id.</returns>
        public Blueprint GetBlueprintId(int id)
        {
            return (_userDb.Blueprints.SingleOrDefault(u => u.BlueprintId == id));
        }

        
        /// <summary>
        /// Gets all existing blueprints.
        /// </summary>
        /// <returns>the list of blueprints</returns>
        public IList<Blueprint> GetAllBlueprint()
        {
            return _userDb.Blueprints.ToList();
        }

        /// <summary>
        /// Gets blueprints quantity.
        /// </summary>
        /// <returns></returns>
        public int GetBlueprintsCount()
        {
            return _userDb.Blueprints.Count();
        }

        #endregion

        #region Create/Remove

        /// <summary>
        /// Creates a blueprint in database for the specific user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="blueprint"></param>
        public void CreateBlueprintForUser(int userId, Blueprint blueprint)
        {
            var user = _userDb.Users.SingleOrDefault(u => u.UserId == userId);
            if (user == null) throw new ArgumentException(String.Format("User with id {0} doesn't exist.",userId));

            blueprint.User = user;
            // save 
            _userDb.Blueprints.Add(blueprint); //EFCodeFirst
        }

        /// <summary>
        /// Removes the blueprint from the database.
        /// </summary>
        /// <param name="blueprint">Blueprint to be removed.</param>
        public void RemoveBlueprint(Blueprint blueprint)
        {
            _userDb.Blueprints.Remove(blueprint); //EFCodeFirst
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