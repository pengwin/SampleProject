using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SampleProject.Models.BlueprintModels;
using SampleProject.Models.UserModels;

namespace SampleProject.Models
{
    /// <summary>
    /// DbContext class for dealing with users.
    /// </summary>
    public class UserContext : DbContext
    {
        // Change the string if you want you database to have a custom name.
        public UserContext()
            : base("SampleProject")
        { }

        // We have separate tables for users and OpenIds as one user can have multiple OpenIds
        public DbSet<User> Users { get; set; }
        public DbSet<OpenId> OpenIds { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Blueprint> Blueprints { get; set; }
    }
}