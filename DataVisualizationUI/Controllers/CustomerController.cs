using DataVisualizationUI.Dtos;
using DataVisualizationUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataVisualizationUI.Controllers
{
    public class CustomerController(CustomerService customerService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> CustomerList(int page = 1, string search = "")
        {
            ViewBag.Search = search;
            var results = await customerService.CustomerListAsync(page, search);
            return View(results);
        }

        [HttpGet]
        public IActionResult CreateCustomer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await customerService.CreateCustomerAsync(dto);
            return RedirectToAction("CustomerList");
        }

        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await customerService.DeleteCustomerAsync(id);
            return RedirectToAction("CustomerList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCustomer(int id)
        {
            var result = await customerService.GetByIdCustomerAsync(id);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCustomer(UpdateCustomerDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await customerService.UpdateCustomerAsync(dto);
            return RedirectToAction("CustomerList");
        }
    }
}
