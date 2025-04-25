using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Order.Models
{
    public class Ordermodel
    {
        [Key]
        public int OrderId { get; set; }

        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
        public CustomerModel Customer { get; set; }//should be seem with customer id in table

        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public int Total { get; set; }

        public bool IsDeleted { get; set; } = false;
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
