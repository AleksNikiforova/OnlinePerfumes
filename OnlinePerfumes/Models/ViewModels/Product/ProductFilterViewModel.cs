using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlinePerfumes.Models;

namespace OnlinePerfumes.Models.ViewModels.Product
{
    public class ProductFilterViewModel
    {
        public int? CategoryId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? Aroma { get; set; }
        public SelectList Categories { get; set; }
        public List<Models.Product> Products { get; set; }
    }
}
