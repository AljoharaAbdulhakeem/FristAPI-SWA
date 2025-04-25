using System.ComponentModel.DataAnnotations.Schema;

namespace Order.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [ForeignKey("OrderId")]
        public int OrderId { get; set; }
        public Ordermodel? Order { get; set; }

    }
}
