using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreWebApi.Entities.Configurations
{
    public class CinemaRoomConfig : IEntityTypeConfiguration<CinemaRoom>
    {
        public void Configure(EntityTypeBuilder<CinemaRoom> modelBuilder)
        {
            modelBuilder.Property(prop => prop.Price)
             .HasPrecision(precision: 9, scale: 2);

            modelBuilder.Property(prop => prop.TypeOfCinemaRoom)
               .HasDefaultValue(TypeOfCinemaRoom.TwoDimensions);
        }
    }
}
