using SampleProject.Authentication.Models;
using SampleProject.Models.UserModels;

namespace SampleProject.Authentication
{
    public interface IUserAuthService
    {
        /// <summary>
        /// Gets current user info from HttpContext.Current.User
        /// </summary>
        /// <returns>null if user doesn't exist</returns>
        UserInfo GetCurrentUserInfo();

        /// <summary>
        /// Sets current user info in HttpContext.Current.User
        /// </summary>
        /// <returns></returns>
        void SetCurrentUserInfo();

        /// <summary>
        /// Sets authentication cookie with auth ticket for the specific user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        void LoginUser(User user);

        /// <summary>
        /// Logout the current user.
        /// </summary>
        void Logout();
    }
}