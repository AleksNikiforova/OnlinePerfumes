using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlinePerfumes.Models.ViewModels.Order
{
    public class OrderAddViewModel
    {
        public SelectList? ProductsList { get; set; }
        public int ProductId {  get; set; }
        public string OrderDate {  get; set; }
        public SelectList? CustomerList { get; set; }
        public int CustomerId { get; set; }
    }
}
