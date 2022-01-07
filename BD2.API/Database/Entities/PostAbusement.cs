using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BD2.API.Database.Entities
{
    public class PostAbusement
    {
        [JsonIgnore]
        public Account Account { get; set; }
        public Guid AccountId { get; set; }

        [JsonIgnore]
        public Post Post { get; set; }
        public Guid PostId { get; set; }

        public string Text { get; set; }
        public DateTime AbusementDate { get; set; }

        [JsonIgnore]
        public Account CheckedBy { get; set; }
        public Guid? CheckedById { get; set; }
        public DateTime? CheckedDate { get; set; }
        public bool? Status { get; set; }
    }

    public class AbusementConfig : IEntityTypeConfiguration<PostAbusement>
    {
        public void Configure(EntityTypeBuilder<PostAbusement> builder)
        {
            builder.HasKey(x => new { x.AccountId, x.PostId });

            builder.HasOne(x => x.Account)
                .WithMany()
                .HasForeignKey(x => x.AccountId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(x => x.Post)
                .WithMany(x => x.Abusements)
                .HasForeignKey(x => x.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.AbusementDate)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(x => x.Text)
                .HasMaxLength(1000)
                .IsRequired(true);

            builder.HasOne(x => x.CheckedBy)
                .WithMany()
                .HasForeignKey(x => x.CheckedById)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
