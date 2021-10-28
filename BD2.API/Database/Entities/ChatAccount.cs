using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BD2.API.Database.Entities
{
    public class ChatAccount
    {
        public Guid AccountId { get; set; }

        [JsonIgnore]
        public Account Account { get; set; }
        public Guid ChatId { get; set; }
        [JsonIgnore]
        public Chat Chat { get; set; }

        public bool IsAdmin { get; set; }
        public DateTime? LastViewDate { get; set; }
    }

    public class ChatAccountConfig : IEntityTypeConfiguration<ChatAccount>
    {
        public void Configure(EntityTypeBuilder<ChatAccount> builder)
        {
            builder.HasKey(x => new { x.ChatId, x.AccountId });

            builder.HasOne(x => x.Chat)
                .WithMany()
                .HasForeignKey(x => x.ChatId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(x => x.Account)
                .WithMany()
                .HasForeignKey(x => x.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Property(x => x.IsAdmin).HasDefaultValue(false);
        }
    }
}
