using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlinePerfumes.Models.ViewModels.Product
{
    public class ProductFilterViewModel
    {
        public int? CategoryId { get; set; } // Категория
        public decimal? MinPrice { get; set; } // Минимална цена
        public decimal? MaxPrice { get; set; } // Максимална цена
        public string? Aroma { get; set; } // Аромат


        public SelectList Categories { get; set; } // За попълване на падащото меню
        public List<Models.Product> Products { get; set; }
        public List<ProductAllViewModel> ProductsAll { get; set; } = new List<ProductAllViewModel>();
    }
}
