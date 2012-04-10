using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SampleProject.Models.Auth;

namespace SampleProject.Models
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
    }
}