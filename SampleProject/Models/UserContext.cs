using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SampleProject.Models.Auth;

namespace SampleProject.Models
{
    /// <summary>
    /// DbContext class for dealing with users.
    /// </summary>
    public class UserContext : DbContext
    {
        // Change the string if you want you database to have a custom name.
        public UserContext()
            : base("MvcOpenID")
        { }

        // We have separate tables for users and OpenIds as one user can have multiple OpenIds
        public DbSet<User> Users { get; set; }
        public DbSet<OpenId> OpenIds { get; set; }
    }
}