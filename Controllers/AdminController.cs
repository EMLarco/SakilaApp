using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SakilaApp.Controllers;

[Authorize(Roles = "Administrator")]
public class AdminController : Controller
{
    public IActionResult Dashboard()
    {
        return View();
    }
}
