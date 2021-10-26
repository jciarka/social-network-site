using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BD2.API.Database.Entities
{
    public class ChatEntry
    {
        public Guid Id { get; set; }

        public Guid AccountId { get; set; }
        [JsonIgnore]
        public Account Account { get; set; }
        public Guid ChatId { get; set; }
        [JsonIgnore]
        public Chat Chat { get; set; }

        public DateTime PostDate { get; set; }
        public string Text { get; set; }
    }

    public class ChatEntryConfig : IEntityTypeConfiguration<ChatEntry>
    {
        public void Configure(EntityTypeBuilder<ChatEntry> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEWSEQUENTIALID ()");

            builder.HasOne(x => x.Chat)
                .WithMany()
                .HasForeignKey(x => x.ChatId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(x => x.Account)
                .WithMany()
                .HasForeignKey(x => x.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Property(x => x.PostDate)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("GETDATE()");
        }
    }
    

}
