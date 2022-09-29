using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreWebApi.Entities.Configurations
{
    public class MovieConfig : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> modelBuilder)
        {
            modelBuilder.Property(prop => prop.Title)
            .HasMaxLength(250)
            .IsRequired();

            modelBuilder.Property(prop => prop.PremiereDate)
                .HasColumnType("date");

            modelBuilder.Property(prop => prop.PosterURL)
                .HasMaxLength(500)
                .IsUnicode(false);
        }
    }
}
