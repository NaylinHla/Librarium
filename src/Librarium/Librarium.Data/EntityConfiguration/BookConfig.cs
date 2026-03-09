using Librarium.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Librarium.Data.EntityConfiguration
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Isbn)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.PublicationYear)
                .IsRequired();
        }
    }
}