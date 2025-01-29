using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePerfumes.Models
{
    public class Category
    {
        [Key]
        public int Id {  get; set; }
        [Required(ErrorMessage ="Category name is mandatory")]
        [StringLength(100,ErrorMessage ="Category name must be 100 words")]
        public string CategoryName { get; set; }
        public ICollection<Product> Products=new List<Product>();
    }
}
