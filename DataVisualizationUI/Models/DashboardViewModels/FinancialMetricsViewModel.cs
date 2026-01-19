namespace DataVisualizationUI.Models.DashboardViewModels
{
    public record FinancialMetricsViewModel
    {
        required public decimal TotalRevenue { get; init; }
        required public decimal TotalProfit { get; init; } // Simulated for now
        required public decimal MonthlyRevenue { get; init; }
        required public int TotalOrders { get; init; }
        required public int TotalCustomers { get; init; }
        required public decimal AverageBasketAmount { get; init; }
        public DateTime Timestamp { get; init; } = DateTime.Now;
    }
}
