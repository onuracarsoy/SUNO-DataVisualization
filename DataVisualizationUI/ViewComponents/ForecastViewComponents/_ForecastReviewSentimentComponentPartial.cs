using DataVisualizationUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataVisualizationUI.ViewComponents.ForecastViewComponents
{
    public class _ForecastReviewSentimentComponentPartial : ViewComponent
    {
        private readonly ForecastService _forecastService;

        public _ForecastReviewSentimentComponentPartial(ForecastService forecastService)
        {
            _forecastService = forecastService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _forecastService.GetReviewSentimentForecastAsync();
            return View(values);
        }
    }
}
