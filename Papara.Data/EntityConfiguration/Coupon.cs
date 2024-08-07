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
    public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
    {
        public void Configure(EntityTypeBuilder<Coupon> builder)
        {
            builder.Property(x => x.Code).IsRequired(true).HasMaxLength(10);
            builder.Property(x => x.Amount).IsRequired(true).HasColumnType("decimal(18,2)");
            builder.Property(x => x.IsActive).IsRequired(true);
            builder.Property(x => x.ExpiryDate).IsRequired(true);
            builder.Property(x => x.InsertDate).IsRequired(true);

            builder.HasMany(x => x.UserCoupons)
                .WithOne(x => x.Coupon)
                .HasForeignKey(x => x.CouponId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
