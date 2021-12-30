using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BD2.API.Database.Entities
{
    public class Chat
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? LastPostDate { get; set; }

        [JsonIgnore]
        public ICollection<ChatAccount> Members { get; set; }

    }

    public class ChatConfig : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEWSEQUENTIALID ()");

            builder.Property(x => x.Name).HasMaxLength(200);
        }
    }           
}
