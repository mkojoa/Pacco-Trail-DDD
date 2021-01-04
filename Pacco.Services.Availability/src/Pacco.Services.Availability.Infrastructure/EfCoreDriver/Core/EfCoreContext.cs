using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pacco.Services.Availability.Infrastructure.EfCore.Entities;
using Pacco.Services.Availability.Infrastructure.EfCore.Entities.Configurations;
using System.Collections.Generic;

namespace Pacco.Services.Availability.Infrastructure.EfCoreDriver.Core
{
    public class EfCoreContext : DbContext
    {
        public EfCoreContext(DbContextOptions<EfCoreContext> options) : base(options)
        {
            //if (!Database.CanConnect())
            //{
            //    Database.EnsureCreated();
            //}
        }
        

        public DbSet<ResourceEntity> ResourceEntity { get; set; } 
        public DbSet<ReservationEntity> ReservationEntity { get; set; } 
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            ReservationEntityConfiguration.Build(builder);
            ResourceEntityConfiguration.Build(builder);

            builder.Entity<ResourceEntity>().HasMany(city => city.Reservations)
              .WithOne()
               .HasForeignKey(con => con.ResourceId);
           // .IsRequired().HasForeignKey(con => con.ResourceId);
        }
    }
}