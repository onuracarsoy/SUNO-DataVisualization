using DataVisualizationUI.Dtos;
using DataVisualizationUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataVisualizationUI.Controllers
{
    public class ProductController(ProductService productService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> ProductList(int page = 1, string search = "")
        {
            ViewBag.Search = search;
            var model = await productService.ProductListAsync(page, search);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ReviewOfProductList(int id, int page = 1)
        {
            var product = await productService.GetProductByIdAsync(id);
            ViewBag.ProductName = product?.ProductName ?? "Bilinmeyen Ürün";
            ViewBag.ProductId = id;

            var model = await productService.GetProductReviewsAsync(id, page);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            ViewBag.Categories = await productService.GetAllCategoriesAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await productService.GetAllCategoriesAsync();
                return View(dto);
            }

            await productService.CreateProductAsync(dto);
            return RedirectToAction("ProductList");
        }

        public async Task<IActionResult> DeleteProduct(int id)
        {
            await productService.DeleteProductAsync(id);
            return RedirectToAction("ProductList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            var product = await productService.GetProductForUpdateAsync(id);
            if (product == null)
                return NotFound();

            ViewBag.Categories = await productService.GetAllCategoriesAsync();
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await productService.GetAllCategoriesAsync();
                return View(dto);
            }

            await productService.UpdateProductAsync(dto);
            return RedirectToAction("ProductList");
        }
    }
}
