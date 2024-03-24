using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using StoreManagement.BL;
using StoreManagement.BL.Domain;
using Tests.IntegrationTests.Config;

namespace Tests.IntegrationTests;

public class ManagerTests : IClassFixture<ExtendedWebApplicationFactoryWithMockAuth<Program>>
{
    
    private readonly  ExtendedWebApplicationFactoryWithMockAuth<Program> _factory;
    
    public ManagerTests(ExtendedWebApplicationFactoryWithMockAuth<Program> factory)
    {

        _factory = factory;

    }

    [Fact]
    public void AddGame_ShouldReturnGame_WithTheDataOfTheNewGame()
    {
        //Arrange

        using (var scope = _factory.Services.CreateScope())
        {
            var mgr = scope.ServiceProvider.GetRequiredService<IManager>();

            string name = "Test Game";
            double? price = 29.99;
            Genre genre = Genre.Action;
            DateOnly yearReleased = new DateOnly(2023, 1, 1);
            int rating = 8;

            //Act
            var newgame = mgr.AddGame(name, price, genre, yearReleased, rating);
            
            //Assert
            Assert.NotNull(newgame);
            Assert.Equal(name, newgame.Name);
            Assert.Equal(price, newgame.Price);
            Assert.Equal(genre, newgame.Genre);
            Assert.Equal(yearReleased, newgame.YearReleased);
            Assert.Equal(rating, newgame.Rating);
            
        }
        
    }
    
    [Fact]
    public void AddGame_ShouldAddGameToRepository()
    {
        //Arrange

        using (var scope = _factory.Services.CreateScope())
        {
            //Arrange
            var mgr = scope.ServiceProvider.GetRequiredService<IManager>();

            int id = 5;

            
            //Act
            var game = mgr.GetGame(id);

            
            //Assert
           Assert.NotNull(game);
           Assert.Equal("Test Game", game.Name);
        }
        
    }

    [Fact]
    public void AddGame_WithInvalidInput_ShouldThrowValidationException()
    {
        // Arrange
        using (var scope = _factory.Services.CreateScope())
        {
            var mgr = scope.ServiceProvider.GetRequiredService<IManager>();
           
            string name = "test";
            double? price = 29.99;
            Genre genre = Genre.Action;
            DateOnly yearReleased = new DateOnly(2023, 1, 1);
            int rating = 11;

            //Act
            var newGame = () =>
            {
                mgr.AddGame(name, price, genre, yearReleased, rating);
            };
            
            //Assert
            Assert.Throws<ValidationException>(newGame);
            
        }
    }
    
    [Fact]
    public void GetStore_ShouldReturnStore_GivenExistingId()
    {
        // Arrange
        int storeId = 1;

        using (var scope = _factory.Services.CreateScope())
        {
            var mgr = scope.ServiceProvider.GetRequiredService<IManager>();

            // Act
            var store = mgr.GetStore(storeId);

            // Assert
            Assert.NotNull(store);
            Assert.Equal(storeId, store.Id);
            
        }
    }
    
    [Fact]
    public void GetStore_ShouldReturnNull_GivenNoneExistingId()
    {
        // Arrange
        int storeId = 0;
        
        using (var scope = _factory.Services.CreateScope())
        {
            var mgr = scope.ServiceProvider.GetRequiredService<IManager>();
            
            // Act
            var result = mgr.GetStore(storeId);

            // Assert
            Assert.Null(result);
        }
    }
    
}