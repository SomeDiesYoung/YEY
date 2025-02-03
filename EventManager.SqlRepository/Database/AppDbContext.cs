using EventManager.Domain.Models;
using EventManager.SqlRepository.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


namespace EventManager.SqlRepository.Database;

public sealed class AppDbContext : DbContext
{
    public DbSet<Event> Events { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<EventSubscription> EventSubscriptions { get; set; }


    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        // modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load(nameof(SqlRepository))); // ვერ იპივა რადგან არ იყო ჩატვირთული კონტექსტში, და სახელით ძებნით ვერ იპოვა
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(EventConfiguration))!); // იპოვის ყველა ვარიანტში რადგან მიაგნებთ საჭირო ადგილს კონკრეტული ტიპის რეფლექციით

        base.OnModelCreating(modelBuilder);



        //modelBuilder.ApplyConfiguration(new EventConfiguration());
        //modelBuilder.ApplyConfiguration(new UserConfiguration());
        //modelBuilder.ApplyConfiguration(new EventSubscriptionConfoguration());
    }
}
