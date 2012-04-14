using System.Collections.Generic;
using System.Linq;
using SampleProject.Models.BlueprintModels;

namespace SampleProject.Models.BlueprintSearch
{
    
    /// <summary> 
    /// Provides the search in the blueprint database.
    /// </summary>
    public class BlueprintSearchService : IBlueprintSearchService
    {
         #region Private fields

        private readonly DatabaseContext _userDb;

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="userDb">UserContext.</param>
        public BlueprintSearchService(DatabaseContext userDb)
        {
            _userDb = userDb;
        }

        /// <summary>
        /// Searches blueprints with name or description or author which contains specified strings.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="author"></param>
        /// <returns></returns>
        public IList<Blueprint> FindBlueprintsContain(string name, string description, string author)
        {
            var result = _userDb.Blueprints.Where(b => b.User.Username.Contains(name) && b.Description.Contains(description) && b.Name.Contains(name)).ToList();
           
            return result;
        }

        /// <summary>
        /// Searches blueprints with name or description or author which contains specified strings.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="author"></param>
        /// <returns></returns>
        public IList<BlueprintSearchResult> SearchBlueprintsParams(string name, string description, string author)
        {
            //var result = _userDb.Blueprints.Where(b => b.User.Username.Contains(name) || b.Description.Contains(description) || b.Name.Contains(name)).Select().ToList();
            var query = from blueprint in _userDb.Blueprints
                         where
                             blueprint.Name.Contains(name) || blueprint.User.Username.Contains(author) ||
                             blueprint.Description.Contains(description)
                         select blueprint;

            var result = new List<BlueprintSearchResult>();
            foreach (var blueprint in query)
            {
                result.Add(new BlueprintSearchResult(name, description, author, blueprint));
            }
            return result.OrderBy(x => x.SearchScore).Reverse().ToList();
        }
    }
}
