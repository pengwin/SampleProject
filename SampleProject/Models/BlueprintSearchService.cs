using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SampleProject.Models.BlueprintModels;

namespace SampleProject.Models
{
    /// <summary>
    /// Result of the text search in parameter.
    /// </summary>
    public class SearchResult
    {
        /// <summary>
        /// Found parameter value
        /// </summary>
        public string Value { get; private set; }

        private string _pattern;
        /// <summary>
        /// Pattern which has been found.
        /// </summary>
        public string Pattern { get; private set; }

        /// <summary>
        /// How many times pattern occurred in string.
        /// </summary>
        public int MatchesCount { get; private set; }

        public SearchResult(string value,string pattern)
        {
            Value = value;
            Pattern = pattern;

            if (pattern != null && value != null)
            {
                MatchesCount = Regex.Matches(value, pattern,RegexOptions.IgnoreCase).Count;
            }
            else
            {
                MatchesCount = 0;
            }
            
        }

    }
    public class BlueprintSearchResult
    {
        public int BlueprintId { get; set; }

        public SearchResult NameResult { get; private set; }

        public SearchResult DescriptionResult { get; private set; }

        public SearchResult AuthorResult { get; private set; }

        public int SearchScore { get; private set; }

        public BlueprintSearchResult(string namePattern,string descrPattern,string authorPattern,Blueprint blueprint)
        {
            BlueprintId = blueprint.BlueprintId;
            NameResult = new SearchResult(blueprint.Name,namePattern);
            AuthorResult = new SearchResult(blueprint.User.Username, authorPattern);
            DescriptionResult = new SearchResult(blueprint.Description,descrPattern);
            SearchScore = NameResult.MatchesCount + DescriptionResult.MatchesCount + AuthorResult.MatchesCount;
        }
    }

    public class BlueprintSearchService
    {
         #region Private fields

        private readonly UserContext _userDb;

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="userDb">UserContext.</param>
        public BlueprintSearchService(UserContext userDb)
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
