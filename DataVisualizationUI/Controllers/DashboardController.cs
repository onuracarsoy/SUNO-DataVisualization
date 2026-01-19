using DataVisualizationUI.Context;
using DataVisualizationUI.Endpoints;
using DataVisualizationUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace DataVisualizationUI.Controllers
{
    public class DashboardController(DataVisualizationDbContext context, DashboardService dashboardService) : Controller
    {


        public IActionResult Index()
        {
            return View();
        }

        // SSE Endpoint - Financial Metrics Stream (SSE Uç Noktası - Finansal Metrik Akışı)
        //This method has descriptive comments.
    
        [HttpGet]
        public async Task StreamFinancialMetrics()
        {
            // Headers for SSE (SSE için Üst Bilgiler)
            Response.Headers.Add("Content-Type", "text/event-stream");
            Response.Headers.Add("Cache-Control", "no-cache");
            Response.Headers.Add("Connection", "keep-alive");

            // Close connection when client disconnects (İstemci bağlantısı kesildiğinde bağlantıyı kapat)
            while (!HttpContext.RequestAborted.IsCancellationRequested)
            {
                try
                {
                    // Take data from service (Servisten veriyi al)
                    var metrics = await dashboardService.GetFinancialMetricsAsync();

                    // Convert to JSON with CamelCase policy (CamelCase politikası ile JSON'a dönüştür)
                    var json = JsonSerializer.Serialize(metrics, new JsonSerializerOptions 
                    { 
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
                    });

                    // Send in SSE format (SSE formatında gönder)
                    await Response.WriteAsync($"data: {json}\n\n");
                    await Response.Body.FlushAsync();
                    // Wait 5 seconds before sending next update (Sonraki güncellemeyi göndermeden önce 5 saniye bekle)
                    await Task.Delay(5000);
                }
                catch (Exception)
                {
                    // if any error occurs, exit the loop (herhangi bir hata oluşursa döngüden çık)
                    break;
                }
            }
        }

        // SSE Endpoint - Graphics Stream (SSE Uç Noktası - Grafik Akışı)

        [HttpGet]
        public async Task StreamGraphics()
        {
            Response.Headers.Add("Content-Type", "text/event-stream");
            Response.Headers.Add("Cache-Control", "no-cache");
            Response.Headers.Add("Connection", "keep-alive");

            while (!HttpContext.RequestAborted.IsCancellationRequested)
            {
                try
                {
                    var metrics = await dashboardService.GetDashboardGraphDataAsync();

                    
                    var json = JsonSerializer.Serialize(metrics, new JsonSerializerOptions 
                    { 
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
                    });

                    await Response.WriteAsync($"data: {json}\n\n");
                    await Response.Body.FlushAsync();

                    await Task.Delay(5000);
                }
                catch (Exception)
                {
                    break;
                }
            }
        }

        // SSE Endpoint - Map Data Stream (SSE Uç Noktası - Harita Veri Akışı)

        [HttpGet]
        public async Task StreamMapData()
        {
            Response.Headers.Add("Content-Type", "text/event-stream");
            Response.Headers.Add("Cache-Control", "no-cache");
            Response.Headers.Add("Connection", "keep-alive");

            while (!HttpContext.RequestAborted.IsCancellationRequested)
            {
                try
                {
                    var mapData = await dashboardService.GetDashboardMapDataAsync();

                    
                    var json = JsonSerializer.Serialize(mapData, new JsonSerializerOptions 
                    { 
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
                    });

                    await Response.WriteAsync($"data: {json}\n\n");
                    await Response.Body.FlushAsync();

                    await Task.Delay(5000);
                }
                catch (Exception)
                {
                    break;
                }
            }
        }

        // SSE Endpoint - Top Lists Stream (SSE Uç Noktası - En İyiler Listesi Akışı)
        [HttpGet]
        public async Task StreamTopLists()
        {
            Response.Headers.Add("Content-Type", "text/event-stream");
            Response.Headers.Add("Cache-Control", "no-cache");
            Response.Headers.Add("Connection", "keep-alive");

            while (!HttpContext.RequestAborted.IsCancellationRequested)
            {
                try
                {
                    var topLists = await dashboardService.GetDashboardListsAsync();

                  
                    var json = JsonSerializer.Serialize(topLists, new JsonSerializerOptions 
                    { 
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
                    });

                    await Response.WriteAsync($"data: {json}\n\n");
                    await Response.Body.FlushAsync();
                    await Task.Delay(5000);
                }
                catch (Exception)
                {
                    break;
                }
            }
        }
    }
}
