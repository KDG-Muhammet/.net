using System.ComponentModel.DataAnnotations;
using Moq;
using StoreManagement.BL;
using StoreManagement.BL.Domain;
using StoreManagement.DAL;

namespace Tests.UnitTests;

public class ManagerTests
{
    
    [Fact]
    public void AddStore_ShouldReturnANewStore_GivenValidData() 
    {
        // Arrange
        var repositoryMock = new Mock<IRepository>();
        var manager = new Manager(repositoryMock.Object);

        int id = 10;
        string name = "Test Store";
        string street = "test street";
        int hour = 8;
        
        repositoryMock.Setup(r => r.CreateStore(It.IsAny<Store>()))
            .Callback((Store store) => { store.Id = id; });
            
        // Act
        var newStore = manager.AddStore(name, street,hour);
        
        // Assert
        repositoryMock.Verify(repo => repo.CreateStore(It.IsAny<Store>()), Times.Once);
        Assert.NotNull(newStore);
        Assert.Equal(id, newStore.Id);
        Assert.Equal(name, newStore.Name);
        Assert.Equal(street, newStore.Address);
        Assert.Equal(hour, newStore.OpeningHour);
        repositoryMock.VerifyAll();

    }
    
    [Fact]
    public void AddStore_WithInvalidData_ShouldThrowValidationException()
    {
        // Arrange
        var repositoryMock = new Mock<IRepository>();
        var manager = new Manager(repositoryMock.Object);

        int id = 10;
        string name = "";
        string street = "test street";
        int hour = 8;

        repositoryMock.Setup(r => r.CreateStore(It.IsAny<Store>()))
            .Callback((Store store) => { store.Id = id; })
            .Verifiable(Times.Never);
        
        
        //Act
        var newStore = () =>
        {
            manager.AddStore(name, street, hour);
        };

        // Assert
        Assert.Throws<ValidationException>(newStore);
        repositoryMock.VerifyAll();
    }
    
    
    
    [Fact]
    public void GetAllGames_ShouldReturnGames()
    {
        var expectedGames = new List<Game>
        {
            new Game( "Game 1",5,Genre.Action,new DateOnly(2023, 1, 1),10),
            new Game( "Game 2",5,Genre.Action,new DateOnly(2023, 1, 1),10),
            new Game( "Game 3",5,Genre.Action,new DateOnly(2023, 1, 1),10),
            
        };

        var repositoryMock = new Mock<IRepository>();
        repositoryMock.Setup(r => r.ReadAllGames()).Returns(expectedGames);

        var manager = new Manager(repositoryMock.Object);

        // Act
        var actualGames = manager.GetAllGames();

        // Assert
        Assert.NotNull(actualGames);
        Assert.Equal(expectedGames.Count, actualGames.Count());

        foreach (var expectedGame in expectedGames)
        {
            Assert.Contains(expectedGame, actualGames);
        }
        
    }
    
}