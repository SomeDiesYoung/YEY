using System;
using Xunit;
using FluentAssertions;
using EventManager.Service.Commands;
using EventManager.Service.Exceptions;
using EventManager.Service.Models.Enums;

namespace EventManager.Tests;

public class UpdateEventCommandTests
{
    [Fact(DisplayName = "ვალიდაციისას თუ დაწყების თარიღი წარსულშია")]
    public void ValidateDateAndDuration_ShouldThrowException_WhenStartDateIsInPast()
    {
        // Arrange
        var command = new UpdateEventCommand
        {
            StartDate = DateTime.Now.AddDays(-1),
            EndDate = DateTime.Now.AddDays(1),
            Name = "Test Event",
            Description = "Test Description",
            Location = "Test Location",
            Status = EventStatus.Active
        };

        // Act
        Action act = () => command.ValidateDateAndDuration();

        // Assert
        act.Should().Throw<ValidationException>()
           .WithMessage("Start date must be in future");
    }

    [Fact(DisplayName = "ვალიდაციისას თუ დასასრულის თარიყი არის დასაწყისზე ადრე თარიღზე")]
    public void ValidateDateAndDuration_ShouldThrowException_WhenEndDateIsInvalid()
    {
        // Arrange
        var command = new UpdateEventCommand
        {
            StartDate = DateTime.Now.AddDays(1),
            EndDate = DateTime.Now.AddDays(-1),
            Name = "Test Event",
            Description = "Test Description",
            Location = "Test Location",
            Status = EventStatus.Active
        };

        // Act
        Action act = () => command.ValidateDateAndDuration();

        // Assert
        act.Should().Throw<ValidationException>()
           .WithMessage("End date must be later or equal start date.");
    }

    [Fact(DisplayName = "ხანგრძლივობა უნდა იყოს ვალიდური როდესაც თარიღი სშორადაა მოცემული")]
    public void ValidateDateAndDuration_ShouldCalculateDuration_WhenDatesAreValid()
    {
        // Arrange
        var startDate = DateTime.Now.AddDays(1);
        var endDate = DateTime.Now.AddDays(1);
        var command = new UpdateEventCommand
        {
            StartDate = startDate,
            EndDate = endDate,
            Name = "Test Event",
            Description = "Test Description",
            Location = "Test Location",
            Status = EventStatus.Active
        };

        // Act
        command.ValidateDateAndDuration();

        // Assert
        command.Duration.Should().Be(endDate - startDate);
    }

    [Fact(DisplayName = "სტატუსის შეცვლა გაუქმებული ღონისძიებისთვის უნდა აღსძვრას შეცდომა")]
    public void ValidationForUpdate_ShouldThrowException_WhenStatusIsCancelled()
    {
        // Arrange
        var command = new UpdateEventCommand
        {
            StartDate = DateTime.Now.AddDays(1),
            EndDate = DateTime.Now.AddDays(2),
            Name = "Test Event",
            Description = "Test Description",
            Location = "Test Location",
            Status = EventStatus.Cancelled
        };

        // Act
        Action act = () => command.ValidationForUpdate();

        // Assert
        act.Should().Throw<ValidationException>()
           .WithMessage("Cannot change status of completed or cancelled events.");
    }

    [Fact(DisplayName = "შეცდომა არ უნდა იყოს სტატუსი როდესაც აქტიურია")]
    public void ValidationForUpdate_ShouldNotThrowException_WhenStatusIsActive()
    {
        // Arrange
        var command = new UpdateEventCommand
        {
            StartDate = DateTime.Now.AddDays(1),
            EndDate = DateTime.Now.AddDays(2),
            Name = "Test Event",
            Description = "Test Description",
            Location = "Test Location",
            Status = EventStatus.Active
        };

        // Act
        Action act = () => command.ValidationForUpdate();

        // Assert
        act.Should().NotThrow();
    }
}
