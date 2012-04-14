using System.Collections.Generic;
using SampleProject.Models.BlueprintModels;

namespace SampleProject.Models.BlueprintSearch
{
    public interface IBlueprintSearchService
    {
        /// <summary>
        /// Searches blueprints with name or description or author which contains specified strings.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="author"></param>
        /// <returns></returns>
        IList<Blueprint> FindBlueprintsContain(string name, string description, string author);

        /// <summary>
        /// Searches blueprints with name or description or author which contains specified strings.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="author"></param>
        /// <returns></returns>
        IList<BlueprintSearchResult> SearchBlueprintsParams(string name, string description, string author);
    }
}