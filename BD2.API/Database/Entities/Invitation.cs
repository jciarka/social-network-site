using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.API.Database.Entities
{
    public class Invitation
    {
        public Guid InvitingId { get; set; }
        public Account Inviting { get; set; }
        public Guid InvitedId { get; set; }
        public Account Invited { get; set; }

        public DateTime InvitationSendAt { get; set; }
    }

    public class InvitationConfig : IEntityTypeConfiguration<Invitation>
    {
        public void Configure(EntityTypeBuilder<Invitation> builder)
        {
            builder.HasKey(x => new { x.InvitingId, x.InvitedId });

            builder.HasOne(x => x.Inviting)
                .WithMany()
                .HasForeignKey(x => x.InvitingId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(x => x.Invited)
                .WithMany()
                .HasForeignKey(x => x.InvitedId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.Property(x => x.InvitationSendAt)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("GETDATE()");

            builder.HasCheckConstraint("NotSelfInvited_Friendship_constraint", "InvitingId <> InvitedId");
        }
    }
}
