using System.Collections.Generic;

namespace DataVisualizationUI.Models.DashboardViewModels
{
    public record DashboardGraphicsViewModel
    {
        // Line Chart: Sales Trend (Generic - 12 Months)
        required public List<string> ChartLabels { get; init; }
        required public List<int> ChartValues { get; init; }

        // Bar Chart: 5 Year Summary
        required public List<string> FiveYearLabels { get; init; }
        required public List<int> FiveYearValues { get; init; }

        // Pie Chart: Category Performance (Top 5)
        required public List<string> TopCategoryNames { get; init; }
        required public List<int> TopCategoryProductCounts { get; init; } // Or Revenue if preferred

        // Doughnut Chart: Payment Method Distribution
        required public List<string> PaymentMethods { get; init; }
        required public List<int> PaymentMethodCounts { get; init; }

        public DateTime Timestamp { get; init; } = DateTime.Now;
    }
}
