using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePerfumes.Models
{
    public class OrderStatusUpdate
    {
        [Key]
        public int Id {  get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public string Status { get; set; }
        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
