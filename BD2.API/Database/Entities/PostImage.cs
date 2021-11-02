using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BD2.API.Database.Entities
{
    public class PostImage
    {
        public Guid PostId { get; set; }
        [JsonIgnore]
        public Post Post { get; set; }
        public Guid ImageId { get; set; }
        [JsonIgnore]
        public Image Image { get; set; }
    }

    public class PostImageConfig : IEntityTypeConfiguration<PostImage>
    {
        public void Configure(EntityTypeBuilder<PostImage> builder)
        {
            builder.HasKey(x => new { x.ImageId, x.PostId });

            builder.HasOne(x => x.Image)
                .WithMany()
                .HasForeignKey(x => x.ImageId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(x => x.Post)
                .WithMany(x => x.Images)
                .HasForeignKey(x => x.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
