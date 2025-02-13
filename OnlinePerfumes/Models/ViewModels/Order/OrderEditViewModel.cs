using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlinePerfumes.Models.ViewModels.Order
{
    public class OrderEditViewModel
    {
        public SelectList? ProductsList { get; set; }
        public int ProductId {  get; set; }
        public string OrderDate {  get; set; }
    }
}
