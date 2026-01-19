using System.Collections.Generic;

namespace DataVisualizationUI.Models.ForecastViewModels.OrderForecastViewModels
{
    // Input data for ML.NET
    public class CountryForecastData
    {
        public float MonthIndex { get; set; }
        public float OrderCount { get; set; }
    }

    // Forecast data from ML.NET
    public class CountryForecastPrediction
    {
        public float[] ForecastedValues { get; set; }
    }

    // ViewModel for presenting forecast results
    public record CountryForecastViewModel
    {
        required public string CountryName { get; init; }
        required public List<MonthlyData> HistoricalData { get; init; } = new();
        required public List<MonthlyData> ForecastData { get; init; } = new();
        required public double GrowthPercentage { get; init; }
        required public int TotalHistorical { get; init; } 
        required public int TotalForecast { get; init; } 
    }
}
