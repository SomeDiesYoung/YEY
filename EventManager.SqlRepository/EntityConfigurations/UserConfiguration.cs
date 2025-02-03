using EventManager.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.SqlRepository.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(16);

            builder.HasIndex(u => u.UserName);
            builder.HasIndex(u => u.Email).IsUnique();
        }
    }
}
