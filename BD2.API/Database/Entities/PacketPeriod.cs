using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Database.Entities
{
    public class PacketPeriod
    {
        public int MonthsPeriod { get; set; }
    }

    public class PacketPeriodConfig : IEntityTypeConfiguration<PacketPeriod>
    {
        public void Configure(EntityTypeBuilder<PacketPeriod> builder)
        {
            builder.HasKey(x => x.MonthsPeriod);
        }
    }
}
