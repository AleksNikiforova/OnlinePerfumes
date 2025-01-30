using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlinePerfumes.Models
{
    public class ProductFilterViewModel
    {
        public int? CategoryId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set;}
        public string? SearchName {  get; set; }
        public SelectList Categories { get; set; }
        public List<Product> Products { get; set; }
    }
}
