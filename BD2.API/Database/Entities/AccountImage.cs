using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BD2.API.Database.Entities
{
    public class AccountImage
    {
        public Guid AccountId { get; set; }
        [JsonIgnore]
        public Account Account { get; set; }
        public Guid ImageId { get; set; }
        [JsonIgnore]
        public Image Image { get; set; }
    }
}

namespace BD2.API.Database.Entities
{
    public class AccountImageConfig : IEntityTypeConfiguration<AccountImage>
    {
        public void Configure(EntityTypeBuilder<AccountImage> builder)
        {
            builder.HasKey(x => new { x.ImageId, x.AccountId });

            builder.HasOne(x => x.Account)
                .WithMany()
                .HasForeignKey(x => x.AccountId);

            builder.HasOne(x => x.Image)
                .WithMany()
                .HasForeignKey(x => x.ImageId);
        }
    }
}
