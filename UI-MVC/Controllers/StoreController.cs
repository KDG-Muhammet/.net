using Microsoft.AspNetCore.Mvc;
using StoreManagement.BL;
using StoreManagement.BL.Domain;

namespace StoreManagement.UI.Web.MVC.Controllers;

public class StoreController : Controller
{
    
    private readonly IManager _mgr;

    public StoreController(IManager manager)
    {
        _mgr = manager; 
    }
    
    public IActionResult Details(int storeId)
    {
        Store store = _mgr.GetStore(storeId);
        return View(store);
    }
    
}