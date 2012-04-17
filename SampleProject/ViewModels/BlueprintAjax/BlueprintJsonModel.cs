using System.ComponentModel.DataAnnotations;

namespace SampleProject.ViewModels.BlueprintAjax
{
    public class BlueprintJsonModel
    {
        public int id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string PreviewData { get; set; }

        public string JsonData{ get; set; }
    }
}