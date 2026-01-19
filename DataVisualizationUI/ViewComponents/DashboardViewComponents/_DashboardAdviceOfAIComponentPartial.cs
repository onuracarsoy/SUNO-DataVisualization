using DataVisualizationUI.Context;
using DataVisualizationUI.Models.DashboardViewModels;
using DataVisualizationUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.Text;

namespace DataVisualizationUI.ViewComponents.DashboardViewComponents
{

    public class _DashboardAdviceOfAIComponentPartial : ViewComponent
    {
  
        private readonly Kernel _kernel;
        private readonly DashboardService _dashboardService;

        public _DashboardAdviceOfAIComponentPartial(Kernel kernel, DashboardService dashboardService)
        {
            _kernel = kernel;
            _dashboardService = dashboardService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
               
                var aiAdvice = await _dashboardService.GetAIAdviceAsync();

                ViewBag.AIAdvice = aiAdvice;
                ViewBag.HasError = false;
            }
            catch (Exception ex)
            {
              
                ViewBag.AIAdvice = "AI tavsiyeleri şu anda kullanılamıyor.";
                ViewBag.HasError = true;
                ViewBag.ErrorMessage = ex.Message;
            }

            return View();
        }

    }
}
