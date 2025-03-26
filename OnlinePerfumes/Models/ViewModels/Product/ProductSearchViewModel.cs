using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlinePerfumes.Models;

namespace OnlinePerfumes.Models.ViewModels.Product
{
    public class ProductSearchViewModel
    {
        public string Name { get; set; }  // Търсене по име на парфюм
        public decimal Price { get; set; }

        public string Aroma { get; set; }
       // public int CategoryId { get; set; }
        public string Category {  get; set; }
        public IEnumerable<Models.Product> Products { get; set; }

    }
}
