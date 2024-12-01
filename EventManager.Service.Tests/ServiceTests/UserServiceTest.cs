﻿using EventManager.Service.Models;
using EventManager.Service.Commands;
using EventManager.Service.Services.Abstractions;
using EventManager.Service.Services;
using EventManager.Service.Exceptions;
using EventManager.Service.Services.Inplementations;
using Moq;
using FluentAssertions;

namespace EventManager.Service.Tests.ServiceTests;

public class UserServiceTest
{
    [Fact(DisplayName ="მომხმარებლის სერვისში თუ ყველაფერი სწორადაა შევსებული მომხმარებელი უნდა დარეგისტრირებულიყოს")]
    public async void RegisterUserService_Should_RegisterUser_When_EverythingIsCorrect()
    {
        //Arrange
        var repository = new Mock<IUserRepository>();
            repository.Setup(x=>x.GetByUserName(It.IsAny<string>()))
                .ReturnsAsync((User)null);
        repository.Setup(x => x.SaveUser(It.IsAny<User>()))
            .Returns(Task.CompletedTask);
        
        var service = new UserService(repository.Object);
        var command = new UserCommand
        {
            UserId = 1,
            UserName = "Test",
            Password = "password",
            Email = "Test@gmail.com"
        };

        //act 
        await service.RegisterUser(command);

        //Assert
           repository.Verify(r => r.GetByUserName("Test"), Times.Once);
           repository.Verify(r => r.SaveUser(It.Is<User>(u =>
            u.Id == command.UserId &&
            u.UserName == command.UserName &&
            u.Email == command.Email &&
            u.Password == command.Password
        )), Times.Once);
    }
    [Fact(DisplayName = "მომხმარებლის სერვისში თუ ველები არ სწორად შევსებული უნდა დაგვირტყას შეცდომა")]
    public async Task RegisterUser_ShouldThrowException_WhenValidationFails()
    {
        // Arrange
        var repository = new Mock<IUserRepository>();
        var service = new UserService(repository.Object);
        var command = new UserCommand
        {
            UserName = "Vigaca",
            Password = "Test",
            Email = "Uhhh@gmail.eu"
        };

        // Act
        Func<Task> act = async () => await service.RegisterUser(command);

        // Assert
        await act.Should().ThrowAsync<ValidationException>()
            .WithMessage("User Id must be positive");
        repository.Verify(r => r.GetByUserName(It.IsAny<string>()), Times.Never); 
        repository.Verify(r => r.SaveUser(It.IsAny<User>()), Times.Never);
    }


}

