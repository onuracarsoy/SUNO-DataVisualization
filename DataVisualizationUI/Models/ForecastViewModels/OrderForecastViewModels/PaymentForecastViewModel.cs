using System.Collections.Generic;

namespace DataVisualizationUI.Models.ForecastViewModels.OrderForecastViewModels
{
 
    public class PaymentForecastData
    {
        public float MonthIndex { get; set; }
        public float OrderCount { get; set; }
    }

    public class PaymentForecastPrediction
    {
        public float[] ForecastedValues { get; set; }
    }

    public record PaymentMethodForecastViewModel
    {
        required public string PaymentMethod { get; init; }
        required public List<MonthlyData> HistoricalData { get; init; } = new();
        required public List<MonthlyData> ForecastData { get; init; } = new();
        required public double GrowthPercentage { get; init; }
    }

    // Monthly data record
    public record MonthlyData
    {
        required public string Period { get; init; } 
        required public float Value { get; init; }
    }
}
