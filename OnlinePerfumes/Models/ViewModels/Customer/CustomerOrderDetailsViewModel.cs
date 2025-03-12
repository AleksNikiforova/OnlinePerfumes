using OnlinePerfumes.Models.ViewModels.OrderProduct;

namespace OnlinePerfumes.Models.ViewModels.Customer
{
    public class CustomerOrderDetailsViewModel
    {
        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }

        public string CustomerName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

       // public string Phone { get; set; }

        public decimal TotalAmount
        {
            get 
            {
                return OrderProducts.Sum(op => op.Price * op.Quantity);
            }

        }

        public List<OrderProductViewModel> OrderProducts { get; set; } = new List<OrderProductViewModel>();
        public string Status { get; set; } = "Изчаква обработка";

    }
}

