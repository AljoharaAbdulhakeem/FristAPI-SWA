using Microsoft.AspNetCore.Mvc;

namespace Order.Controllers
{
    public class Admin : Controller
    {
        //Presnt The Defulte Page For Amdin
        public IActionResult Index()
        {
            return View();
        }
    }
}
