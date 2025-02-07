using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlinePerfumes.Models.ViewModels
{
    public class ProductViewModel
    {
        
        
            public int Id { get; set; }  // Product ID
            public string Name { get; set; }  // Product name
            public string Description { get; set; } = string.Empty;  // Product description
            public decimal Price { get; set; }  // Product price
            public int Quantity { get; set; }  // Quantity of the product available
            public DateTime DateAdded { get; set; } = DateTime.Now;  // Date the product was added
            public string Slug { get; set; } = "";  // URL-friendly name for the product
            public int CategoryId { get; set; }  // Category ID the product belongs to
            public string CategoryName { get; set; }  // Category name
        

    }
}
