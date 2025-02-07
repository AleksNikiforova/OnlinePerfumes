using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace OnlinePerfumes.Models.ViewModels
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
