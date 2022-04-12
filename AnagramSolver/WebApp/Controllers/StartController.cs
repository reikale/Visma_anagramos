using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class StartController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}