using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePerfumes.Models
{
    public class Product
    {
        //[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is mandatory")]
        [StringLength(50, ErrorMessage = "Name must be 50 letters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is mandatory")]
        [StringLength(100, ErrorMessage = "Description must be 100 words")]
        public string Aroma { get; set; }
        public string Description {  get; set; }

        [Required(ErrorMessage = "Price is mandatory")]
        [Range(0.01, 100000, ErrorMessage = "Price must be between 0.01 and 100000")]
        public decimal Price { get; set; }
        public int StockQuantity { get; set; } = 0;
        public int Countity { get; set; } = 1;
        public string? ImagePath { get; set; }

        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        public ICollection<OrderProduct> OrderProducts=new List<OrderProduct>();
        public ICollection<CartItem> CartItems { get; set; }

    }
}
