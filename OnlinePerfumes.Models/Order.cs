using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePerfumes.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        [Required]
        public decimal TotalAmount { get; set; }
        public ICollection<OrderProduct> OrderProducts=new List<OrderProduct>();
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string Status { get; set; } = "Изчаква обработка";
    }
}
