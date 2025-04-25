namespace Order.Models
{
    public class CustomerOrderDetailsViewModel
    {
        public string ProductName { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public float Total => Price * Quantity;
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
    }

}
