using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Database.Entities
{
    public class PacketGroupsLimit
    {
        public int GroupsLimit { get; set; }
    }

    public class PacketGroupsLimitConfig : IEntityTypeConfiguration<PacketGroupsLimit>
    {
        public void Configure(EntityTypeBuilder<PacketGroupsLimit> builder)
        {
            builder.HasKey(x => x.GroupsLimit);
            builder.Property(x => x.GroupsLimit)
                .ValueGeneratedNever();
        }
    }

}
