using EventManager.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.SqlRepository.EntityConfigurations
{
    internal sealed class ApplicationUserRolesConfiguration : IEntityTypeConfiguration<ApplicationUserRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
        {
            builder.HasData(
                new ApplicationUserRole
                {
                    Id = "A117A8B5-F055-4A06-98A6-faxA4CEDBB24",
                    Name = "Member",
                    NormalizedName = "MEMBER",
                    AccessDescription = "Can Subscribe/Unsubscribe on Events, has access to own account manipulations",
                     ConcurrencyStamp = "member-concurrency-stamp",
                },
                new ApplicationUserRole
                {
                    Id = "190F2xxC-7177-4C77-BAd2-9121A40206BB",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    AccessDescription = "Can Manipulate with Events, has access to own account manipulations",
                    ConcurrencyStamp = "admin-concurrency-stamp",
                },     
                new ApplicationUserRole
                {
                    Id = "e2H52d72-326e-4AV3-8f1b-7d1a2c2ed14b",
                    Name = "Owner",
                    NormalizedName = "OWNER",
                    AccessDescription = "Can Manage Roles, can assign Roles to Users",
                    ConcurrencyStamp = "owner-concurrency-stamp",
                }
                );
        }
    }
}
