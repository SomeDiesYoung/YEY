using EventManager.Domain.Commands;
using EventManager.Domain.Exceptions;
using FluentAssertions;
namespace EventManager.Service.Tests.Commands.Test;

public class UserCommandTest
{
    [Fact(DisplayName = "ვალიდაციისას თუ id არის ნაკლები 1 შეცდომას ისვრის ვალიდაცია")]
    public void UserCommand_ShouldThrowException_WhenIdIsFilledIncorrectly()
    {
        //Arrange
        var command = new RegisterCustomerCommand
        {
            UserName = "Test",
            Password = "Test",
            Email = "Test"
        };

        //Act
        Action action = () => { command.Validate(); };

        //Assert
        action.Should().Throw<ValidationException>().WithMessage("User Id must be positive");
    } 
    [Fact(DisplayName = "ვალიდაციისას თუ სახელი არ არის სწორად შევსებული ისვრის ვალიდაცია")]
    public void UserCommand_ShouldThrowException_WhenNameIsWhitespace()
    {
        //Arrange
        var command = new RegisterCustomerCommand
        {
            UserName = "     ",
            Password = "afadgWRVREV",
            Email = "Test@gmail.com"
        };

        //Act
        Action action = () => { command.Validate(); };

        //Assert
        action.Should().Throw<ValidationException>().WithMessage("User Name Length must be between 1 and 150 chars");
    }  
    
    [Fact(DisplayName = "ვალიდაციისას თუ ელ.ფოსტა არ არის სწორად შევსებული ისვრის ვალიდაცია")]
    public void UserCommand_ShouldThrowException_WhenEmailFilledIncorrectly()
    {
        //Arrange
        var command = new RegisterCustomerCommand
        {
            UserName = "wgberv",
            Password = "afadgWRVREV",
            Email = "Test!gmail.com"
        };

        //Act
        Action action = () => { command.Validate(); };

        //Assert
        action.Should().Throw<ValidationException>().WithMessage("Invalid email format");
    }

}
