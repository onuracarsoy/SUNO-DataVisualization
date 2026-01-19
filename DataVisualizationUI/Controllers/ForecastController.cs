using DataVisualizationUI.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;

namespace DataVisualizationUI.Controllers
{
    public class ForecastController : Controller
    {
        private readonly DataVisualizationDbContext _context;
        private readonly MLContext _mLContext;

        public ForecastController(DataVisualizationDbContext context, MLContext mLContext)
        {
            _context = context;
            _mLContext = mLContext;
        }

        public IActionResult OrderForecasts()
        {
            return View();
        }

        public IActionResult ReviewForecasts()
        {
            return View();
        }
    }
}
