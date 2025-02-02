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
        public DateTime OrderDate { get; set; }
        [Required]
        public int TotalAmount { get; set; }
        [Required]
        public string ShippingAddress {  get; set; }
        [ForeignKey(nameof(OrderStatus))]
        public int OrderStatusId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        [ForeignKey(nameof(Payment))]
        public int OrderPaymentId { get; set; }
        public ICollection<OrderProduct> OrderProducts=new List<OrderProduct>();
        public ICollection<OrderStatusUpdate> StatusUpdates { get; set; }
    }
}
