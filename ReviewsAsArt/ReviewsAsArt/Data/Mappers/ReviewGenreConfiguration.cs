using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReviewsAsArt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewsAsArt.Data.Mappers
{
    public class ReviewGenreConfiguration : IEntityTypeConfiguration<Reviewgenre>
    {
        public void Configure(EntityTypeBuilder<Reviewgenre> builder)
        {
            builder.ToTable("Reviewgenre");
            builder.HasKey(t=>t.Titel);
            builder.HasMany(t => t.Regels).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(t => t.GeblokkeerdeUserIDs).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
