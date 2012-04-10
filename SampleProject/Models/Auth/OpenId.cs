using System.ComponentModel.DataAnnotations;

namespace SampleProject.Models.Auth
{
    /// <summary>
    /// Entity class for storing OpenIDs.
    /// </summary>
    public class OpenId
    {
        [Key]
        public int OpenIdId { get; set; }

        private string _openIdUrl;
        [Required]
        public string OpenIdUrl
        {
            get { return _openIdUrl; }
            set
            {
                _openIdUrl = value;
                ProviderName = GuessOpenIdFancyName(_openIdUrl);
            }
        }

        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// OpenId provider fancy name
        /// </summary>
        [NotMapped]
        public string ProviderName { get; private set; }

        public virtual User User { get; set; }

        /// <summary>
        /// Try to define openid provider name (like: google, yandex, blogger etc.) with
        /// </summary>
        /// <param name="openIdUrl"></param>
        /// <returns></returns>
        public static string GuessOpenIdFancyName(string openIdUrl)
        {
            if (openIdUrl.IndexOf("google.com/accounts/o8/id") > -1)
            {
                return "google";
            }
            if (openIdUrl.IndexOf("openid.yandex.ru") > -1)
            {
                return "yandex";
            }
            if (openIdUrl.IndexOf("myopenid.com") > -1)
            {
                return "myopenid";
            }

            return "";
        }
    }
}