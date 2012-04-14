using System.Collections.Generic;
using SampleProject.Models.BlueprintModels;

namespace SampleProject.Models.Repositories
{
    public interface IBlueprintRepository
    {
        /// <summary>
        /// Save the changes to the database.
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Gets the blueprint by id.
        /// </summary>
        /// <param name="id">id of the blueprint.</param>
        /// <returns>blueprint with a specific id.</returns>
        Blueprint GetBlueprintById(int id);

        /// <summary>
        /// Gets the blueprint by author id.
        /// </summary>
        /// <param name="id">id of the author of blueprint.</param>
        /// <returns>blueprint with a specific author.</returns>
        Blueprint GetBlueprintByUserId(int userId);

        /// <summary>
        /// Gets all existing blueprints.
        /// </summary>
        /// <returns>the list of blueprints</returns>
        IList<Blueprint> GetAllBlueprints();

        /// <summary>
        /// Gets all existing blueprints with the specific author.
        /// </summary>
        /// <returns>the list of blueprints</returns>
        IList<Blueprint> GetAllBlueprintsByUserId(int userId);

        /// <summary>
        /// Gets blueprints quantity.
        /// </summary>
        /// <returns></returns>
        int GetBlueprintsCount();

        /// <summary>
        /// Creates a blueprint in database for the specific user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="blueprint"></param>
        void CreateBlueprintForUser(int userId, Blueprint blueprint);

        /// <summary>
        /// Removes the blueprint from the database.
        /// </summary>
        /// <param name="blueprint">Blueprint to be removed.</param>
        void RemoveBlueprint(Blueprint blueprint);
    }
}