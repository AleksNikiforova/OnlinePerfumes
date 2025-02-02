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
        [Key]
        public int Id {  get; set; }
        [Required(ErrorMessage = "Name is mandatory")]
        [StringLength(50,ErrorMessage ="Name must be 50 letters")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Description is mandatory")]
        [StringLength(100,ErrorMessage ="Description must be 100 words")]
        public string Description { get; set; }
        [Required(ErrorMessage ="Price is mandatory")]
        [Range(0.01,100000,ErrorMessage ="Price must be between 0.01 and 100000")]
        public decimal Price {  get; set; }
        [Required]
        public int StockQuantity { get; set; } = 0;
        [Required]
        public DateTime DateAdded { get; set; } = DateTime.Now;
        [Required]
        public string Slug { get; set; } = "";
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; } 
        public Category Category { get; set; }
        public ICollection<OrderProduct> OrderProducts=new List<OrderProduct>();
        public ICollection<Review> Reviews=new List<Review>();
    }
}
