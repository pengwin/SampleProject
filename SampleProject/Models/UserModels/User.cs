using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SampleProject.Models.BlueprintModels;

namespace SampleProject.Models.UserModels
{
    /// <summary>
    /// Entity class for storing users
    /// </summary>
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MinLength(6)]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        public string FullName { get; set; }

        public virtual ICollection<OpenId> OpenIds { get; set; }

        public virtual ICollection<Role> Roles { get; set; }

        public virtual ICollection<Blueprint> Blueprints { get; set; }
    }
}