using EventManager.Domain.Models;
using EventManager.Identity.Models;
using EventManager.SqlRepository.EntityConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Reflection;


namespace EventManager.SqlRepository.Database;

public sealed class AppDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,string>
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Event> Events { get; set; }
    public DbSet<Cutsomer> Customers { get; set; }
    public DbSet<EventSubscription> EventSubscriptions { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load(nameof(SqlRepository))); // ვერ იპივა რადგან არ იყო ჩატვირთული კონტექსტში, და სახელით ძებნით ვერ იპოვა
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(EventConfiguration))!); // იპოვის ყველა ვარიანტში რადგან მიაგნებთ საჭირო ადგილს კონკრეტული ტიპის რეფლექციით




        //modelBuilder.ApplyConfiguration(new EventConfiguration());
        //modelBuilder.ApplyConfiguration(new UserConfiguration());
        //modelBuilder.ApplyConfiguration(new EventSubscriptionConfoguration());
    }
}
