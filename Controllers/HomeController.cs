using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Order.Data;
using Order.Models;

namespace Order.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly AppDbContext _db;
        public HomeController(ILogger<HomeController> logger, AppDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        // Rout to Presnt Two Button User \ Admin 
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        // Rout to The page For Create New Customer 
        [HttpGet]
        public IActionResult CreateNewCustomer()
        {
            return View();
        }

        //Post Info From page "Create New Customer To the Database And Routing to The index pgae

        [HttpPost]
        public IActionResult CreateNewCustomer([Bind( "Name", "ContactInfo")] CustomerModel NewCustomer)
        {
            
            _db.Customer.Add(NewCustomer);
            _db.SaveChanges();

            TempData["SuccessMessage"] = "Added Successfully";

            return RedirectToAction("Index");
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
