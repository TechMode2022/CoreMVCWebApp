using CoreMVCWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Diagnostics;

namespace CoreMVCWebApp.Controllers
{
    public class GradeController : Controller
    {

        List<Grade> grdlist = new List<Grade>();
        public Grade grades;
        public string strurl= "http://localhost:8010/api/";

        public IActionResult Index()
        {

            
            return View();
        }

        [HttpGet]
        
       public IActionResult Getgrades() 
        {
            int flag = 0;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(strurl);

                    // http get

                    var responseTask = client.GetAsync("Grade/GetAllGrades");
                    responseTask.Wait();

                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<Grade[]>();
                        readTask.Wait();
                        grdlist = readTask.Result.ToList();
                        flag = 1;

                    }
                }

                

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            if (flag == 1)
            {
                return View(grdlist);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
       }

        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }

        [HttpPost]
        public IActionResult Create(Grade grd)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(strurl);
                // http get
                var postTask = client.PostAsJsonAsync<Grade>("Grade", grd);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<int>();
                    readTask.Wait();

                    var insertedGrade = readTask.Result;
                    if (insertedGrade > 0)
                    {
                        return RedirectToAction("GetGrades");
                        //Console.WriteLine("{0} records of grade inserted ", insertedGrade);
                    }

                }
                else
                {
                    return View();
                }
            }

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            // call the web api GetGragdeById method 
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(strurl);

                // http get

                var responseTask = client.GetAsync($"Grade/GradeId?id={id}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Grade>();
                    readTask.Wait();

                    var grd = readTask.Result;
                    if (grd != null)
                    {
                        return View(grd);
                    }

                }
                else
                {
                    Console.WriteLine("Can't retrieve record...!");
                }

            }
            return View();

            //if (grades != null)
            //{
            //    return View(grades);
            //}
            //else
            //    return RedirectToAction("GetGrades");
        }

        [HttpPost]
        public ActionResult Edit(Grade grade)
        {
            int recupdateded = 0;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(strurl);
                var postback = client.PutAsJsonAsync<Grade>("Grade/UpdateGrade?grdid=" + grade.GradeId, grade);

                postback.Wait();

                var result = postback.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readtask = result.Content.ReadAsAsync<int>();
                    readtask.Wait();

                    recupdateded= readtask.Result;
                    return RedirectToAction("GetGrades");
                }
            }

            return View(grade);
        }

        [HttpGet]
        public ActionResult Delete(int id) 
        {
            int recDeleted = 0;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(strurl);
                var responsetask = client.DeleteAsync($"grade/DeleteGrade?id={id}");

                responsetask.Wait();

                var result = responsetask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readtask = result.Content.ReadAsAsync<int>();
                    readtask.Wait();

                    recDeleted = readtask.Result;

                    if(recDeleted > 0)
                    {
                    return RedirectToAction("GetGrades");

                    }

                }
            }
            return View();

        }

    }
}
