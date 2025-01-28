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
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal Price {  get; set; }
        [Required]
        public int StockQuantity { get; set; }
        [Required]
        public DateTime DateAdded { get; set; }
        [Required]
        public string Slug {  get; set; }
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<OrderProduct> Orders=new List<OrderProduct>();
        public ICollection<Review> Reviews=new List<Review>();
    }
}
