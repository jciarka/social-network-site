using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Database.Entities
{
    public class Friendship
    {
        public Guid FirstFriendId { get; set; }
        public Account FirstFriend { get; set; }
        public Guid SecondFriendId { get; set; }
        public Account SecondFriend { get; set; }

        public DateTime FriendshipBeginDate { get; set; }
    }

    public class FriendshipConfig : IEntityTypeConfiguration<Friendship>
    {
        public void Configure(EntityTypeBuilder<Friendship> builder)
        {
            builder.HasKey(x => new { x.FirstFriendId, x.SecondFriendId });

            builder.HasOne(x => x.FirstFriend)
                .WithMany()
                .HasForeignKey(x => x.FirstFriendId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(x => x.SecondFriend)
                .WithMany()
                .HasForeignKey(x => x.SecondFriendId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasCheckConstraint("NotSelfFriends_Friendship_constraint", "FirstFriendId <> SecondFriendId");
        }
    }
}
