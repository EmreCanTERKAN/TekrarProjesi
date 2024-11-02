using System.ComponentModel.DataAnnotations;
using TekrarProjesi.Data.Enums;

namespace TekrarProjesi.WebApi.Models
{
    public class AddHotelRequest
    {
        [Required]
        public string Name { get; set; }
        public int? Stars { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public AccomodationType AccomodationType { get; set; }
        [Required]
        public List<int> FeatureId { get; set; }
    }
}
