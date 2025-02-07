using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlinePerfumes.Models.ViewModels
{
    public class ProductViewModel
    {
        public string Name { get; set; }=string.Empty;
        
        public string Description { get; set; } = string.Empty;
        
        public decimal Price { get; set; }
        
        public int StockQuantity { get; set; } = 0;
        
        public DateTime DateAdded { get; set; } = DateTime.Now;
        
        public string Slug { get; set; } = "";
       
        public List<int>? SelectedOrderIds { get; set; }
        public List<SelectListItem>Orders { get; set; }
    }
}
