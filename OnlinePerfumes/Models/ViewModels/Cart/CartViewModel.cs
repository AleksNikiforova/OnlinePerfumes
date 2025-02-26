namespace OnlinePerfumes.Models.ViewModels.Cart
{
    public class CartViewModel
    {
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();//Списък от продукти
        public decimal TotalPrice
        {
            get
            {
                return CartItems.Sum(item => item.Quantity * item.Product.Price);
            }
        }

    }
}

