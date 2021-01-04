using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Pacco.Services.Availability.Infrastructure.EfCore.Entities.Configurations
{
    internal static class ResourceEntityConfiguration
    {
        public static void Build(ModelBuilder builder)
        {
            builder.Entity<ResourceEntity>(b =>
            {
                b.Property(p => p.Id)
                 .IsRequired();

                b.Property(p => p.Tags)
                .HasColumnType("varchar")
                .HasMaxLength(50)
                 .IsRequired();

                b.Property(p => p.Version)
                 .IsRequired();

                b.Property(p => p.Tags)
                    .HasConversion(
                        v => JsonConvert.SerializeObject(v),
                        v => JsonConvert.DeserializeObject<IEnumerable<string>>(v));

                //b.Property(p => p.Reserv)
                // .IsRequired();

                b.HasMany(r => r.Reservations)
                    .WithOne()
                    .HasForeignKey(rsv => rsv.ResourceId);

                //b.HasMany(c => c.Reserv)
                // .WithOne(e => e.Resou);

                b.HasKey(k => k.Id);
            });
        }
    }
}