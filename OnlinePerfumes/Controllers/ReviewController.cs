using Microsoft.AspNetCore.Mvc;
using OnlinePerfumes.Core.IServices;
using OnlinePerfumes.Models;

namespace OnlinePerfumes.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add(int productId)
        {
            var model = new Review
            {
                ProductId = productId
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult>Add(Review review)
        {
            if(ModelState.IsValid) 
            {
                    review.UserId = int.Parse(User.FindFirst("UserId").Value);
                    await _reviewService.Add(review);
                    return RedirectToAction("ProductReviews", new { productId = review.ProductId });
            }
            return View(review);
        }
        [HttpPost]
        public async Task<IActionResult>Delete(int id)
        {
            var review = await _reviewService.GetById(id);
            if(review==null)
            {
                return NotFound();
            }
            var currentUserId = int.Parse(User.FindFirst("UserId").Value);
            if (review.UserId != currentUserId)
            {
                return Forbid();
            }
            await _reviewService.Delete(id);
            return RedirectToAction("ProductReviews", new { productId = review.ProductId });


        }
    }
}
