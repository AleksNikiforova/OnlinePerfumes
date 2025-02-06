using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlinePerfumes.Models
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }

       
        public List<int>? SelectedProductIds { get; set; }

        
        public List<SelectListItem> Products { get; set; }
    }
}
