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
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public decimal Price { get; set; }
        public string Aroma { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public string? ImagePath { get; set; }
        public IEnumerable<SelectListItem>? Categories { get; set; }


    }
}
