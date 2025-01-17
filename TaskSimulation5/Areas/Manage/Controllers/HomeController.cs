using Microsoft.AspNetCore.Mvc;

namespace TaskSimulation5.Areas.Manage.Controllers
{
        [Area("Manage")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
