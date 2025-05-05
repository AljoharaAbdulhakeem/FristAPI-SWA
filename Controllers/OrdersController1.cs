using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Order.Data;
using Order.Models;
using static Order.Models.CreateOrderViewModel;

namespace Order.Controllers
{
    public class OrdersController1 : Controller
    {

        private readonly AppDbContext _db;
        public OrdersController1(AppDbContext db)
        {
            _db = db;
        }

        // seem with OrdersbyCustomerId  
        public IActionResult Index(int ? id)
        {
            var orders = _db.Order.Where(a => a.CustomerId == id).ToList();            


            if(id==null || orders == null)
            {
                return NotFound();
            }
            ViewData["orders"] = orders;
            ViewBag.OrderCount = orders.Count;
            return View(orders);
        }

        // GET : routing to the Loockup view 
        public IActionResult Lookup()
        {
            return View();
        }

        // POST : send the info abut customer to the database
        [HttpPost]
        public IActionResult Lookup(CustomerLookupViewModel model)
        {
            var customer = _db.Customer.FirstOrDefault(c => c.Name == model.Name);

            if (customer == null)
            {
                ModelState.AddModelError("", "Customer Not Found");
                return View(model);
            }

            return RedirectToAction("OrdersbyCustomerId", new { id = customer.Id });
        }

        // list all orders for specific Customer
        public IActionResult OrdersbyCustomerId(int? id)
        {
            var orders = _db.Order.Where(o => o.CustomerId == id && !o.IsDeleted).ToList();
            //var orders = _db.Order.Where(a => a.CustomerId == id).ToList();
            if (id == null || orders == null)
            {
                return NotFound();
            }
            ViewData["orders"] = orders;
            ViewBag.OrderCount = orders.Count;
            return View(orders);

        }


        // List All Orders (Admin) and ignore The delted customer 
        public IActionResult AllOrders()
        {
            //var orders = _db.Order.ToList();
            var orders = _db.Order.Include(o => o.Customer).Where(o => !o.Customer.IsDeleted).ToList();

            ViewData["orders"] = orders;
            ViewBag.OrderCount = orders.Count;
            return View(orders);
        }

        //GET: / Orders / Create
        public IActionResult Create()
        {
            var products = _db.Products.ToList();
            var customers = _db.Customer.ToList();

            var model = new CreateOrderViewModel
            {
                Products = products,
                Customers = customers
            };

            return View(model); 
        }



        // customer : creat new order 
        [HttpPost]
        public IActionResult Create(Models.CreateOrderViewModel model)
        {
            // Get the Product from table (Products)
            var item = _db.Products.FirstOrDefault(i => i.Id == model.ProductId); 

            if (item == null)
            {
                // lis t producta and customer 
                model.Products = _db.Products.ToList();
                model.Customers = _db.Customer.ToList();

                return View("Item Not Found");
            }

            //Check the Quantity 
            if (item.Quantity < model.Quantity)
            {
              
                model.Products = _db.Products.ToList();
                model.Customers = _db.Customer.ToList();
                return View("NotEnoughQuantity");

            }


            var newOrder = new Ordermodel
            {
                CustomerId = model.CustomerId,
                OrderDate = DateTime.Now,
                Status = "Pending", 
                Total = 0
            };

            // To save the changes and reflixing on the database , to connect (ordermodelid and orderitems)
            _db.Order.Add(newOrder);
            _db.SaveChanges();  
            

            var orderItem = new OrderItem
            {
                OrderId = newOrder.OrderId,
                ProductId = model.ProductId,
                Quantity = model.Quantity
            };




            _db.OrderItems.Add(orderItem);
            item.Quantity -= model.Quantity;


            _db.SaveChanges();

            return RedirectToAction("OrdersbyCustomerId", "OrdersController1", new { id = model.CustomerId });
        }


        // list all orders and edit the status / id
        // post : /porduct/edit/id
        [HttpGet]
        public IActionResult edit(int? id ) // id = OrdermodelId
        {

            //var order = _db.Order.FirstOrDefault(p => p.OrderId == id);
            var order = _db.Order.ToList().Find(p => p.OrderId == id);

            if (order == null)
            {
                return View("NotFound");
            }

            return View(order); 
        }

        // Reflix the status on database
        [HttpPost]
        public IActionResult edit(int id, string status)
        {
            var existingOrder = _db.Order.FirstOrDefault(o => o.OrderId == id);

            if (existingOrder == null)
            {
                return NotFound();
            }
            existingOrder.Status = status;

            _db.SaveChanges();

            TempData["SuccessMessage"] = "Status updated successfully";

            return RedirectToAction("AllOrders");
        }

        // List Ordre Details for customer and calculate the total  
        public IActionResult OrderDetails(int customerId)
        {
            var customer = _db.Customer
                .Include(c => c.Orders)
                    .ThenInclude(o => o.Items)
                        .ThenInclude(i => i.Product)
                .FirstOrDefault(c => c.Id == customerId);

            if (customer == null)
            {
                return View("NotFound");
            }

            var details = customer.Orders.SelectMany(order => order.Items.Select(item => new CustomerOrderDetailsViewModel
            {
                ProductName = item.Product.Name,
                Price = item.Product.Price,
                Quantity = item.Quantity,
                OrderDate = order.OrderDate,
                Status = order.Status
            })).ToList();

            ViewBag.CustomerName = customer.Name;
            ViewBag.Total = details.Sum(d => d.Total);

            return View(details);
        }

        // Delet order ( Admin )
        [HttpPost]
        public IActionResult DeleteOrder(int id)
        {
            var order = _db.Order.ToList().FirstOrDefault(p => p.OrderId == id);

            _db.Order.Remove(order);
            _db.SaveChanges();
            TempData["SuccessDEL"] = "Delet Successfully";
            return RedirectToAction("AllOrders");
        }

        // delete order customer (User) soft deleted 
        [HttpPost]
        public IActionResult DeleteOrderCustomer(int orderID)
        {
            var order = _db.Order.ToList().FirstOrDefault(p => p.OrderId == orderID);

            if (order == null)
            {
                return View("NotFound");
            }


            order.IsDeleted = true;
            _db.SaveChanges();
            TempData["SuccessDEL"] = "Delet Successfully";
            return RedirectToAction("OrdersbyCustomerId" , "OrdersController1", new { id = order.OrderId });
        }








    }
}
