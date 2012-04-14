using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace SampleProject.HtmlHelpers
{
    public static class HighlightPatternExtension
    {
        /// <summary>
        /// Encloses each pattern substring in the target string with highlight tag.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="target"></param>
        /// <param name="pattern"></param>
        /// <param name="highlightTag"></param>
        /// <returns></returns>
        public static MvcHtmlString HighlightPattern(this HtmlHelper helper, string target, string pattern, string highlightTag)
        {
            // This is a dirty code which encloses with tags every pattern match .
            // And in the same time save the original case of the pattern match.
            // I know it can be achieved with one regular expression. But I don't know this expression.

            var openTag = String.Format("<{0}>", highlightTag);
            var closeTag = String.Format("</{0}>", highlightTag);
            var builder = new StringBuilder(target);

            if (!string.IsNullOrEmpty(pattern))
            {

                var match = Regex.Match(target, pattern, RegexOptions.IgnoreCase);

                int count = 0;
                while (match.Success)
                {
                    builder.Insert(match.Index + count, openTag);
                    count += openTag.Length;
                    builder.Insert(match.Index + match.Length + count, closeTag);
                    count += closeTag.Length;
                    match = match.NextMatch();
                }
            }
            return new MvcHtmlString(builder.ToString());
        }
    }

}