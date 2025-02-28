using Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class TotalRevenueConfiguration : IEntityTypeConfiguration<TotalRevenue>
    {
        public void Configure(EntityTypeBuilder<TotalRevenue> builder)
        {
            builder.ToTable("TotalRevenues");
            builder.HasKey(x => x.doanhthu_id);
            builder.Property(x => x.doanhthu_id).UseIdentityColumn();
            builder.Property(x => x.date).IsRequired();
            builder.Property(x => x.tongdoanhthu).HasPrecision(18, 0).IsRequired();
        }
    }
}
