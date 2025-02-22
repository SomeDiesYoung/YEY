using EventManager.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace EventManager.SqlRepository.EntityConfigurations
{
    internal sealed class ApplicationUserRolesConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.HasData(
                new ApplicationRole
                {
                    Id = "A117A8B5-F055-4A06-98A6-faxA4CEDBB24",
                    Name = "Member",
                    NormalizedName = "MEMBER",
                    AccessDescription = "Can Subscribe/Unsubscribe on Events, has access to own account manipulations",
                    ConcurrencyStamp = "member-concurrency-stamp",
                },
                new ApplicationRole
                {
                    Id = "190F2xxC-7177-4C77-BAd2-9121A40206BB",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    AccessDescription = "Can Manipulate with Events, has access to own account manipulations",
                    ConcurrencyStamp = "admin-concurrency-stamp",
                },
                new ApplicationRole
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


internal sealed class ApplicationUserSeed : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        var hasher = new PasswordHasher<ApplicationUser>();
        builder.HasData(new ApplicationUser
        {
            Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
            UserName = "ServiceDefaultOwner",
            Email = "EventManagerOwner@yopmail.com",
            EmailConfirmed = true,
            LockoutEnabled = true,
            NormalizedEmail = "EVENTMANAGEROWNER@YOPMAIL.COM",
            NormalizedUserName = "SERVICEDEFAULTOWNER",
            //"!PasswordOwner!123!"
            PasswordHash = "AQAAAAIAAYagAAAAEGIOE+75U/gx0OdCzYPi19fYQZUZao7vshDU74orMUYLNSgWOuYq0uGUzi9IyKbATQ=="
        });

    }
}

internal sealed class ApplicationUserRoles : IEntityTypeConfiguration<IdentityUserRole<string>>
{

    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(
            new IdentityUserRole<string>
            {
                RoleId = "e2H52d72-326e-4AV3-8f1b-7d1a2c2ed14b",
                UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
            });
    }
}

}
