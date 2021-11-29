using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Database.Entities
{
    public class GroupTopic
    {
        public string Topic { get; set; }
    }

    public class GroupTypeConfig : IEntityTypeConfiguration<GroupTopic>
    {
        public void Configure(EntityTypeBuilder<GroupTopic> builder)
        {
            builder.HasKey(x => x.Topic);
            builder.Property(x => x.Topic)
                .HasMaxLength(100)
                .ValueGeneratedNever();
        }
    }
}
