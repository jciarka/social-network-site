using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Database.ViewEntities
{
    public class PostCommentNotification
    {
        public Guid PostId { get; set; }
        public Guid? GroupId { get; set; }
        public string PostTitle { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Text { get; set; }
        public DateTime CommentDate { get; set; }
    }

    public class PostCommentNotificationConfiguration : IEntityTypeConfiguration<PostCommentNotification>
    {
        public void Configure(EntityTypeBuilder<PostCommentNotification> builder)
        {
            builder.HasNoKey();
            builder.ToView("NOPostCommentNotificationView");
        }
    }
}
