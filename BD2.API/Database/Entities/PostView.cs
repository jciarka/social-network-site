using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BD2.API.Database.Entities
{
    public class PostView
    {
        [JsonIgnore]
        public Account Account { get; set; }
        public Guid AccountId { get; set; }

        [JsonIgnore]
        public Post Post { get; set; }
        public Guid PostId { get; set; }

        public DateTime ViewDate { get; set; }
    }

    public class PostViewConfig : IEntityTypeConfiguration<PostView>
    {
        public void Configure(EntityTypeBuilder<PostView> builder)
        {
            builder.HasKey(x => new { x.AccountId, x.PostId });

            builder.HasOne(x => x.Account)
                .WithMany()
                .HasForeignKey(x => x.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(x => x.Post)
                .WithMany(x => x.Views)
                .HasForeignKey(x => x.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Property(x => x.ViewDate)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("GETDATE()");
        }
    }
}

