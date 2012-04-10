namespace SampleProject.ViewModels.User
{
    /// <summary>
    /// List of available login methods.
    /// </summary>
    public enum LoginMethod
    {
        Google,Yandex,OpenId
    }

    /// <summary>
    /// Transfers data from the login from.
    /// </summary>
    public class LoginViewData
    {
        /// <summary>
        /// OpenId identifier from login form.
        /// </summary>
        public string OpenIdUrl { get; set; }

        /// <summary>
        /// Login method which is chosen in login form.
        /// </summary>
        public LoginMethod LoginMethod { get; set; }

        /// <summary>
        /// Optional return url.
        /// </summary>
        public string ReturnUrl { get; set; }
    }
}