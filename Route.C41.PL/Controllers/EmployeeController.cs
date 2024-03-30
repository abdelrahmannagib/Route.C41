using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.C41.BLL.Interfaces;
using Route.C41.DAL.Models;
using Route.C41.PL.ViewModels;
using System;
using System.Linq;

namespace Route.C41.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _EmployeeRepository;
        private readonly IWebHostEnvironment _env;
		private readonly IMapper _mapper;

		public EmployeeController(IEmployeeRepository EmployeeRepository, IWebHostEnvironment env,IMapper mapper)
        {
            _EmployeeRepository = EmployeeRepository;
            _env = env;
			_mapper = mapper;
		}
        public IActionResult Index(string searchInp)
        {
            // TempData.Keep();
            // ViewData["Message"]= "Hi ViewData";
            var employees = Enumerable.Empty<Employee>();
            if (string.IsNullOrEmpty(searchInp))
            {
                employees= _EmployeeRepository.GetAll();
            }
            else
            {
                employees= _EmployeeRepository.SearchByName(searchInp.ToLower());

            }
            return View(employees);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeViewModel Employee)
        {
            ///if (ModelState.IsValid)//True if all validiation done Server side 
            ///{
            ///    var count = _EmployeeRepository.Add(Employee);
            ///    if (count > 0)
            ///    {
            ///        TempData["Message"] = "Created";
            ///        return RedirectToAction(nameof(Index));
            ///    }
            ///    else
            ///    {
            ///        TempData["Message"] = "Error";
            ///        return RedirectToAction(nameof(Index));
            ///    }
            ///}
            ///return View(Employee);

            var mappedEmp=_mapper.Map<EmployeeViewModel,Employee>(Employee);
			var count = _EmployeeRepository.Add(mappedEmp);
			if (count > 0)
			{
				TempData["Message"] = "Created";
				return RedirectToAction(nameof(Index));
			}
			else
			{
				TempData["Message"] = "Error";
				return RedirectToAction(nameof(Index));
			}

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
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel employeeVM)//Id From Segment
        {
			var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

			if (id != employeeVM.Id)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                return View(employeeVM);
            }
            try
            {
				_EmployeeRepository.Update(mappedEmp);
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

                return View(employeeVM);
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
