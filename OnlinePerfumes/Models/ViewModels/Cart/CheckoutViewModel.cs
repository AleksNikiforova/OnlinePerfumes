namespace OnlinePerfumes.Models.ViewModels.Cart
{
    public class CheckoutViewModel
    {
        public List<CartItem> CartItems { get; set; }
        public decimal TotalPrice => CartItems.Sum(item => item.Product.Price * item.Quantity);
        //public string ShippingAddress { get; set; }
    }
}
