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
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id {  get; set; }
        [Required(ErrorMessage = "Name is mandatory")]
        [StringLength(50,ErrorMessage ="Name must be 50 letters")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Description is mandatory")]
        [StringLength(100,ErrorMessage ="Description must be 100 words")]
        public string Aroma { get; set; }
        [Required(ErrorMessage ="Price is mandatory")]
        [Range(0.01,100000,ErrorMessage ="Price must be between 0.01 and 100000")]
        public decimal Price {  get; set; }
        public string? ImagePath {  get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<OrderProduct> OrderProducts=new List<OrderProduct>();
       
    }
}
