using Microsoft.EntityFrameworkCore;
using TekrarProjesi.Data.Entities;

namespace TekrarProjesi.Data.Context
{
    public class TekrarAppDbContext  : DbContext
    {
        public TekrarAppDbContext(DbContextOptions<TekrarAppDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FeatureConfiguration());
            modelBuilder.ApplyConfiguration(new HotelConfiguration());
            modelBuilder.ApplyConfiguration(new HotelFeatureConfiguration());
            modelBuilder.ApplyConfiguration(new ReservationConfiguration());
            modelBuilder.ApplyConfiguration(new RoomConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            base.OnModelCreating(modelBuilder);

        }

        public DbSet<FeatureEntity> Features => Set<FeatureEntity>();
        public DbSet<HotelEntity> Hotels => Set<HotelEntity>();
        public DbSet<HotelFeatureEntity> HotelFeatures => Set<HotelFeatureEntity>();
        public DbSet<ReservationEntity> Reservations => Set<ReservationEntity>();
        public DbSet<RoomEntity> Rooms => Set<RoomEntity>();
        public DbSet<UserEntity> Users => Set<UserEntity>();

    }
}
