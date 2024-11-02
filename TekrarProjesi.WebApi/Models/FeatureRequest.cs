using System.ComponentModel.DataAnnotations;

namespace TekrarProjesi.WebApi.Models
{
    public class FeatureRequest
    {
        [Required]
        
        public string Title { get; set; }
    }
}
