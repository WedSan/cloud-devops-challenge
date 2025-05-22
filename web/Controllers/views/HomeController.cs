using Microsoft.AspNetCore.Mvc;

namespace web.Controllers.views;

public class HomeController : Controller
{

    public IActionResult Index()
    {
        return View();
    }
}