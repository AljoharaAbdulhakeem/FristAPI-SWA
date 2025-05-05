using Microsoft.AspNetCore.Mvc;

namespace Order.Controllers
{
    public class user : Controller
    {
        // presnt The Defulte Page For User 
        public IActionResult Index()
        {
            return View();
        }
    }
}
