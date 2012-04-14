using SampleProject.Models.UserModels;

namespace SampleProject.Authentication
{
    public interface IApiKeyStore
    {
        /// <summary>
        /// Validates the specified api key.
        /// </summary>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        bool IsValidApiKey(string apiKey);

        /// <summary>
        /// Gets user who has this api key.
        /// </summary>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        User GetApiKeyOwner(string apiKey);

    }
}