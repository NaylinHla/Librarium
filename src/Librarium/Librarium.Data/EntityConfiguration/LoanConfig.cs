using Librarium.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Librarium.Data.EntityConfiguration
{
    public class LoanConfig : IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder)
        {
            builder.ToTable("Loans");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.LoanDate)
                .IsRequired();

            builder.Property(x => x.ReturnDate)
                .IsRequired(false);

            builder.HasOne(x => x.Book)
                .WithMany(x => x.Loans)
                .HasForeignKey(x => x.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Member)
                .WithMany(x => x.Loans)
                .HasForeignKey(x => x.MemberId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}