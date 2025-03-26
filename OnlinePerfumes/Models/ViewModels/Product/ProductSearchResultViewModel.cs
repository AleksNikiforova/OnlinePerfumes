namespace OnlinePerfumes.Models.ViewModels.Product
{
    public class ProductSearchResultViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string Aroma { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }
        public int StockQuantity { get; set; }
    }
}
