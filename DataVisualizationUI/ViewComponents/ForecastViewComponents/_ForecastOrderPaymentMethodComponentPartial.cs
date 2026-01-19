using DataVisualizationUI.Context;
using DataVisualizationUI.Models.ForecastViewModels.OrderForecastViewModels;
using DataVisualizationUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using Microsoft.ML.Transforms.TimeSeries;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DataVisualizationUI.ViewComponents.ForecastViewComponents
{
    public class _ForecastOrderPaymentMethodComponentPartial : ViewComponent
    {

        private readonly ForecastService _forecastService;

        public _ForecastOrderPaymentMethodComponentPartial(ForecastService forecastService)
        {
            _forecastService = forecastService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var results = await _forecastService.GetOrderPaymentMethodForecastAsync();

            return View(results);
        }
    }
}
