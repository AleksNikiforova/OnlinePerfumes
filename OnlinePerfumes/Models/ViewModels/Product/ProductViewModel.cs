using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlinePerfumes.Models.ViewModels.Product
{
    public class ProductViewModel
    {


        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Price is mandatory")]
        [Range(0.01, 100000)]
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        [Required(ErrorMessage = "Aroma is mandatory")]
        [StringLength(200)]
        public string Aroma { get; set; }
        [Required(ErrorMessage = "Description is mandatory")]
        public string Description { get; set; }
        [Required]
        public int? CategoryId { get; set; }
        public string? ImagePath { get; set; }
        public IFormFile? ImageFile { get; set; }
        public IEnumerable<SelectListItem>? Categories { get; set; }


    }
}
