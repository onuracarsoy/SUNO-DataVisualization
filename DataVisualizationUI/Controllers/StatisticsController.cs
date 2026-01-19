using DataVisualizationUI.Context;
using Microsoft.AspNetCore.Mvc;

namespace DataVisualizationUI.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly DataVisualizationDbContext _context;

        public StatisticsController(DataVisualizationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.CategoryCount = _context.Categories.Count();
            ViewBag.ProductCount = _context.Products.Count();
            ViewBag.OrderCount = _context.Orders.Count();
            ViewBag.CustomerCount = _context.Customers.Count();

            ViewBag.CompletedOrders = _context.Orders.Count(o => o.OrderStatus == "Tamamlandı");
            ViewBag.CanceledOrders = _context.Orders.Count(o => o.OrderStatus == "İptal Edildi");
            ViewBag.CargoOrders = _context.Orders.Count(o => o.OrderStatus == "Kargoda");
            ViewBag.WaitingOrders = _context.Orders.Count(o => o.OrderStatus == "Beklemede");
            ViewBag.PrepareOrders = _context.Orders.Count(o => o.OrderStatus == "Hazırlanıyor");


            ViewBag.PopCategory = _context.Products
              .GroupBy(p => p.Category.CategoryName)
              .OrderByDescending(g => g.Count())
              .Select(g => g.Key)
              .FirstOrDefault();

            ViewBag.PopPaymentMethod = _context.Orders
                .GroupBy(o => o.PaymentMethod)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault();

            ViewBag.PopProduct = _context.Orders
                .GroupBy(o => o.Products.ProductName)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault();

            ViewBag.TopCountry = _context.Orders
            .GroupBy(o => o.Customers.CustomerCountry)
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key)
            .FirstOrDefault();


            ViewBag.CreditCardCount = _context.Orders.Count(o => o.PaymentMethod == "Kredi Kartı");
            ViewBag.BankCardCount = _context.Orders.Count(o => o.PaymentMethod == "Banka Kartı");
            ViewBag.PayPalCount = _context.Orders.Count(o => o.PaymentMethod == "PayPal");
            ViewBag.BankTransferCount = _context.Orders.Count(o => o.PaymentMethod == "Havale / EFT");
            ViewBag.GooglePayCount = _context.Orders.Count(o => o.PaymentMethod == "Google Pay");
            ViewBag.ApplePayCount = _context.Orders.Count(o => o.PaymentMethod == "Apple Pay");
            ViewBag.DoorPayCount = _context.Orders.Count(o => o.PaymentMethod == "Kapıda Ödeme");


            return View();
        }
    }
}
