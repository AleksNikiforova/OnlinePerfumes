namespace OnlinePerfumes.Models.ViewModels.Cart
{
    public class CheckoutViewModel
    {
        public List<CartItem> CartItems { get; set; }=new List<CartItem>();
        //public decimal TotalPrice => CartItems.Sum(item => item.Product.Price * item.Quantity);
        public string ShippingAddress { get; set; }
    }
}
