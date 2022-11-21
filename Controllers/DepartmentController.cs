using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WebApp.Context;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class DepartmentController : Controller
    {
        MyContext myContext;

        public DepartmentController(MyContext myContext)
        {
            this.myContext = myContext;
        }

        //GET ALL
        public IActionResult Index()
        {
            var data = myContext.Departments.ToList();
            return View(data);
        }

        //GET BY ID
        public IActionResult Details(int id)
        {
            var data = myContext.Departments.Find(id);
            return View(data);
        }

        //INSERT - GET POST
        public IActionResult Create()
        {
            // get data di sini
            // ex: dropdown data didapat dari database
            var Divisions = myContext.Divisions.Select(a => new SelectListItem()
            {
                Value = a.Id.ToString(),
                Text = a.Name
            }).ToList();
            ViewBag.Divisions = Divisions;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Department department)
        {
            myContext.Departments.Add(department);
            var result = myContext.SaveChanges();
            if (result > 0)
            {
                return RedirectToAction("Index", "Department");
            }
            return View();
        }

        //UPDATE - GET POST
        public IActionResult Edit(int id)
        {
            var data = myContext.Departments.Find(id);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Department department)
        {
            var data = myContext.Departments.Find(id);
            if (data != null)
            {
                data.Name = department.Name;
                data.DivisionID = department.DivisionID;
                myContext.Entry(data).State = EntityState.Modified;
                var result = myContext.SaveChanges();
                if (result > 0)
                {
                    return RedirectToAction("Index", "Department");
                }
            }
            return View();
        }

        //DELETE - GET POST
        public IActionResult Delete(int id)
        {
            var data = myContext.Divisions.Find(id);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Division division)
        {
            myContext.Divisions.Remove(division);
            var result = myContext.SaveChanges();
            if (result > 0)
            {
                return RedirectToAction("Index", "Division");
            }
            return View();
        }
    }
}
