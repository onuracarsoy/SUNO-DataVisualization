using DataVisualizationUI.Context;
using DataVisualizationUI.Models.DashboardViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.Text;

namespace DataVisualizationUI.Services
{
    public class DashboardService(DataVisualizationDbContext context, Kernel kernel)
    {


        // Financial Metrics Method (Finansal Metrikler Metodu)
        public async Task<FinancialMetricsViewModel> GetFinancialMetricsAsync()
        {
            // Fetch data (Verileri getir)
            var totalRevenue = context.Orders.Sum(x => x.Products.UnitPrice * x.Quantity);
            var totalOrders = context.Orders.Count();
            var totalCustomers = context.Customers.Count();

            // Calculate metrics (Metrikleri hesapla)
            var model = new FinancialMetricsViewModel
            {
                TotalRevenue = totalRevenue,
                TotalProfit = totalRevenue * 0.35m, // Simulated 35% profit margin
                TotalOrders = totalOrders,
                TotalCustomers = totalCustomers,
                MonthlyRevenue = totalRevenue, // For now, assumed as total since we don't have date filters yet 
                AverageBasketAmount = totalOrders > 0 ? totalRevenue / totalOrders : 0
            };

            return model;
        }

        // AI Advice Method (Yapay Zeka Tavsiye Metodu)
        public async Task<string> GetAIAdviceAsync()
        {

            var stats = await GetStatsForAIAdvice();
            try
            {
                // Get chat completion service from the injected Kernel (Inject edilen Kernel'dan chat completion service al)
                var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

                // Create prompt (Prompt oluştur)
                var prompt = CreatePrompt(stats);

                // Chat settings (Sohbet ayarları)
                var settings = new OpenAIPromptExecutionSettings
                {
                    MaxTokens = 150,
                    Temperature = 0.7,
                    TopP = 1
                };

                // Create chat history (Chat history oluştur)
                var chatHistory = new ChatHistory();
                chatHistory.AddSystemMessage("Sen bir iş zekası asistanısın. E-ticaret dashboard verilerini analiz edip kısa, öz ve eyleme dönüştürülebilir Türkçe tavsiyeler veriyorsun. Yanıtların maksimum 2-3 cümle olmalı.");
                chatHistory.AddUserMessage(prompt);

                // Get response from AI (AI'dan yanıt al)
                var result = await chatCompletionService.GetChatMessageContentAsync(
                    chatHistory,
                    executionSettings: settings,
                    kernel: kernel
                );

                return result?.Content ?? "AI'dan yanıt alınamadı.";
            }
            catch (HttpRequestException httpEx)
            {
                return $"Teknik bir hata oluştu.";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AI Advice: {ex.Message}");
                return $"Teknik bir hata oluştu.";
            }
        }

        #region Helper Methods of AI Advice Method

        private async Task<DashboardStatsForAIAdviceViewModel> GetStatsForAIAdvice()
        {
            var totalCustomers = await context.Customers.CountAsync();
            var totalProducts = await context.Products.CountAsync();
            var totalOrders = await context.Orders.CountAsync();

            var totalRevenue = await context.Orders
                .Include(o => o.Products)
                .Where(o => o.Products != null)
                .SumAsync(o => o.Quantity * o.Products.UnitPrice);

            var avgOrderValue = totalOrders > 0 ? totalRevenue / totalOrders : 0;

            var lowStockProducts = await context.Products
                .Where(p => p.StockQuantity < 10)
                .CountAsync();

            var recentOrdersCount = await context.Orders
                .Where(o => o.OrderDate >= DateTime.Now.AddDays(-7))
                .CountAsync();

            var topProduct = await context.Products
                .Include(p => p.Orders)
                .OrderByDescending(p => p.Orders.Count)
                .Select(p => p.ProductName)
                .FirstOrDefaultAsync();

            return new DashboardStatsForAIAdviceViewModel
            {
                TotalCustomers = totalCustomers,
                TotalProducts = totalProducts,
                TotalOrders = totalOrders,
                TotalRevenue = totalRevenue,
                AverageOrderValue = avgOrderValue,
                LowStockProductsCount = lowStockProducts,
                RecentOrdersCount = recentOrdersCount,
                TopProductName = topProduct ?? "Henüz sipariş yok"
            };
        }

