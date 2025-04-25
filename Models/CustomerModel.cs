namespace Order.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactInfo { get; set; }

        public List<Ordermodel> Orders { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
