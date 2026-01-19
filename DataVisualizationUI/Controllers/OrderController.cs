using AutoMapper;
using DataVisualizationUI.Dtos;
using DataVisualizationUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataVisualizationUI.Controllers
{
    public class OrderController(OrderService orderService, IMapper mapper) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> OrderList(int page = 1, string search = "")
        {
            ViewBag.Search = search;
            var model = await orderService.OrderListAsync(page, search);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DetailOrder(int id)
        {
            var order = await orderService.GetOrderByIdAsync(id);
            if (order == null)
                return RedirectToAction("OrderList");

            if (order.Products != null)
            {
                ViewBag.ProductName = order.Products.ProductName;
                ViewBag.ProductDescription = order.Products.ProductDescription;
                ViewBag.ProductImageUrl = order.Products.ProductImageUrl;
                ViewBag.ProductPrice = order.Products.UnitPrice;
                ViewBag.ProductStock = order.Products.StockQuantity;
            }

            if (order.Customers != null)
            {
                ViewBag.CustomerName = order.Customers.CustomerName;
                ViewBag.CustomerSurname = order.Customers.CustomerSurname;
                ViewBag.CustomerCity = order.Customers.CustomerCity;
                ViewBag.CustomerCountry = order.Customers.CustomerCountry;
                ViewBag.CustomerEmail = order.Customers.CustomerEmail;
                ViewBag.CustomerPhone = order.Customers.CustomerPhone;
                ViewBag.CustomerAddress = order.Customers.CustomerAddress;
            }

            var dto = mapper.Map<OrderDto>(order);
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> CreateOrder()
        {
            ViewBag.Customers = await orderService.GetAllCustomersAsync();
            ViewBag.Products = await orderService.GetAllProductsAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Customers = await orderService.GetAllCustomersAsync();
                ViewBag.Products = await orderService.GetAllProductsAsync();
                return View(dto);
            }

            await orderService.CreateOrderAsync(dto);
            return RedirectToAction("OrderList");
        }

        public async Task<IActionResult> DeleteOrder(int id)
        {
            await orderService.DeleteOrderAsync(id);
            return RedirectToAction("OrderList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateOrder(int id)
        {
            var order = await orderService.GetOrderByIdAsync(id);
            if (order == null)
                return RedirectToAction("OrderList");

            ViewBag.Customers = await orderService.GetAllCustomersAsync();
            ViewBag.Products = await orderService.GetAllProductsAsync();

            ViewBag.CustomerName = order.Customers != null
                ? $"{order.Customers.CustomerName} {order.Customers.CustomerSurname}"
                : null;
            ViewBag.ProductName = order.Products != null
                ? $"{order.Products.ProductName} (Stok: {order.Products.StockQuantity})"
                : null;

            var dto = mapper.Map<UpdateOrderDto>(order);
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrder(UpdateOrderDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Customers = await orderService.GetAllCustomersAsync();
                ViewBag.Products = await orderService.GetAllProductsAsync();

                var order = await orderService.GetOrderByIdAsync(dto.OrderId);
                ViewBag.CustomerName = order?.Customers != null
                    ? $"{order.Customers.CustomerName} {order.Customers.CustomerSurname}"
                    : null;
                ViewBag.ProductName = order?.Products != null
                    ? $"{order.Products.ProductName} (Stok: {order.Products.StockQuantity})"
                    : null;

                return View(dto);
            }

            await orderService.UpdateOrderAsync(dto);
            return RedirectToAction("OrderList");
        }
    }
}
