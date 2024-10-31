using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TekrarProjesi.Data.Entities
{
    public class HotelFeatureEntity : BaseEntity
    {
        public int HotelId { get; set; }
        public int FeatureId { get; set; }

        // Relational

        public HotelEntity Hotel { get; set; }
        public FeatureEntity Feature { get; set; }
    }

    public class HotelFeatureConfiguration : BaseConfiguration<HotelFeatureEntity>
    {
        public override void Configure(EntityTypeBuilder<HotelFeatureEntity> builder)
        {
            builder.Ignore(x => x.Id);
            builder.HasKey("HotelId", "FeatureId");
            base.Configure(builder);
        }
    }
}
