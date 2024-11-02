using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekrarProjesi.Data.Enums;

namespace TekrarProjesi.Business.Operations.Hotel.Dtos
{
    public class HotelDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Stars  { get; set; }
        public string Location { get; set; }
        public AccomodationType AccomodationType { get; set; }
        public List<HotelFeaturesDto> FeaturesDtos { get; set; }
    }
}
