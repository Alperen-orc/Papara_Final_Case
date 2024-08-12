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
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Price).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.Stock).IsRequired();
            builder.Property(x => x.PointPercentage).IsRequired().HasColumnType("decimal(5,2)");
            builder.Property(x => x.MaxPoint).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(x => x.InsertDate).IsRequired();

            builder.HasKey(p => p.Id);

            builder.HasMany(p => p.ProductCategories)
                   .WithOne(pc => pc.Product)
                   .HasForeignKey(pc => pc.ProductId);

            builder.HasMany(x => x.OrderDetails)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
