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

        public string? ImagePath { get; set; }
        public string? Name { get; set; }
        public int Price { get; set; }
        public int StockQuantity { get; set; }

        public string? CategoryName { get; set; }
        public string? Id { get; set; }
    }
}
