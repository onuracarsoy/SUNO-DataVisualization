using Microsoft.AspNetCore.Mvc;

namespace DataVisualizationUI.ViewComponents.LayoutViewComponents
{
    public class _LayoutHeaderComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
