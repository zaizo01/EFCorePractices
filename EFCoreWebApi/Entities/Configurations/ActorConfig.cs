using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreWebApi.Entities.Configurations
{
    public class ActorConfig : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> modelBuilder)
        {
            modelBuilder.Property(prop => prop.Name)
              .HasMaxLength(150)
              .IsRequired();

            modelBuilder.Property(prop => prop.DateOfBirth)
                .HasColumnType("date");
        }
    }
}
