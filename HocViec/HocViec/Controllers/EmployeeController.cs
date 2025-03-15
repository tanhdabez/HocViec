using Microsoft.AspNetCore.Mvc;

namespace HocViec.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            string role = HttpContext.Session.GetString("Role");
            if (role != "Employee")
            {
                return RedirectToAction("Login", "Authentication");
            }

            return View();
        }
    }
}
