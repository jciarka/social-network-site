using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BD2.API.Database.Entities
{
    public class Packet
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        [JsonIgnore]
        public PacketGroupsLimit GroupsLimitObject { get; set; }
        public int GroupsLimit { get; set; }
        [JsonIgnore]
        public PacketPeopleLimit PeopleLimitObject { get; set; }
        public int PeopleLimit { get; set; }
        [JsonIgnore]
        public PacketPeriod PacketPeriodObject { get; set; }
        public int PacketPeriod { get; set; }
        public int IsValid { get; set; }
    }

    public class PacketConfig : IEntityTypeConfiguration<Packet>
    {
        public void Configure(EntityTypeBuilder<Packet> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEWSEQUENTIALID ()");

            builder.Property(x => x.Name)
                .IsRequired(true)
                .HasMaxLength(100);

            builder.Property(x => x.Price)
                .IsRequired(true)
                .HasPrecision(5, 2);

            builder
                .HasOne(x => x.GroupsLimitObject)
                .WithMany()
                .HasForeignKey(x => x.GroupsLimit)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.PeopleLimitObject)
                .WithMany()
                .HasForeignKey(x => x.PeopleLimit)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.PacketPeriodObject)
                .WithMany()
                .HasForeignKey(x => x.PacketPeriod)
                .OnDelete(DeleteBehavior.Restrict);



        }
    }
}
