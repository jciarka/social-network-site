using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BD2.API.Database.Entities
{
    public class PostReaction
    {
        [JsonIgnore]
        public Account Account { get; set; }
        public Guid AccountId { get; set; }

        [JsonIgnore]
        public Post Post { get; set; }
        public Guid PostId { get; set; }

        public DateTime ReactionDate { get; set; }
        public ReactionType Type { get; set; }
    }

    public class ReactionConfig : IEntityTypeConfiguration<PostReaction>
    {
        public void Configure(EntityTypeBuilder<PostReaction> builder)
        {
            builder.HasKey(x => new { x.AccountId, x.PostId });

            builder.HasOne(x => x.Account)
                .WithMany()
                .HasForeignKey(x => x.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(x => x.Post)
                .WithMany(x => x.Reactions)
                .HasForeignKey(x => x.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Property(x => x.ReactionDate)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("GETDATE()");
        }
    }

        public enum ReactionType
    {
        Dislike = 1,
        Like = 2,
    }
}
