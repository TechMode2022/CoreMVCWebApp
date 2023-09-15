using CoreMVCWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CoreMVCWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
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
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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