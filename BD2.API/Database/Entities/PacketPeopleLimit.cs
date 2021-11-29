using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Database.Entities
{
    public class PacketPeopleLimit
    {
        public int PeopleLimit { get; set; }
    }

    public class PacketPeopleLimitConfig : IEntityTypeConfiguration<PacketPeopleLimit>
    {
        public void Configure(EntityTypeBuilder<PacketPeopleLimit> builder)
        {
            builder.HasKey(x => x.PeopleLimit);
            builder.Property(x => x.PeopleLimit)
                .ValueGeneratedNever();
        }
    }
}