        private string CreatePrompt(DashboardStatsForAIAdviceViewModel stats)
        {

            var sb = new StringBuilder();
            sb.AppendLine("Aşağıdaki e-ticaret dashboard verilerini analiz et ve kısa bir tavsiye sun:");
            sb.AppendLine();
            sb.AppendLine($"📊 Toplam Müşteri: {stats.TotalCustomers:N0}");
            sb.AppendLine($"📦 Toplam Ürün: {stats.TotalProducts:N0}");
            sb.AppendLine($"🛒 Toplam Sipariş: {stats.TotalOrders:N0}");
            sb.AppendLine($"💰 Toplam Gelir: {stats.TotalRevenue:C0}");
            sb.AppendLine($"📈 Ortalama Sipariş Değeri: {stats.AverageOrderValue:C0}");
            sb.AppendLine($"⚠️ Düşük Stoklu Ürünler: {stats.LowStockProductsCount}");
            sb.AppendLine($"🕐 Son 7 Gündeki Siparişler: {stats.RecentOrdersCount}");
            sb.AppendLine($"🏆 En Çok Satan Ürün: {stats.TopProductName}");
            sb.AppendLine();
            sb.AppendLine("Profesyonel ve eyleme dönüştürülebilir bir öngörü veya tavsiye ver (maksimum 2-3 cümle).");

            return sb.ToString();
        }



        #endregion

