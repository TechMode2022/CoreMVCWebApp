using CoreMVCWebApp.Filters;
using CoreMVCWebApp.Models;
using CoreMVCWebApp.Service;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CoreMVCWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        IHelloWorldService _helloWorldService;
        private IWebHostEnvironment Environment;

        public HomeController(ILogger<HomeController> logger,IWebHostEnvironment webHostEnvironment, IHelloWorldService helloWorldService)
        {
            _logger = logger;
            _helloWorldService = helloWorldService;
            Environment= webHostEnvironment;// constructor Injection
        }
        [Log]
       
        public IActionResult Index()
        {
            string hello = _helloWorldService.SaysHello();
            ViewBag.Hello = hello;
            _logger.LogError("Log Message in the Index() method");
            _logger.LogInformation("Log Message in the Index() method");
            _logger.LogCritical("Logging Critical Information");
            _logger.LogDebug("Logging Debug Information");
            _logger.LogError("Logging Error Information");
            _logger.LogTrace("Logging trace ");
            _logger.LogWarning("Logging warning ");


            return View();
            //return Redirect("home/about");
            //return Redirect("http://www.msn.com");
        }

        public ActionResult About()
        {

            return View();
        }

        public IActionResult Privacy()
        {
            string msg = null;
            ViewBag.Message = msg.Length;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult CustomError()
        {
            return View();
        }

        public ActionResult Details(int? id)
        {
            if(!id.HasValue)
                return RedirectToAction("Index");
            else
                return View();
        }

        public RedirectToActionResult MyHome()
        {
            return RedirectToAction("About","home");
        }

        public JsonResult person()
        {
            var persons = new List<Person>
            { 
                new Person{ Id = 1,FirstName="Harry",LastName="Potter"},
                new Person{ Id = 2,FirstName="James",LastName="Raj"}
            };
            var jresult = Json(persons);
            return new JsonResult(jresult);
        }

        // For downloading the file
        public FileResult MyFile()
        {
            byte[] filesBytes = System.IO.File.ReadAllBytes("logexceptions.txt");
            return File(filesBytes, "text/plain", "MyFile.txt");
        }

        public ContentResult MyContent()
        {
            return Content("Hello from MyContent", "text/html");
        }

        public RedirectToRouteResult Rediredt2Route()
        {
            return RedirectToRoute(new { controller = "Home", action=""});
        }
        public StatusCodeResult NotFoundExample()
        {
            return new StatusCodeResult(404);
        }

        public UnauthorizedResult AgainNotAuthorised()
        {
            return new UnauthorizedResult();
        }
    }
}