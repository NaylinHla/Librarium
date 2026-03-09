using Librarium.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Librarium.Data.EntityConfiguration
{
    public class MemberConfig : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.ToTable("Members");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(200);
        }
    }
}