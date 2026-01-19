using System.Collections.Generic;

namespace DataVisualizationUI.Models.DashboardViewModels
{
    public record DashboardListsViewModel
    {
        required public List<TopSellingProductItem> TopSellingProducts { get; init; }
        required public List<LowStockProductItem> LowStockProducts { get; init; }
        public DateTime Timestamp { get; init; } = DateTime.Now;
    }

    public record TopSellingProductItem
    {
        required public string ProductName { get; init; }
        required public string CategoryName { get; init; }
        required public string ImageUrl { get; init; }
        required public decimal UnitPrice { get; init; }
        required public int TotalSold { get; init; }
        required public decimal TotalRevenue { get; init; }
    }

    public record LowStockProductItem
    {
        required public string ProductName { get; init; }
        required public string ImageUrl { get; init; }
        required public int StockQuantity { get; init; }
        required public int CategoryId { get; init; }
    }
}
