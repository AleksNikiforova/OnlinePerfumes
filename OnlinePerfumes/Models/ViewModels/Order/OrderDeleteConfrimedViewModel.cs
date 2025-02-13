namespace OnlinePerfumes.Models.ViewModels.Order
{
    public class OrderDeleteConfrimedViewModel
    {
        public int Id { get; set; }
        public string ProductName {  get; set; }
        public string CustomerName {  get; set; }
        public string OrderDate {  get; set; }
        public decimal Price {  get; set; }
        public string CategoryName {  get; set; }

    }
}
