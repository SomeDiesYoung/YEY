using EventManager.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.SqlRepository.EntityConfigurations
{
    public class EventSubscriptionConfoguration : IEntityTypeConfiguration<EventSubscription>
    {
        public void Configure(EntityTypeBuilder<EventSubscription> builder)
        {
            builder.ToTable("EventSubscriptions");

            builder.HasKey(es => es.Id);

            builder.Property(es => es.UserId)
                .IsRequired();

            builder.Property(es => es.EventId)
                .IsRequired();

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(es => es.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Event>()
                .WithMany()
                .HasForeignKey(es => es.EventId)
                  .OnDelete(DeleteBehavior.Cascade); ;
        }
    }
}
