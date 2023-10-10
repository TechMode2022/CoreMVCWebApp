using CoreMVCWebApp.Service;
using Microsoft.AspNetCore.Mvc;

namespace CoreMVCWebApp.Controllers
{
    public class CalculatorController : Controller
    {
        private readonly ILogger<CalculatorController> _logger;
        ICalculatorService _calculatorService;
        private IWebHostEnvironment Environment;

        public CalculatorController(ILogger<CalculatorController> logger, ICalculatorService calculatorService, IWebHostEnvironment webHostEnvironment)
        {
            _logger= logger;
            _calculatorService= calculatorService;
            Environment= webHostEnvironment;
        }

        public IActionResult Add()
        {
            ViewBag.Add = _calculatorService.Add(2, 3);
            return View();
        }

        public IActionResult Subtract()
        {
            ViewBag.Subtract = _calculatorService.Subtract(4, 2);
            return View();
        }

        public IActionResult Mul()
        {
            ViewBag.Mul = _calculatorService.Multiply(4, 2);
            return View();
        }

        public IActionResult Div()
        {

            ViewBag.Div = _calculatorService.Divide(4, 2);
            return View();
        }
    }
}
