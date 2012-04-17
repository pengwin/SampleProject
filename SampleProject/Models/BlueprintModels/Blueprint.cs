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

        /// <summary>
        /// SVG picture
        /// </summary>
        public virtual string VectorPreview { get; set; }

        /// <summary>
        /// Stringified json data for editor application.
        /// </summary>
        public virtual string JsonData { get; set; }

    }
}