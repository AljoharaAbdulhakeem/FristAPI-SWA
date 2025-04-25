using Microsoft.AspNetCore.Mvc;

namespace Order.Controllers
{
    public class user : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
