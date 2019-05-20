using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReviewsAsArt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewsAsArt.Data.Mappers
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Reviews");
            builder.HasKey(t => t.ReviewId);
            builder.HasOne(t => t.Rg).WithMany().OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(t => t.Werk).WithMany().OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(t => t.Commentaars).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
