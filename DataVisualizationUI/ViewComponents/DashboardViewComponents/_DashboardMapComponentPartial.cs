using DataVisualizationUI.Context;
using DataVisualizationUI.Models.DashboardViewModels;
using DataVisualizationUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataVisualizationUI.ViewComponents.DashboardViewComponents
{
    public class _DashboardMapComponentPartial : ViewComponent
    {
        private readonly DashboardService _dashboardService;

        public _DashboardMapComponentPartial(DashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _dashboardService.GetDashboardMapDataAsync();
            return View(result);
        }


    }
}
