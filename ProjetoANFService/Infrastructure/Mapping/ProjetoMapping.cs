using ProjetoANFService.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace ProjetoANFService.Infrastructure.Mapping
{
    public class ProjetoMapping
    {
        public class AuthorMap : EntityTypeConfiguration<Author>
        {
            public AuthorMap()
            {
                HasKey(p => p.Id);

                Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Author_Name") { IsUnique = true }));

                Property(p => p.CreatedBy)
                    .IsRequired();

                Property(p => p.CreatedDate)
                    .IsRequired();

                Property(p => p.ModifiedBy);
                Property(p => p.ModifiedDate);

                Property(p => p.RowVersion)
                    .IsConcurrencyToken();

                Property(p => p.IsActive)
                    .IsRequired();
            }
        }

        public class GenreMap : EntityTypeConfiguration<Genre>
        {
            public GenreMap()
            {
                HasKey(p => p.Id);

                Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Book_Name") { IsUnique = true }));

                Property(p => p.CreatedBy)
                    .IsRequired();

                Property(p => p.CreatedDate)
                    .IsRequired();

                Property(p => p.ModifiedBy);
                Property(p => p.ModifiedDate);

                Property(p => p.RowVersion)
                    .IsConcurrencyToken();

                Property(p => p.IsActive)
                    .IsRequired();
            }
        }

        public class BookMap : EntityTypeConfiguration<Book>
        {
            public BookMap()
            {
                HasKey(p => p.Id);

                Property(p => p.Title)
                    .IsRequired()
                    .HasMaxLength(80)
                    .HasColumnAnnotation("Index", new IndexAnnotation( new IndexAttribute("IX_Genre_Name") { IsUnique = true }));

                Property(p => p.PublicationDate);
                Property(p => p.Language);
                Property(p => p.Description);

                Property(p => p.CreatedBy)
                    .IsRequired();

                Property(p => p.CreatedDate)
                    .IsRequired();

                Property(p => p.ModifiedBy);
                Property(p => p.ModifiedDate);

                Property(p => p.RowVersion)
                    .IsConcurrencyToken();

                Property(p => p.IsActive)
                    .IsRequired();

                HasRequired(p => p.Genre);
                HasRequired(p => p.Author);
            }
        }
    }
}