using EventManager.Service.Commands;
using EventManager.Service.Models;
using EventManager.Service.Exceptions;
using FluentAssertions;
using Moq;
using EventManager.Service.Services.Abstractions;
using System.Xml.Linq;
namespace EventManager.Service.Tests.Commands.Test;

public class CreateEventCommandTest
{

    [Fact(DisplayName = "ვალიდაციისას თუ დაწყების თარიღი წარსულშია")]
    public void CreateCommandTest_ValidateDateAndDuration_ShouldThrowException_WhileStardDateIsInPast()
    {
        //Arrange
        var command = new CreateEventCommand
        {
            StartDate = DateTime.Now.AddDays(-2),
            EndDate = DateTime.Now.AddDays(+2),
            Description = "Something",
            Location = "Somewhere",
            Name = "Name",
            Status = EventStatus.Active
        };

        //Act
        Action act = () => command.ValidateDateAndDuration();

        //Assert
        act.Should().Throw<ValidationException>().WithMessage("Start date must be in the future");
    }

    [Fact(DisplayName = "ვალიდაციისას თუ დასასრულის თარიყი არის დასაწყისზე ადრე თარიღზე")]
    public void CreateCommandTest_ValidateDateAndDuration_ShouldThrowException_WhileEndDateIsEmpty()
    {

        //Arrange
        var command = new CreateEventCommand
        {
            StartDate = DateTime.Now.AddDays(+1),
            EndDate = DateTime.Now.AddDays(-2),
            Description = "Something",
            Location = "Somewhere",
            Name = "Name",
            Status = EventStatus.Active
        };

        //Act

        Action act = () => command.ValidateDateAndDuration();

        //Assert 
        act.Should().Throw<ValidationException>().WithMessage("End date must be later than start date");
    }


    [Fact(DisplayName = "ვალიდაციისას ხანგრძლივობის არასწორ დათვლისას დაარტყას შეცდომა")]

    public void CreateCommandTest_ValidateDateAndDuration_ShouldThrowException_whenDurationIsNotEqualEndDateMinusStartDate()
    {
        var startDate = DateTime.Now.AddDays(+1);
        var endDate = DateTime.Now.AddDays(+3);
        var command = new CreateEventCommand
        {
            //Arrange

            StartDate = startDate,
            EndDate = endDate,
            Description = "Something",
            Location = "Somewhere",
            Name = "Name",
            Status = EventStatus.Active,
        };
        //Act
        command.ValidateDateAndDuration();

        //Assert
        command.Duration.Should().Be(endDate - startDate);
    }


    [Fact(DisplayName = "ვალიდაციის უშეცდომოთ გასვლისას არ უნდა დაარტყას შეცდომა")]
    public void CreateCommandTest_Should_Not_ThrowExceptions()
    {
       
        var command = new CreateEventCommand
        {
            //Arrange

            StartDate = DateTime.Now.AddDays(+1),
            EndDate = DateTime.Now.AddDays(+4),
            Description = "Something",
            Location = "Somewhere",
            Name = "Name",
            Status = EventStatus.Active,
        };
        //Act
        Action act = ()=> command.ValidateDateAndDuration();

        //Assert
        act.Should().NotThrow();

    }
    //Ar vici ra vuyo magas solutions vedzeb

    /*
    [Fact(DisplayName = "ვალიდაციისას თუ სახელი უკვე არსებობს უნდა დაარტყას შეცდომა")]
    public void CreateCommandTest_Name_Should_Be_Unique()
    {
        // Arrange
        var MockRepository = new Mock<IEventRepository>();
        MockRepository.Setup(x => x.GetByName("Test Name"))
                     .Returns(new Event {
                         Description = "Name",
                         Location = "adsa",
                         Name = "Test Name",
                         Status = EventStatus.Active};

        var command = new CreateEventCommand
        {
            Description = "Name",
            Location = "adsa",
            Name = "Test Name",
            Status = EventStatus.Active
        };

        // Act
        Action act = () => command.Validate(MockRepository.Object);

        // Assert
        act.Should().Throw<ValidationException>()
           .WithMessage("An event with the name 'Test Name' already exists.");
    }
    */
}