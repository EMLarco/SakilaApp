using Microsoft.AspNetCore.Mvc;
namespace SakilaApp.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();
    public IActionResult Privacy() => View();
}
