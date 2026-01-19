using DataVisualizationUI.Context;
using DataVisualizationUI.Models;
using DataVisualizationUI.Models.DashboardViewModels;
using DataVisualizationUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataVisualizationUI.ViewComponents.DashboardViewComponents
{
    public class _DashboardGraphicsComponentPartial : ViewComponent
    {
        private readonly DashboardService _dashboardService;

        public _DashboardGraphicsComponentPartial(DashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _dashboardService.GetDashboardGraphDataAsync();
            return View(result);

        }
    }
}
