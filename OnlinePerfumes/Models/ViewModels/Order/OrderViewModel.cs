using Microsoft.AspNetCore.Mvc.Rendering;
using OnlinePerfumes.Models.ViewModels.Product;
using System.ComponentModel.DataAnnotations;

namespace OnlinePerfumes.Models.ViewModels.Order
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int TotalAmount { get; set; }
        public string ShippingAddress { get; set; }
        public List<int>? SelectedProductIds { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
