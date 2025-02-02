using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePerfumes.Models
{
    public class OrderStatus
    {
        [Key]
        public int Id {  get; set; }
        [Required]
        public string StatusDescription {  get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<OrderStatusUpdate> OrderStatusUpdates { get; set; }

    }
}
