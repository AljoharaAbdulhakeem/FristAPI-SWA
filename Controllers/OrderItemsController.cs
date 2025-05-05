using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Order.Data;

using Order.Models;

namespace OrderItems.Controllers
{
    public class OrderItemsController : Controller
    {
        private readonly AppDbContext _db;
        public OrderItemsController(AppDbContext db)
        {
            _db = db;
        }
        // List All Product 
        //GET /OrderItems
        public IActionResult Index()
        {
            var Items = _db.OrderItems.ToList();
            ViewData["Items"] = Items;
            return View();
        }
        // List All Customer And Ignore any Deleted Customer (Soft Deleted)
        public IActionResult AllCustomer()
        {
            var customer = _db.Customer.Where(c => !c.IsDeleted).ToList();
            ViewData["Customer"] = customer;
            return View();
        }

        // Routing to View To Add New Product 
        public IActionResult CreateNewItem()
        {
            return View();
        }

        // Routing to List All Product And Edit/Delete
        public IActionResult Products()
        {
            var products = _db.Products.ToList();
            ViewData["products"] = products;
            return View();
        }

        //admin:create new item send to the database
        [HttpPost]
        public IActionResult CreateNewItem([Bind( "Name","Description" ,"Price","Quantity")] Order.Models.Product itemNew)
        {
 

            _db.Products.Add(itemNew);
            _db.SaveChanges();

            //itemNew.Id = itemNew.OrderItemId;

            TempData["SuccessMessage"] = "Added Successfully";

            return RedirectToAction("Products");
        }

        // Check Quantity From Database
        [HttpPost]
        public IActionResult CheckQuantity(int itemId, int requestedQuantity)
        {
            var item = _db.Products.FirstOrDefault(i => i.Id == itemId);

            if (item == null)
            {

                return View("NotFound");
            }

            if (item.Quantity <= 0)
            {

                return View("SoldOut");
            }

            if (requestedQuantity > item.Quantity)
            {

                ViewBag.Available = item.Quantity;
                return View("NotEnoughQuantity");
            }


            item.Quantity -= requestedQuantity;
            _db.SaveChanges();

            ViewBag.Taken = requestedQuantity;
            return View("QuantityConfirmed");

        }

        // Delete Product (Admin)
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var order = _db.Products.ToList().FirstOrDefault(p => p.Id == id);

            _db.Products.Remove(order);
            _db.SaveChanges();
            return RedirectToAction("Products");
        }

        // Delet Customer (Admin) Soft Delete
        [HttpPost]
        public IActionResult DeleteCustomer(int id)
        {
            var customer = _db.Customer.FirstOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return View("NotFound");
            }

  
            customer.IsDeleted = true;
            _db.SaveChanges();

            return RedirectToAction("AllCustomer");
        }


        // Rout to the View Edit Item
        public IActionResult editItems(int? id)
        {
            var Items = _db.Products.ToList().Find(p => p.Id == id);
            if (Items == null || id == null)
            {
                return View("NotFound");
            }

            ViewData["Items"] = Items;
            return View("editItems", Items);

        }


        // Reflix the Edit on the dataebase
        [HttpPost]
        public IActionResult editItems(Order.Models.Product edit)
        {
            var existingItem = _db.Products.FirstOrDefault(i => i.Id == edit.Id);

            if (existingItem == null)
            {
                return NotFound(); 
            }

            
            if (edit.Id != 0)
                existingItem.Id = edit.Id;

            if (edit.Name!= null)
                existingItem.Name = edit.Name;

            if (edit.Price != 0)
                existingItem.Price = edit.Price;

            if (edit.Quantity != 0)
                existingItem.Quantity = edit.Quantity;

            _db.SaveChanges();

            TempData["SuccessMessage"] = "Edit Successfully";

            return RedirectToAction("Products");
        }

    }
}