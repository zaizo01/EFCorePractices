using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreWebApi.Entities.Configurations
{
    public class MovieActorConfig : IEntityTypeConfiguration<MovieActor>
    {
        public void Configure(EntityTypeBuilder<MovieActor> modelBuilder)
        {
            modelBuilder.HasKey(prop =>
                new { prop.MovieId, prop.ActorId });

            modelBuilder.Property(prop => prop.Character)
              .HasMaxLength(150);
        }
    }
}
