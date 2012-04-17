using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace SampleProject.Models.BlueprintSearch
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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">parameter value</param>
        /// <param name="pattern">pattern for searching</param>
        public SearchResult(string value, string pattern)
        {
            Value = value;
            Pattern = pattern;

            if (pattern != null && value != null)
            {
                MatchesCount = Regex.Matches(value, pattern, RegexOptions.IgnoreCase).Count;
            }
            else
            {
                MatchesCount = 0;
            }

        }

    }
}