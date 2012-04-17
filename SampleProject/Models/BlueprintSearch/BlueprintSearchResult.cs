using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SampleProject.Models.BlueprintModels;

namespace SampleProject.Models.BlueprintSearch
{
    /// <summary>
    /// Blueprint search result entity.
    /// Stores query patterns and results;
    /// </summary>
    public class BlueprintSearchResult
    {
        public int BlueprintId { get; set; }

        public SearchResult NameResult { get; private set; }

        public SearchResult DescriptionResult { get; private set; }

        public SearchResult AuthorResult { get; private set; }

        /// <summary>
        /// Value to sort results.
        /// </summary>
        public int SearchScore { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="namePattern"></param>
        /// <param name="descrPattern"></param>
        /// <param name="authorPattern"></param>
        /// <param name="blueprint"></param>
        public BlueprintSearchResult(string namePattern, string descrPattern, string authorPattern, Blueprint blueprint)
        {
            BlueprintId = blueprint.BlueprintId;
            NameResult = new SearchResult(blueprint.Name, namePattern);
            AuthorResult = new SearchResult(blueprint.User.Username, authorPattern);
            DescriptionResult = new SearchResult(blueprint.Description, descrPattern);
            SearchScore = NameResult.MatchesCount + DescriptionResult.MatchesCount + AuthorResult.MatchesCount;
        }
    }
}