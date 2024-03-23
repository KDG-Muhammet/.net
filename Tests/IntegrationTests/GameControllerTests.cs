using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using StoreManagement.BL.Domain;
using StoreManagement.DAL.EF;
using StoreManagement.UI.Web.MVC.Models;
using Tests.IntegrationTests.Config;

namespace Tests.IntegrationTests;

public class GameControllerTests :  IClassFixture<ExtendedWebApplicationFactoryWithMockAuth<Program>>
{
    private readonly ExtendedWebApplicationFactoryWithMockAuth<Program> _factory;
    
    public GameControllerTests(ExtendedWebApplicationFactoryWithMockAuth<Program> factory)
    {
        _factory = factory;
    }
    
    [Fact]
    public void Add_ShouldReturnRedirectToAction_WhenModelStateIsValid()
    {
        // Arrange
        var client = _factory
            .SetAuthenticatedUser(
                new Claim(ClaimTypes.NameIdentifier, "Id"), 
                new Claim(ClaimTypes.Name, "test@app.com"), 
                new Claim(ClaimTypes.Email, "test@app.com"),
                new Claim(ClaimTypes.Role, "Admin"))
            .CreateClient(
            new WebApplicationFactoryClientOptions {
                AllowAutoRedirect = false
            });
        
        string url = "/Game/Add";
        var game = new Dictionary<string, string>()
        {
            { "Name" , "Test Game"},
            { "Price" , "29.99" },
            { "Genre" , "Action" },
            { "YearReleased" , "2023-1-1" },
            { "Rating" , "8" }
        };
        HttpContent httpContent = new FormUrlEncodedContent(game); 
        
        // Act
        var response = client.PostAsync(url, httpContent).Result;

        // Assert
        Assert.Equal(HttpStatusCode.Redirect,response.StatusCode);
        Assert.StartsWith("/Game/Details", response.Headers.Location?.OriginalString);
    }
    
    [Fact]
    public void Add_ShouldReturnViewWithModel_WhenModelStateIsInvalid()
    {
        // Arrange
        var client = _factory.CreateClient(
            new WebApplicationFactoryClientOptions {
                AllowAutoRedirect = false
            });
        
         var game = new Dictionary<string, string>()
        {
            { "Name" , ""},
            { "Price" , "29.99" },
            { "Genre" , "Action" },
            { "YearReleased" , "2023-1-1" },
            { "Rating" , "8" }
        };
         HttpContent httpContent = new FormUrlEncodedContent(game); 

        // Act
        var response =  client.PostAsync("/Game/Add", httpContent).Result;
        var result =  response.Content.ReadAsStringAsync().Result;

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("The Name field is required", result); 
    }
}
