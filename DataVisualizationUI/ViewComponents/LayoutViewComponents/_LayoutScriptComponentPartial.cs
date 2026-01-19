using Microsoft.AspNetCore.Mvc;

namespace DataVisualizationUI.ViewComponents.LayoutViewComponents
{
    public class _LayoutScriptComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
