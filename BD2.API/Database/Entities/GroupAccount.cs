using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BD2.API.Database.Entities
{
    public class GroupAccount
    {
        public Guid AccountId { get; set; }
        [JsonIgnore]
        public Account Account { get; set; }

        public Guid GroupId { get; set; }
        [JsonIgnore]
        public Group Group { get; set; }

        public bool IsAdmin { get; set; }
        public DateTime? LastViewDate { get; set; }
    }

    public class GroupAccountConfig : IEntityTypeConfiguration<GroupAccount>
    {
        public void Configure(EntityTypeBuilder<GroupAccount> builder)
        {
            builder.HasKey(x => new { x.GroupId, x.AccountId });

            builder.HasOne(x => x.Group)
                .WithMany()
                .HasForeignKey(x => x.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(x => x.Account)
                .WithMany()
                .HasForeignKey(x => x.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Property(x => x.IsAdmin).HasDefaultValue(false);
        }
    }
}
