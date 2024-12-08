using EventManager.Service.Commands;
using EventManager.Service.Exceptions;
using EventManager.Service.Models.Enums;
using FluentAssertions;
using System.Xml.Linq;

namespace EventManager.Service.Tests;

public class EventCommandTest
{
    private class MockEventCommand : EventCommand
    {
        public override void ValidateDateAndDuration()
        {

            if (StartDate >= EndDate)
                throw new ValidationException("Start date must be earlier than end date");
            if (Duration.TotalMinutes <= 0)
                throw new ValidationException("Duration must be greater than zero");
        }
    }

    [Fact(DisplayName = "ვალიდაციის გასვლისას არასწორი ფორმატის შემთხვევაში უნდა გაისროლოს შეცდომა")]
    public void CommandEvent_Validation_Should_Throw_Exception_WhenEventIsInvalid()
    {
        //Arrange

        var command = new MockEventCommand
        {
            EventId = -1,
            Name = "Valid Name",
            Description = "Valid Description",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddHours(1),
            Duration = TimeSpan.FromHours(1),
            Location = "Valid Location",
            Status = EventStatus.Active
        };



        //Act
        Action act = () => command.Validate();

        //Assert
        act.Should().Throw<ValidationException>();
    }

    [Fact(DisplayName = "ვალიდაციის გასვლისას თუ თარიღი არასწორ ფორმატშია შეცდომა უნდა გაისროლდეს")]
    public void CommandEvent_ValidationDate_Should_Throw_Exception_WhenEventDateIsInvalid()
    {
        //Arrange
        var command = new MockEventCommand
        {
            EventId = 1,
            Name = "Valid Name",
            Description = "Valid Description",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddHours(-1),
            Duration = TimeSpan.FromHours(-1),
            Location = "Valid Location",
            Status = EventStatus.Active
        };

        //Act
        Action act = ()=> command.ValidateDateAndDuration();

        //Assert
        act.Should().Throw<ValidationException>()
            .WithMessage("Start date must be earlier than end date");
    }

       [Fact(DisplayName = "ვალიდაციის გასვლისას თუ თარიღი არასწორ ფორმატშია შეცდომა უნდა გაისროლდეს")]
    public void CommandEvent_ValidationDuration_Should_Throw_Exception_WhenEventDurationIsInvalid()
    {
        //Arrange
        var command = new MockEventCommand
        {
            EventId = 1,
            Name = "Valid Name",
            Description = "Valid Description",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddHours(+1),
            Duration = TimeSpan.FromHours(0),
            Location = "Valid Location",
            Status = EventStatus.Active
        };

        //Act
        Action act = ()=> command.ValidateDateAndDuration();

        //Assert
        act.Should().Throw<ValidationException>()
            .WithMessage("Duration must be greater than zero");
    }

    [Fact(DisplayName = "თუ სახელის შესავსები ველი არ არის შევსებული უნდა დაგვირტყას შეცდომა")]
    public void Validate_Shout_Throw_Exception_WhileNameIsEmpty()
    {
        //arrange 
        var command = new MockEventCommand
        {
            EventId = 1,
            Name = "",
            Description = "Valid Description",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddHours(+1),
            Duration = TimeSpan.FromHours(0),
            Location = "Valid Location",
            Status = EventStatus.Active
        };
        //Act
        Action act = () => command.Validate();

        //Assert

        act.Should().Throw<ValidationException>();
    }

    [Fact(DisplayName ="ვალიდაციის გასვლისას თუ არ არის ნაპოვნი შეცდომა მშვიდობიანად უდანდა გაიაროს შეცდომების გარეშე")]
    public void Validate_ShouldPass_WhenAllPropertiesAreValid()
    {
        // Arrange
        var command = new MockEventCommand
        {
            EventId = 1,
            Name = "Valid Name",
            Description = "Valid Description",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddHours(1),
            Duration = TimeSpan.FromHours(1),
            Location = "Valid Location",
            Status = EventStatus.Active
        };

        // Act
        Action act = () => command.Validate();

        // Assert
        act.Should().NotThrow();
    }
}

