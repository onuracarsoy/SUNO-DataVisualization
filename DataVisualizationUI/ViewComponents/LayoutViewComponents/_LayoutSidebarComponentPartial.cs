using Microsoft.AspNetCore.Mvc;

namespace DataVisualizationUI.ViewComponents.LayoutViewComponents
{
    public class _LayoutSidebarComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
