using System.Net;
using System.Text;
using Newtonsoft.Json;
using StoreManagement.UI.Web.MVC.Models.Dto;
using Tests.IntegrationTests.Config;

namespace Tests.IntegrationTests;

public class CompaniesController :  IClassFixture<ExtendedWebApplicationFactoryWithMockAuth<Program>>
{
    private readonly ExtendedWebApplicationFactoryWithMockAuth<Program> _factory;
    
    public CompaniesController(ExtendedWebApplicationFactoryWithMockAuth<Program> factory)
    {
        _factory = factory;
    }
    [Fact]
    public void Get_ShouldReturnOK_IfCompanyExists()
    {
        // Arrange
        var client = _factory.CreateClient();
        int companyId = 1; 

        // Act
        var response = client.GetAsync($"/api/Companies/{companyId}").Result;
        var company = response.Content.ReadAsStringAsync(); 

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var responseContentType = response.Content.Headers.GetValues("Content-Type").FirstOrDefault();
        Assert.Contains("application/json", responseContentType);
        Assert.NotNull(company);
        Assert.Equal(companyId, company.Id);
      
    }

    // [Fact]
    // public void Get_ShouldReturnNotFoundForNonExistingCompany()
    // {
    //     // Arrange
    //     var client = _factory.CreateClient();
    //     int nonExistingCompanyId = 999; 
    //
    //     // Act
    //     var response =  client.GetAsync($"/api/Companies/{nonExistingCompanyId}").Result;
    //
    //     // Assert
    //     Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    // }
}


    
    