using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SampleProject.Models.UserModels;

namespace SampleProject.Models.BlueprintModels
{

    public class Canvas
    {
        [Key]
        public int CanvasId { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int GridStep { get; set; }

        //public int BlueprintId { get; set; }
        [Required]
        public Blueprint Blueprint { get; set; }

    }

    public class Rectangle
    {
        [Key]
        public int RectangleId { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        [Required]
        public Blueprint Blueprint { get; set; }

    }

    public class Ellipse
    {
        [Key]
        public int EllipseId { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int VerticalRadius { get; set; }

        public int HorisontalRadius { get; set; }

        [Required]
        public Blueprint Blueprint { get; set; }

    }

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

        public Canvas Canvas { get; set; }

        public virtual ICollection<Rectangle> Rectangles { get; set; }

        public virtual ICollection<Ellipse> Ellipses { get; set; }


    }
}