namespace DataVisualizationUI.Models.DashboardViewModels
{
    public record DashboardStatsForAIAdviceViewModel
    {
        required public int TotalCustomers { get; init; }
        required public int TotalProducts { get; init; }
        required public int TotalOrders { get; init; }
        required public decimal TotalRevenue { get; init; }
        required public decimal AverageOrderValue { get; init; }
        required public int LowStockProductsCount { get; init; }
        required public int RecentOrdersCount { get; init; }
        required public string TopProductName { get; init; }
    }
}
