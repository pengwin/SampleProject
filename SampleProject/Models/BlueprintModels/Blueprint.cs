using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SampleProject.Models.UserModels;

namespace SampleProject.Models.BlueprintModels
{
    /// <summary>
    /// Entity class for storing blueprints.
    /// </summary>
    public class Blueprint
    {
        [Key]
        public int BlueprintId { get; set; }

        [Required]
        [MinLength(4)]
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Changed { get; set; }

        [Required]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public virtual string VectorPreview { get; set; }

        public virtual string JsonData { get; set; }

    }
}