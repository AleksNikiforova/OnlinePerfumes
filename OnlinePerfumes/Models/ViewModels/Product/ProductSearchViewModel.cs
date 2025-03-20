using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlinePerfumes.Models;

namespace OnlinePerfumes.Models.ViewModels.Product
{
    public class ProductSearchViewModel
    {
        public string Name { get; set; }  // Търсене по име на парфюм
        public IEnumerable<Models.Product> Products { get; set; }
    }
}
