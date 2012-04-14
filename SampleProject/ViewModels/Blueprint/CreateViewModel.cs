using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SampleProject.ViewModels.Blueprint
{
    public class CreateViewModel
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

    }
}