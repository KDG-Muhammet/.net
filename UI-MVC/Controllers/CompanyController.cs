using Microsoft.AspNetCore.Mvc;

namespace StoreManagement.UI.Web.MVC.Controllers;

public class CompanyController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}