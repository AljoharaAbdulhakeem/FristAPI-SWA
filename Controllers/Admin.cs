using Microsoft.AspNetCore.Mvc;

namespace Order.Controllers
{
    public class Admin : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
