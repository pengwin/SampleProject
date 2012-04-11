using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SampleProject.Models.UserModels
{
    /// <summary>
    /// Entity class for storing user roles.
    /// </summary>
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        public string RoleName { get; set; }

        public virtual ICollection<User> Users { get; set; }
        
        public Role()
        {
            Users = new List<User>();
        }

    }
}