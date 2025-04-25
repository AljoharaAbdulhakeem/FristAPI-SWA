namespace Order.Models
{
        public class CreateOrderViewModel
        {
            public int CustomerId { get; set; }
            public int ProductId { get; set; }  // NOT porductId
            public int Quantity { get; set; }

         public List<Product>? Products { get; set; }
        public List<CustomerModel>? Customers { get; set; }
    }
}
