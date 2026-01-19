using DataVisualizationUI.Dtos;
using DataVisualizationUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataVisualizationUI.Controllers
{
    public class ReviewController(ReviewService reviewService) : Controller
    {
        public async Task<IActionResult> ReviewList(int page = 1, int pageSize = 10, string search = "")
        {
            ViewBag.Search = search;
            var viewModel = await reviewService.ReviewListAsync(page, pageSize, search);
            return View(viewModel);
        }
    }
}
