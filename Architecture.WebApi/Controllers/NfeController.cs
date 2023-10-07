using Microsoft.AspNetCore.Mvc;

namespace Architecture.WebApi.Controllers;

public class NfeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
