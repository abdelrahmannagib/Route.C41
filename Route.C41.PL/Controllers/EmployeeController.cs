using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.C41.BLL;
using Route.C41.BLL.Interfaces;
using Route.C41.BLL.Reopsitories;
using Route.C41.DAL.Models;
using Route.C41.PL.Helpers;
using Route.C41.PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;

namespace Route.C41.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _EmployeeRepository;
        private readonly IWebHostEnvironment _env;
		private readonly IMapper _mapper;
		private readonly IUnitOFWork _unitOfWork;

		public EmployeeController( IWebHostEnvironment env,IMapper mapper

		  ,IUnitOFWork unitOfWork)
        {
          //  _EmployeeRepository = EmployeeRepository;
            _env = env;
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}
        public IActionResult Index(string searchInp)
        {

			#region comm
			//// TempData.Keep();
			//// ViewData["Message"]= "Hi ViewData";
			//var employees = Enumerable.Empty<Employee>();
			//if (string.IsNullOrEmpty(searchInp))
			//{
			//    employees = _EmployeeRepository.GetAll();
			//}
			//else
			//{
			//    employees = _EmployeeRepository.SearchByName(searchInp.ToLower());

			//}
			//return View(employees); 
			#endregion

			var empolyees = Enumerable.Empty<Employee>();
			var employeeRepo = _unitOfWork.Repository<Employee>() as EmployeeRepository;
			if (string.IsNullOrEmpty(searchInp))
				empolyees = employeeRepo.GetAll();


			else
				empolyees = employeeRepo.SearchByName(searchInp.ToLower());

			var MappedEmps = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(empolyees);

			return View(MappedEmps);
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

			if (ModelState.IsValid) // Server Side Validation
			{

				Employee.ImageName=DocumentSettings.UploadFile(Employee.Image, "images");

				var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(Employee);


				_unitOfWork.Repository<Employee>().Add(mappedEmp);
				var count = _unitOfWork.Complete();
				if (count > 0)
				{
					return RedirectToAction("Index");
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