        // Graphics Data Method (Grafik Verileri Metodu)
        public async Task<DashboardGraphicsViewModel> GetDashboardGraphDataAsync()
        {
            // 1. Line Chart: Last 12 Months (Yearly Summary) (1. Çizgi Grafik: Son 12 Ay (Yıllık Özet))
            var today = DateTime.Today;
            var oneYearAgo = today.AddMonths(-11).AddDays(-today.Day + 1); // Start from first day of the month 11 months ago

            var ordersLast12Months = context.Orders
                .Where(o => o.OrderDate >= oneYearAgo)
                .GroupBy(o => new { o.OrderDate.Year, o.OrderDate.Month })
                .Select(g => new { Year = g.Key.Year, Month = g.Key.Month, Count = g.Count() })
                .ToList();

            var chartLabels = new List<string>();
            var chartValues = new List<int>();

            for (int i = 0; i < 12; i++)
            {
                var currentMonth = oneYearAgo.AddMonths(i);
                var orderData = ordersLast12Months.FirstOrDefault(o => o.Year == currentMonth.Year && o.Month == currentMonth.Month);

                chartLabels.Add(currentMonth.ToString("MMMM", new System.Globalization.CultureInfo("tr-TR")));
                chartValues.Add(orderData?.Count ?? 0);
            }

            // 1.5. Bar Chart: Last 5 Years Summary (1.5. Çubuk Grafik: Son 5 Yıl Özeti)
            var fiveYearsAgo = today.AddYears(-4); // Current year + previous 4 years
            var ordersLast5Years = context.Orders
                 .Where(o => o.OrderDate.Year >= fiveYearsAgo.Year)
                 .GroupBy(o => o.OrderDate.Year)
                 .Select(g => new { Year = g.Key, Count = g.Count() })
                 .ToList();

            var fiveYearLabels = new List<string>();
            var fiveYearValues = new List<int>();

            for (int i = 0; i < 5; i++)
            {
                var year = fiveYearsAgo.Year + i;
                var orderData = ordersLast5Years.FirstOrDefault(o => o.Year == year);

                fiveYearLabels.Add(year.ToString());
                fiveYearValues.Add(orderData?.Count ?? 0);
            }

            // 2. Pie Chart: Top 5 Categories by Product Count (Proxy for performance for now) (2. Pasta Grafik: Ürün Sayısına Göre İlk 5 Kategori (Şimdilik performans temsili))
            var topCategories = context.Products
                .Include(p => p.Category)
                .GroupBy(p => p.Category.CategoryName)
                .Select(g => new { Category = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Take(5)
                .ToList();

            var topCategoryNames = topCategories.Select(x => x.Category).ToList();
            var topCategoryProductCounts = topCategories.Select(x => x.Count).ToList();

            // 3. Doughnut Chart: Payment Method Distribution (3. Halka Grafik: Ödeme Yöntemi Dağılımı)
            var paymentMethodsData = context.Orders
                .GroupBy(o => o.PaymentMethod)
                .Select(g => new { Method = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .ToList();

            var paymentMethods = paymentMethodsData.Select(x => x.Method).ToList();
            var paymentMethodCounts = paymentMethodsData.Select(x => x.Count).ToList();

            return new DashboardGraphicsViewModel
            {
                ChartLabels = chartLabels,
                ChartValues = chartValues,
                FiveYearLabels = fiveYearLabels,
                FiveYearValues = fiveYearValues,
                TopCategoryNames = topCategoryNames,
                TopCategoryProductCounts = topCategoryProductCounts,
                PaymentMethods = paymentMethods,
                PaymentMethodCounts = paymentMethodCounts
            };
        }

   
        // Map Data Method (Harita Verileri Metodu)
        public async Task<DashboardMapViewModel> GetDashboardMapDataAsync()
        {
            // Get Top 5 Countries by Order Count (Sipariş Sayısına Göre İlk 5 Ülkeyi Getir)
            var topCountriesData = context.Orders
                .Include(o => o.Customers)
                .GroupBy(o => o.Customers.CustomerCountry)
                .Select(g => new
                {
                    CountryName = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
                .Take(5)
                .ToList();

            var topCountriesList = new List<MapLocationItem>();

            foreach (var item in topCountriesData)
            {
                var coords = GetCoordinatesForCountry(item.CountryName);
                topCountriesList.Add(new MapLocationItem
                {
                    CountryName = item.CountryName,
                    OrderCount = item.Count,
                    Latitude = coords.lat,
                    Longitude = coords.lng
                });
            }

            return new DashboardMapViewModel
            {
                 TopCountries = topCountriesList
            };
        }

        #region Helper Methods of Map Data Method
        private (double lat, double lng) GetCoordinatesForCountry(string countryName)
        {
            // Simple static mapping for demo purposes (Demo amaçlı basit statik eşleştirme)
            // In a real app, use a Geocoding API or a Database table (Gerçek bir uygulamada Geocoding API veya bir veritabanı tablosu kullanın)
            var mappings = new Dictionary<string, (double, double)>
            {
                { "Turkey", (38.9637, 35.2433) },
                { "Türkiye", (38.9637, 35.2433) },
                { "USA", (37.0902, -95.7129) },
                { "ABD", (37.0902, -95.7129) },
                { "United States", (37.0902, -95.7129) },
                { "Germany", (51.1657, 10.4515) },
                { "Almanya", (51.1657, 10.4515) },
                { "France", (46.2276, 2.2137) },
                { "Fransa", (46.2276, 2.2137) },
                { "UK", (55.3781, -3.4360) },
                { "United Kingdom", (55.3781, -3.4360) },
                { "İngiltere", (55.3781, -3.4360) },
                { "Italy", (41.8719, 12.5674) },
                { "İtalya", (41.8719, 12.5674) },
                { "Spain", (40.4637, -3.7492) },
                { "İspanya", (40.4637, -3.7492) },
                { "Netherlands", (52.1326, 5.2913) },
                { "Hollanda", (52.1326, 5.2913) },
                { "Russia", (61.5240, 105.3188) },
                { "Rusya", (61.5240, 105.3188) },
                { "China", (35.8617, 104.1954) },
                { "Çin", (35.8617, 104.1954) },
                { "Japan", (36.2048, 138.2529) },
                { "Japonya", (36.2048, 138.2529) }
            };

            if (mappings.TryGetValue(countryName, out var coords))
            {
                return coords;
            }

            // Default to 0,0 (Atlantic Ocean) if unknown (Bilinmiyorsa 0,0 (Atlantik Okyanusu) varsayılanına dön)
            return (0, 0);

        }
        #endregion


        // Lists Method (Liste Metodu)
        public async Task<DashboardListsViewModel> GetDashboardListsAsync()
        {
            // 1. Top Selling Products (Top 5) (1. En Çok Satan Ürünler (İlk 5))
            // Fix: Split into two queries for EF Core translation compatibility

            // Step A: Get ProductIds and TotalSold count (A Adımı: Ürün Kimliklerini ve Toplam Satış sayısını al)
            var topSellingStats = context.Orders
                .GroupBy(o => o.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    TotalSold = g.Sum(o => o.Quantity)
                })
                .OrderByDescending(x => x.TotalSold)
                .Take(5)
                .ToList();

            // Step B: Fetch details for these products (B Adımı: Bu ürünlerin detaylarını getir)
            var productIds = topSellingStats.Select(x => x.ProductId).ToList();

            var products = context.Products
                .Include(p => p.Category)
                .Where(p => productIds.Contains(p.ProductId))
                .ToList();

            // Step C: Merge in memory (C Adımı: Bellekte birleştir)
            var topSellingProducts = topSellingStats.Select(stat =>
            {
                var product = products.FirstOrDefault(p => p.ProductId == stat.ProductId);
                return new TopSellingProductItem
                {
                    ProductName = product != null ? product.ProductName : "Unknown",
                    CategoryName = product != null && product.Category != null ? product.Category.CategoryName : "-",
                    ImageUrl = product != null ? product.ProductImageUrl : "",
                    UnitPrice = product != null ? product.UnitPrice : 0,
                    TotalSold = stat.TotalSold,
                    // Assuming revenue is based on current price as per original query logic
                    TotalRevenue = stat.TotalSold * (product != null ? product.UnitPrice : 0)
                };
            }).ToList();

            // 2. Low Stock Products (Top 5 Critical) (2. Düşük Stoklu Ürünler (Kritik İlk 5))
            var lowStockProducts = context.Products
                .Where(p => p.StockQuantity < 20)
                .OrderBy(p => p.StockQuantity) // Lowest stock first
                .Take(5)
                .Select(p => new LowStockProductItem
                {
                    ProductName = p.ProductName,
                    ImageUrl = p.ProductImageUrl,
                    StockQuantity = p.StockQuantity,
                    CategoryId = p.CategoryId
                })
                .ToList();

            return new DashboardListsViewModel
            {
                TopSellingProducts = topSellingProducts,
                LowStockProducts = lowStockProducts
            };

        }
    }
}
