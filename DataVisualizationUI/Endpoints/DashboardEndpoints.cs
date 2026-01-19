using Azure;
using DataVisualizationUI.Context;
using DataVisualizationUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using System.Runtime.CompilerServices;

namespace DataVisualizationUI.Endpoints
{
    public static class DashboardEndpointsForMinimalApi
    {

        #region AI Advice SSE Endpoints (with minimal api)

        // Add dashboard endpoints to the WebApplication (WebApplication'a dashboard uç noktalarını ekleyin)
        public static void MapDashboardEndpoints(this WebApplication app)
        {
            // 🎯 SSE Minimal API - AI Dashboard Advice
            app.MapGet("/sse/ai-advice", async (
                HttpContext context,
                DataVisualizationDbContext db,
                Kernel kernel) =>
            {
                context.Response.Headers.Add("Content-Type", "text/event-stream");
                context.Response.Headers.Add("Cache-Control", "no-cache");
                context.Response.Headers.Add("Connection", "keep-alive");

                await foreach (var advice in StreamAIAdvice(db, kernel, context.RequestAborted))
                {
                    var json = System.Text.Json.JsonSerializer.Serialize(new
                    {
                        advice,
                        timestamp = DateTime.Now
                    });

                    await context.Response.WriteAsync($"data: {json}\n\n");
                    await context.Response.Body.FlushAsync();
                }
            });
        }


        //AI Advice Stream Generator (with yield return use ) (AI Tavsiye Akış Oluşturucu (yield return kullanımı ile))
        private static async IAsyncEnumerable<string> StreamAIAdvice(
            DataVisualizationDbContext db,
            Kernel kernel,
            [EnumeratorCancellation] CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                string advice;
                try
                {
                  
                    var totalCustomers = await db.Customers.CountAsync(ct);
                    var totalProducts = await db.Products.CountAsync(ct);
                    var totalOrders = await db.Orders.CountAsync(ct);
                    var totalRevenue = await db.Orders
                        .Include(o => o.Products)
                        .SumAsync(x => x.Products.UnitPrice * x.Quantity, ct);

                    // Take advice from AI (AI'dan tavsiye al)
                    var chatService = kernel.GetRequiredService<IChatCompletionService>();
                    var chatHistory = new ChatHistory();
                    chatHistory.AddSystemMessage("Sen bir iş zekası asistanısın. Kısa ve öz Türkçe tavsiyeler veriyorsun (max 2 cümle).");
                    chatHistory.AddUserMessage($@"
Dashboard verileri:
- Müşteri: {totalCustomers}
- Ürün: {totalProducts}
- Sipariş: {totalOrders}
- Gelir: {totalRevenue:C}

Kısa bir tavsiye ver.");

                    var result = await chatService.GetChatMessageContentAsync(chatHistory, kernel: kernel, cancellationToken: ct);
                    advice = result?.Content ?? "Dashboard sağlıklı görünüyor!";
                }
                catch (Exception)
                {
                    advice = "AI tavsiyeleri geçici olarak kullanılamıyor.";
                }

                yield return advice;

                // Wait for 10 minutes before sending the next advice (Bir sonraki tavsiyeyi göndermeden önce 10 dakika bekleyin)
                try
                {
                    await Task.Delay(600000, ct);
                }
                catch (TaskCanceledException)
                {
                    // User closed the page or connection was lost (Kullanıcı sayfayı kapattı veya bağlantı kesildi)
                    // 'yield break' terminates the async iterator (IAsyncEnumerable) ('yield break', asenkron yineleyiciyi (IAsyncEnumerable) sonlandırır)
                    // Normal 'break' only exits the while loop, but the method continues running (Normal 'break' sadece while döngüsünden çıkar, ancak metot çalışmaya devam eder)
                    // 'yield break' closes the entire stream and completely terminates the method ('yield break' tüm akışı kapatır ve metodu tamamen sonlandırır)
                    yield break;
                }
            }
        }

        #endregion
    }

  
}
