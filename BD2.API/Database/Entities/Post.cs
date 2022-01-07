using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BD2.API.Database.Entities
{
    public class Post
    {
        public Guid Id { get; set; }

        // basic post data
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime PostDate { get; set; }

        // counts - denormalisation
        public int ViewsCounts { get; set; }
        public int CommentsCount { get; set; }
        public int PositiveReactionsCount { get; set; }
        public int NegativeReactionCount { get; set; }

        [JsonIgnore]
        public ICollection<PostComment> Comments { get; set; }
        [JsonIgnore]
        public ICollection<PostImage> Images { get; set; }
        [JsonIgnore]
        public ICollection<PostView> Views { get; set; }
        [JsonIgnore]
        public ICollection<PostReaction> Reactions { get; set; }

        public DateTime? LastCommentDate { get; set; }
        public DateTime? LastReactionDate { get; set; }
        public DateTime? LastOwnerViewDate { get; set; }

        // Account
        public Guid OwnerId { get; set; }
        [JsonIgnore]
        public Account Owner { get; set; }

        // Post 
        public Guid? GroupId { get; set; }
        [JsonIgnore]
        public Group Group { get; set; }
    }

    public class PostConfig : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEWSEQUENTIALID ()");

            builder.Property(x => x.PostDate)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasOne(x => x.Owner)
                .WithMany()
                .HasForeignKey(x => x.OwnerId);

            builder.HasOne(x => x.Group)
                .WithMany(x => x.Posts)
                .HasForeignKey(x => x.GroupId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
