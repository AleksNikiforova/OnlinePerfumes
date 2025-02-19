namespace OnlinePerfumes.Models.ViewModels.Customer
{
    public class CustomerOrderViewModel
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
    }
}
