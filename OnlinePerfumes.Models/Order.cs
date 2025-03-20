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
    public enum OrderStatus
    {
        Oбработка = 0,       // Изчаква потвърждение
        Потвърдена = 1,     // Потвърдена
        Отказана = 2       // Отказана

    }

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
        //public string Status { get; set; }
        [Required]
        public virtual int OrderStatusId
        {
            get
            {
                return (int)this.Status;
            }
            set
            {
                Status = (OrderStatus)value;
            }
        }
        [EnumDataType(typeof(OrderStatus))]
        public OrderStatus Status { get; set; }
    }
}
