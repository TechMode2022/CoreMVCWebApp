using CoreMVCWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreMVCWebApp.Controllers
{
    public class StudentController : Controller
    {
        List<Student> stdList = new List<Student>();
        public Student student;
        public string strurl = "http://localhost:8010/api/";
        public IActionResult GetStudents()
        {
            int flag = 0;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(strurl);

                    // http get


                    var responseTask = client.GetAsync("student/GetAllStudents");
                    responseTask.Wait();

                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<Student[]>();
                        readTask.Wait();
                        stdList = readTask.Result.ToList();
                        flag = 1;

                    }
                }
            } catch (Exception ex) { }
                return View(stdList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }

        [HttpPost]
        public IActionResult Create(Student std)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(strurl);
                // http get
                var postTask = client.PostAsJsonAsync<Student>("student", std);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<int>();
                    readTask.Wait();

                    var insertedGrade = readTask.Result;
                    if (insertedGrade > 0)
                    {
                        return RedirectToAction("GetStudents");
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

                var responseTask = client.GetAsync($"student/GetStudentById?Id={id}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Student>();
                    readTask.Wait();

                    var std = readTask.Result;
                    if (std != null)
                    {
                        return View(std);
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

        [HttpGet]
        public ActionResult Delete(int id)
        {
            // call the web api GetGragdeById method 
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(strurl);

                // http get

                var responseTask = client.GetAsync($"student/GetStudentById?Id={id}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Student>();
                    readTask.Wait();

                    var std = readTask.Result;
                    if (std != null)
                    {
                        return View(std);
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
    }
}
