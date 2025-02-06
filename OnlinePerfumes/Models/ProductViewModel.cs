using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlinePerfumes.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }=string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Price { get; set; }  
        public List<int>? SelectedOrderIds { get; set; }
        public List<SelectListItem>Orders { get; set; }
    }
}
