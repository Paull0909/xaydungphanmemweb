using Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");
            builder.HasKey(x => x.product_id);
            builder.Property(x => x.product_id).UseIdentityColumn();
            builder.Property(x => x.product_name).IsRequired();
            builder.Property(x => x.discount).IsRequired();
            builder.Property(x => x.status).IsRequired();
            builder.Property(x => x.price).HasPrecision(18, 2).IsRequired();
            builder.HasOne(x => x.Category).WithMany(x => x.Products)
                .HasForeignKey(x => x.type_id);
           builder.HasOne(x=>x.advertisement).WithMany(x=>x.products).HasForeignKey(x => x.advertisement_id);
        }
    }
}
