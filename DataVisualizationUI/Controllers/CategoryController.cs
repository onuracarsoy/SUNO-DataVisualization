using DataVisualizationUI.Dtos;
using DataVisualizationUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataVisualizationUI.Controllers
{
    public class CategoryController(CategoryService categoryService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> CategoryList(string search = "")
        {
            ViewBag.Search = search;
            var categories = await categoryService.CategoryListAsync(search);
            return View(categories);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await categoryService.CreateCategoryAsync(dto);
            return RedirectToAction("CategoryList");
        }

        public async Task<IActionResult> DeleteCategory(int id)
        {
            await categoryService.DeleteCategoryAsync(id);
            return RedirectToAction("CategoryList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            var category = await categoryService.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await categoryService.UpdateCategoryAsync(dto);
            return RedirectToAction("CategoryList");
        }
    }
}
