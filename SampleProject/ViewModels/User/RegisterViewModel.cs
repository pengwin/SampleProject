using System.ComponentModel.DataAnnotations;

namespace SampleProject.ViewModels.User
{
    public class RegisterViewModel
    {
        [Required]
        [MinLength(6)]
        public string Username { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        [Required]
        public string OpenIdIdentifier { get; set; }

        public string ReturnUrl { get; set; }

        public string OpenIdProviderName { get; set; }
    }
}