using CoreMVCWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CoreMVCWebApp.Controllers
{
    public class PersonController : Controller
    {
        private List<Person> personList;

        public PersonController()
        {
            // Initialize the person list if the file exists, otherwise create an empty list.
            if (System.IO.File.Exists("D:\\Sidhant\\Training\\CoreMVCWebApp\\person.txt"))
            {
                personList = GetPersonsList();
            }
            else
            {
                personList = new List<Person>();
            }
        }

        public IActionResult DisplayPerson()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetPersons()
        {
            return View(personList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Person per)
        {
            if (per != null)
            {
                // Generate a unique Id for the new person.
                per.Id = GenerateUniqueId();
                personList.Add(per);
                SavePersonsList(personList);
            }

            return RedirectToAction("GetPersons");
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            // Find the person to edit by Id.
            Person p1 = personList.FirstOrDefault(x => x.Id == Id);

            if (p1 != null)
            {
                return View(p1);
            }

            // Handle the case where the person with the specified Id was not found.
            return RedirectToAction("GetPersons");
        }

        [HttpPost]
        public ActionResult Edit(Person per)
        {
            if (per != null)
            {
                // Find the person to edit by Id.
                Person p2 = personList.FirstOrDefault(x => x.Id == per.Id);

                if (p2 != null)
                {
                    // Update the person's properties.
                    p2.FirstName = per.FirstName;
                    p2.LastName = per.LastName;
                    p2.Age = per.Age;

                    SavePersonsList(personList);
                }
            }

            return RedirectToAction("GetPersons");
        }

        [HttpGet]
        public ActionResult Details(int Id)
        {
            // Find the person by Id.
            Person person = personList.FirstOrDefault(x => x.Id == Id);
            return View(person);
        }

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            // Find the person to delete by Id.
            Person personToDelete = personList.FirstOrDefault(x => x.Id == Id);

            if (personToDelete != null)
            {
                // Remove the person from the list.
                personList.Remove(personToDelete);
                SavePersonsList(personList);
            }

            return RedirectToAction("GetPersons");
        }

        private List<Person> GetPersonsList()
        {
            List<Person> perList = new List<Person>();

            using (StreamReader sr = new StreamReader("D:\\Sidhant\\Training\\CoreMVCWebApp\\person.txt"))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] parts = line.Split(',');

                    if (parts.Length == 4)
                    {
                        Person p = new Person
                        {
                            Id = Convert.ToInt32(parts[0]),
                            FirstName = parts[1],
                            LastName = parts[2],
                            Age = Convert.ToInt32(parts[3])
                        };

                        perList.Add(p);
                    }
                }
            }

            return perList;
        }

        private void SavePersonsList(List<Person> persons)
        {
            using (StreamWriter sw = new StreamWriter("D:\\Sidhant\\Training\\CoreMVCWebApp\\person.txt"))
            {
                foreach (Person person in persons)
                {
                    sw.WriteLine($"{person.Id},{person.FirstName},{person.LastName},{person.Age}");
                }
            }
        }

        private int GenerateUniqueId()
        {
            // Generate a unique Id based on the maximum existing Id in the list.
            int maxId = personList.Count > 0 ? personList.Max(p => p.Id) : 0;
            return maxId + 1;
        }
    }
}
