using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using StoreManagement.BL;
using StoreManagement.BL.Domain;
using StoreManagement.UI.Web.MVC.Controllers;
using StoreManagement.UI.Web.MVC.Models;

namespace Tests.UnitTests;

public class GameControllerTests
{
    private Mock<UserManager<TUser>> GetMockUserManager<TUser>() where TUser : class {
        var userManagerMock = new Mock<UserManager<TUser>>(
            new Mock<IUserStore<TUser>>().Object,
            new Mock<IOptions<IdentityOptions>>().Object,
            new Mock<IPasswordHasher<TUser>>().Object,
            new IUserValidator<TUser>[0],
            new IPasswordValidator<TUser>[0],
            new Mock<ILookupNormalizer>().Object,
            new Mock<IdentityErrorDescriber>().Object,
            new Mock<IServiceProvider>().Object,
            new Mock<ILogger<UserManager<TUser>>>().Object);
        return userManagerMock;
    }
    
    [Fact]
    public void Add_ShouldReturnViewResult_WithInvalidModelState()
    {
        // Arrange
        var managerMock = new Mock<IManager>();
        var userMock = GetMockUserManager<IdentityUser>();
        var controller = new GameController(managerMock.Object, userMock.Object);
        var game = new Game("test", 10, Genre.Action, new DateOnly(2022, 10, 1), 20);
        var newGameModel = new NewViewGameModel
        {
            Name = "test", 
            Price = 20,  
            Genre = Genre.Action,
            YearReleased = new DateOnly(2001, 4,12),
            Rating = 20
        };
        managerMock.Setup(m => m.AddGame(newGameModel.Name,newGameModel.Price,newGameModel.Genre,newGameModel.YearReleased,newGameModel.Rating ))
            .Returns(game)
            .Verifiable(Times.Once);

        // Act
        var result = controller.Add(newGameModel);
        
        // Assert
        managerMock.Verify(mgr => mgr.AddGame(newGameModel.Name, newGameModel.Price, newGameModel.Genre, newGameModel.YearReleased, newGameModel.Rating), Times.Once);
        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Details", redirectResult.ControllerName?? nameof(GameController.Details));
        Assert.Null(redirectResult.ControllerName);
    }
    
    [Fact]
    public void AddGame_ShouldRedirectToDetails_WithValidData()
    {
        // Arrange
        var managerMock = new Mock<IManager>();
        var userMock = GetMockUserManager<IdentityUser>();
        var controller = new GameController(managerMock.Object,userMock.Object);
        var game = new Game("test", 10, Genre.Action, new DateOnly(2022, 10, 1), 5);
        var newGameModel = new NewViewGameModel()
        {
            Name = "test", 
            Price = 20,  
            Genre = Genre.Action,
            YearReleased = new DateOnly(2001, 4,12),
            Rating = 7
        };
         managerMock.Setup(m => m.AddGame(newGameModel.Name,newGameModel.Price,newGameModel.Genre,newGameModel.YearReleased,newGameModel.Rating ))
             .Returns(game)
            .Verifiable(Times.Once);
        
        
        // Act
        var result = controller.Add(newGameModel);
        
        // Assert
        managerMock.Verify(mgr => mgr.AddGame(newGameModel.Name, newGameModel.Price, newGameModel.Genre, newGameModel.YearReleased, newGameModel.Rating), Times.Once);
        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Details", redirectResult.ControllerName?? nameof(GameController.Details));
    }
    
    
    [Fact]
    public void Details_ShouldReturnViewWithGame_WhenIdExists()
    {
        // Arrange
        int gameId = 1;
        var expectedGame = new Game("test",10,Genre.Action, new DateOnly(2022,10,1),5);
        var managerMock = new Mock<IManager>();
        var userMock = GetMockUserManager<IdentityUser>();
        managerMock.Setup(m => m.GetGameWithStores(gameId)).Returns(expectedGame);
        var controller = new GameController(managerMock.Object, userMock.Object);

        // Act
        var result = controller.Details(gameId);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<Game>(viewResult.Model);
        Assert.Equal(expectedGame.Id, model.Id);
        Assert.Equal(expectedGame.Name, model.Name);
    }
}