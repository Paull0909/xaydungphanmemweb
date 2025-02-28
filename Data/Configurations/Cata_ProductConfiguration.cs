using Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class Cata_ProductConfiguration : IEntityTypeConfiguration<Variants_product>
    {
        public void Configure(EntityTypeBuilder<Variants_product> builder)
        {
            builder.ToTable("Variants_product");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.product_id).IsRequired();
            builder.Property(x=>x.Name).IsRequired();
            builder.HasOne(x=>x.Product).WithMany(x=>x.variants).HasForeignKey(x=>x.product_id);
            
        }
    }
}
