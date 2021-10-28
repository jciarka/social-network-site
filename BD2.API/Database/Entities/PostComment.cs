using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BD2.API.Database.Entities
{
    public class PostComment
    {
        [JsonIgnore]
        public Account Account { get; set; }
        public Guid AccountId { get; set; }

        [JsonIgnore]
        public Post Post { get; set; }
        public Guid PostId { get; set; }


        public string Text { get; set; }
        public DateTime CommentDate { get; set; }
    }

    public class CommentConfig : IEntityTypeConfiguration<PostComment>
    {
        public void Configure(EntityTypeBuilder<PostComment> builder)
        {
            builder.HasKey(x => new { x.AccountId, x.PostId });

            builder.HasOne(x => x.Account)
                .WithMany()
                .HasForeignKey(x => x.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(x => x.Post)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            
            builder.Property(x => x.CommentDate)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
