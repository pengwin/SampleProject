using System.ComponentModel.DataAnnotations;

namespace SampleProject.Models
{
    /// <summary>
    /// Entity class for storing user roles
    /// </summary>
    public class UserRole
    {
        [Key]
        public int UserRoleId { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}