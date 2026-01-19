using DataVisualizationUI.Context;
using DataVisualizationUI.Models.ForecastViewModels.OrderForecastViewModels;
using DataVisualizationUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using Microsoft.ML.Transforms.TimeSeries;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace DataVisualizationUI.ViewComponents.ForecastViewComponents
{
    public class _ForecastOrderCountryComponentPartial : ViewComponent
    {
        private readonly ForecastService _forecastService;

        public _ForecastOrderCountryComponentPartial(ForecastService forecastService)
        {
            _forecastService = forecastService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
           var results = await _forecastService.GetOrderCountryForecastAsync();

            return View(results);
        }
    }
}
