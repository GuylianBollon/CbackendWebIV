using Microsoft.EntityFrameworkCore;
using ReviewsAsArt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewsAsArt.Data.Mappers
{
    public class BerichtConfiguration : IEntityTypeConfiguration<Bericht>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Bericht> builder)
        {
            builder.ToTable("Bericht");
            builder.HasKey(t => t.Id);
        }
    }
}
