using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReviewsAsArt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewsAsArt.Data.Mappers
{
    public class RegellijnConfiguration : IEntityTypeConfiguration<Regellijn>
    {
        public void Configure(EntityTypeBuilder<Regellijn> builder)
        {
            builder.ToTable("Regel");
            builder.HasKey(t => t.RegellijnID);
        }
    }
}
