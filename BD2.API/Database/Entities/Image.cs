using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BD2.API.Database.Entities
{
    public class Image
    {
        public Guid Id { get; set; }
        public byte[] File { get; set; }
        public string MimeType { get; set; }
        public DateTime PostDate { get; set; }
        public Guid PostId { get; set; }
    }

    public class ImageConfig : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.Property(x => x.MimeType)
                .IsRequired(true)
                .HasMaxLength(100);

            builder
                .Property(x => x.PostDate)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("GETDATE()");
        }
    }

    public static class ImageMimeType
    {
        public static string BMP => "image/bmp";
        public static string GIF => "image/gif";
        public static string JPG => "image/jpeg";
        public static string JPEG => "image/jpeg";

        public static IEnumerable<string> GetImageTypes()
        {
            return typeof(ImageMimeType)
              .GetFields(BindingFlags.Public | BindingFlags.Static)
              .Where(f => f.FieldType == typeof(string))
              .Select(f => (string)f.GetValue(null))
              .AsEnumerable();
        }
    }
}
