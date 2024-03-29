using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.C41.BLL.Interfaces;
using Route.C41.DAL.Models;
using System;

namespace Route.C41.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _EmployeeRepository;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IEmployeeRepository EmployeeRepository, IWebHostEnvironment env)
        {
            _EmployeeRepository = EmployeeRepository;
            _env = env;
            

        }
        public IActionResult Index()
        {
            ViewData["Message"]= "Hi ViewData";
            return View(_EmployeeRepository.GetAll());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee Employee)
        {
            if (ModelState.IsValid)//True if all validiation done Server side 
            {
                var count = _EmployeeRepository.Add(Employee);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(Employee);


        }
        [HttpGet]
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (id is null)
                return BadRequest();

            var Employee = _EmployeeRepository.Get(id.Value);
            if (Employee == null)
                return NotFound();

            return View(viewName, Employee);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");
            // return View(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Employee Employee)//Id From Segment
        {
            if (id != Employee.ID)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                return View(Employee);
            }
            try
            {
                _EmployeeRepository.Update(Employee);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                //1 Log Exception
                //2 Friendly message
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "Error Occured During Yacta :(");

                return View(Employee);
            }
        }

        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        public IActionResult Delete(Employee Employee)
        {
            _EmployeeRepository.Delete(Employee);
            return RedirectToAction(nameof(Index));
        }
    }
}
