using DataVisualizationUI.Context;
using DataVisualizationUI.Models;
using DataVisualizationUI.Models.DashboardViewModels;
using DataVisualizationUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DataVisualizationUI.ViewComponents.DashboardViewComponents
{
    public class _DashboardFinancialMetricsComponentPartial : ViewComponent
    {
        private readonly DashboardService _dashboardService;

        public _DashboardFinancialMetricsComponentPartial(DashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _dashboardService.GetFinancialMetricsAsync();
            return View(result);
        }
    }
}
