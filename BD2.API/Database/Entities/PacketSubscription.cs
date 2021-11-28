using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BD2.API.Database.Entities
{
    public class PacketSubscription
    {
        public Guid Id { get; set; }
        public DateTime ExpirationDate { get; set; }

        public ICollection<Group> Groups { get; set; }

        [JsonIgnore]
        public Account Owner { get; set; }
        public Guid OwnerId { get; set; }

        public Packet Packet { get; set; }
        public Guid PacketId { get; set; }

    }

    public class PacketSubscriptionConfig : IEntityTypeConfiguration<PacketSubscription>
    {
        public void Configure(EntityTypeBuilder<PacketSubscription> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEWSEQUENTIALID ()");

            builder.Property(x => x.ExpirationDate)
                .HasColumnType("date");

            builder
                .HasMany(x => x.Groups)
                .WithOne(x => x.Subscription)
                .HasForeignKey(x => x.SubscriptionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.Owner)
                .WithMany(x => x.Subscriptions)
                .HasForeignKey(x => x.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Packet)
                .WithMany()
                .HasForeignKey(x => x.PacketId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
