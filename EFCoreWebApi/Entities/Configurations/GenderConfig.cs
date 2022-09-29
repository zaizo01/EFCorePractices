using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreWebApi.Entities.Configurations
{
    public class GenderConfig : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> modelBuilder)
        {
            modelBuilder.HasKey(prop => prop.Identificador);

            modelBuilder.Property(prop => prop.Name)
                .HasMaxLength(150)
                .IsRequired();
            //.HasColumnName("NombreGenero")

            //modelBuilder.Entity<Gender>().ToTable(name: "TablaGenero", schema: "Peliculas");
        }
    }
}
