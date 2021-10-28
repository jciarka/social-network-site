using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BD2.API.Database.Entities
{
    public class Friendship
    {
        public Guid FirstFriendId { get; set; }
        [JsonIgnore]
        public Account FirstFriend { get; set; }
        public Guid SecondFriendId { get; set; }
        [JsonIgnore]
        public Account SecondFriend { get; set; }

        public DateTime FriendshipBeginDate { get; set; }
    }

    public class FriendshipConfig : IEntityTypeConfiguration<Friendship>
    {
        public void Configure(EntityTypeBuilder<Friendship> builder)
        {
            builder.HasKey(x => new { x.FirstFriendId, x.SecondFriendId });

            builder.HasOne(x => x.FirstFriend)
                .WithMany(x => x.Friendships)
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
