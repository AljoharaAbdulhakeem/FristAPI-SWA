using System.ComponentModel.DataAnnotations.Schema;

namespace Order.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
    }

}
