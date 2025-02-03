using EventManager.Domain.Models;
using EventManager.Domain.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace EventManager.SqlRepository.EntityConfigurations
{
    public sealed class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events");

            builder.HasKey(e => e.Id);

            builder.Property(e =>e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Description)
                .IsRequired(false)
                .HasMaxLength(4000);

            builder.Property(e => e.Location)
                .IsRequired()
                .HasMaxLength(120);

            builder.Property(e => e.Status)
                .IsRequired()
                //.HasConversion(
                //    v => v.ToString(),
                //   v => Enum.Parse<EventStatus>(v)); //Mirchevnia optimizirebuli iyos
                .HasConversion<byte>();


            builder.Property(e => e.StartDate)
                 .IsRequired(false)
                 .HasColumnType("datetime2");

            builder.Property(e => e.EndDate)
                .IsRequired(false)
                .HasColumnType("datetime2");

            builder.Property(e => e.DurationInHours)
                 .IsRequired(false)
                 .HasColumnType("float");

            builder.HasIndex(e => e.DurationInHours);
        }
    }
}
