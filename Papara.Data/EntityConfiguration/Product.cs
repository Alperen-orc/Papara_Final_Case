using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Papara.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.Data.EntityConfiguration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.Description).IsRequired(true).HasMaxLength(200);
            builder.Property(x => x.Price).IsRequired(true).HasColumnType("decimal(18,2)");
            builder.Property(x => x.IsActive).IsRequired(true);
            builder.Property(x => x.Stock).IsRequired(true);
            builder.Property(x => x.PointPercentage).IsRequired(true).HasColumnType("decimal(5,2)");
            builder.Property(x => x.MaxPoint).IsRequired(true).HasColumnType("decimal(18,2)");
            builder.Property(x => x.InsertDate).IsRequired(true);

            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Category)
                   .WithMany(c => c.Products)
                   .HasForeignKey(p => p.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.OrderDetails)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
