using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Database.Entities
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid AuthoId { get; set; }
        public Author Author { get; set; }
    }

    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEWSEQUENTIALID ()");
            builder.HasOne(x => x.Author)
                .WithMany(x => x.Books)
                .HasForeignKey(x => x.AuthoId);
        }
    }
}
