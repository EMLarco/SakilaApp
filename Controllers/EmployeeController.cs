using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SakilaApp.Controllers;

[Authorize(Roles = "Employee,Administrator")]
public class EmployeeController : Controller
{
    public IActionResult Dashboard()
    {
        return View();
    }
}
