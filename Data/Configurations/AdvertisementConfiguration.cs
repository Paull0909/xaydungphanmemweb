using Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class AdvertisementConfiguration : IEntityTypeConfiguration<Advertisement>
    {
        public void Configure(EntityTypeBuilder<Advertisement> builder)
        {
            builder.ToTable("Advertisement");
            builder.HasKey(x => x.id);
            builder.Property(x => x.content).IsRequired();
            builder.Property(x => x.status).IsRequired();
            builder.Property(x => x.title).IsRequired();
            builder.Property(x => x.content).IsRequired();
            builder.Property(x => x.img_path).IsRequired();

        }
    }
}
