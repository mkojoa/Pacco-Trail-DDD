using Microsoft.EntityFrameworkCore;

namespace Pacco.Services.Availability.Infrastructure.EfCore.Entities.Configurations
{
    public static class ReservationEntityConfiguration
    {
        public static void Build(ModelBuilder builder)
        {
            builder.Entity<ReservationEntity>(b =>
            {
                b.Property(p => p.Id)
                 .IsRequired();

                b.Property(p => p.DateTime)
                 .IsRequired();

                b.Property(p => p.Priority)
                 .IsRequired();

                //b.HasOne(e => e.Resou)
                // .WithMany(c => c.Reserv);

                b.HasKey(k => k.Id);
            });
        }
    }
}