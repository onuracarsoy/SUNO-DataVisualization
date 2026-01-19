using System.Collections.Generic;

namespace DataVisualizationUI.Models.DashboardViewModels
{
    public record DashboardMapViewModel
    {
        required public List<MapLocationItem> TopCountries { get; init; }
        public DateTime Timestamp { get; init; } = DateTime.Now;
    }

    public record MapLocationItem
    {
        required public string CountryName { get; init; }
        required public int OrderCount { get; init; }
        required public double Latitude { get; init; }
        required public double Longitude { get; init; }
    }
}
