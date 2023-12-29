using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StoreManagement.BL;
using StoreManagement.BL.Domain;

namespace StoreManagement.UI.Web.MVC.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class CompaniesController : ControllerBase
{
    private readonly IManager _mgr;

    public CompaniesController(IManager manager)
    {
        _mgr = manager; 
    }
    
    // GET: /Books
    [HttpGet]
    public IActionResult GetAll()
    {
        List<Company> allCompanies = _mgr.GetAllCompanies().ToList();
        
        if (!allCompanies.Any())
            return NoContent();
        
        return Ok(allCompanies);
    }
    
}