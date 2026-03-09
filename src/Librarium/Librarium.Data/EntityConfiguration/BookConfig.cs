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

            builder.Property(x => x.IsRetired)
                .IsRequired()
                .HasDefaultValue(false);

            builder.HasMany(x => x.Authors)
                .WithMany(x => x.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BookAuthors",
                    right => right
                        .HasOne<Author>()
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade),
                    left => left
                        .HasOne<Book>()
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade),
                    join =>
                    {
                        join.ToTable("BookAuthors");
                        join.HasKey("BookId", "AuthorId");
                    });
        }
    }
}