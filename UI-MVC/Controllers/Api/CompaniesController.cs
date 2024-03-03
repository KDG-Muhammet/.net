using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StoreManagement.BL;
using StoreManagement.BL.Domain;
using StoreManagement.UI.Web.MVC.Models.Dto;

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
    
    [HttpGet]
    public IActionResult GetAll()
    {
        List<Company> allCompanies = _mgr.GetAllCompanies().ToList();
        
        if (!allCompanies.Any())
            return NoContent();
        
        return Ok(allCompanies);
    }
    
    // GET: /api/Companies/{id}
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        Company company = _mgr.GetCompanyById(id);

        if (company == null)
            return NotFound();

        return Ok(company);
    }

    
    [HttpPost] 
    [Authorize]
    public IActionResult Post(NewCompanyDto newCompanyDto)
    {
        Company company = _mgr.AddCompany(newCompanyDto.Name, newCompanyDto.Address, newCompanyDto.YearFounded);
        return CreatedAtAction("Get", new {id=company.Id}, company);
    }
}

    