using Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Amount).HasPrecision(18, 0).IsRequired();
            builder.Property(x => x.Fee).HasPrecision(18,0).IsRequired();
            builder.HasOne(x => x.User).WithMany(x => x.Transactions).HasForeignKey(x => x.UserId);
        }
    }
}
