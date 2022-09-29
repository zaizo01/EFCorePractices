using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreWebApi.Entities.Configurations
{
    public class CinemaOfferConfig : IEntityTypeConfiguration<CinemaOffer>
    {
        public void Configure(EntityTypeBuilder<CinemaOffer> modelBuilder)
        {
            modelBuilder.Property(prop => prop.PercentageDiscount)
              .HasPrecision(precision: 5, scale: 2);

            modelBuilder.Property(prop => prop.StartDate)
               .HasColumnType("date");

            modelBuilder.Property(prop => prop.EndDate)
               .HasColumnType("date");
        }
    }
}
