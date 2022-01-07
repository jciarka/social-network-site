using BD2.API.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Database.ViewEntities
{
    public class PostReactionNotification
    {
        public Guid PostId { get; set; }
        public Guid? GroupId { get; set; }
        public string PostTitle { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime ReactionDate { get; set; }
        public ReactionType Type { get; set; }
    }

    public class PostReactionNotificationConfiguration : IEntityTypeConfiguration<PostReactionNotification>
    {
        public void Configure(EntityTypeBuilder<PostReactionNotification> builder)
        {
            builder.HasNoKey();
            builder.ToView("NOPostReactionNotificationView");
        }
    }
}
