using EventManager.Identity.Services.Abstractions;
using EventManager.Identity.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.SqlRepository.EntityConfigurations
{
    internal sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshTokens");
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Value).HasMaxLength(128).IsUnicode(false);
            builder.HasIndex(x => x.Value).IsUnique();
            builder.HasOne(r => r.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(deleteBehavior: DeleteBehavior.Cascade);
            builder.Property(r => r.UserId).IsRequired();
        }
    }
}
