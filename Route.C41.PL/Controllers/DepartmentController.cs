using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.C41.BLL.Interfaces;
using Route.C41.DAL.Models;
using System;
using System.Linq;

namespace Route.C41.PL.Controllers
{
	public class DepartmentController : Controller
	{
		private readonly IDepartmentRepository _departmentRepository;
        private readonly IWebHostEnvironment _env;

        public DepartmentController(IDepartmentRepository departmentRepository,IWebHostEnvironment env)
		{
			_departmentRepository = departmentRepository;
            _env = env;
        }
		public IActionResult Index()
		{

			return View(_departmentRepository.GetAllAsync());
		}
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(Department department)
		{
			if(ModelState.IsValid)//True if all validiation done Server side 
			{
				var count =_departmentRepository.Add(department);
				if(count > 0)
				{
					return RedirectToAction(nameof(Index));
				}
			}
			return View(department);


		}
		[HttpGet]
		public IActionResult Details(int? id,string viewName="Details")
		{
			if (id is null)
				return BadRequest();

			var department = _departmentRepository.GetAsync(id.Value);
			if (department == null)
				return NotFound();

			return View(viewName,department);
		}

		[HttpGet]
		public IActionResult Edit(int? id)
		{
			return Details(id,"Edit");
			// return View(id);
		}
		
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit([FromRoute]int id,Department department)//Id From Segment
		{
			if(id!=department.Id)
				return BadRequest();
			
			if(!ModelState.IsValid)
			{
				return View(department);
			}

			try
			{
				_departmentRepository.Update(department);
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{

				//1 Log Exception
				//2 Friendly message
				if(_env.IsDevelopment())
					ModelState.AddModelError(string.Empty,ex.Message);
				else
                    ModelState.AddModelError(string.Empty, "Error Occured During Yacta :(");

				return View(department);
            }
		}
		
		public IActionResult Delete(int? id)
		{
			return Details(id, "Delete");
		}

		[HttpPost]
		public IActionResult Delete(Department department)
		{
			_departmentRepository.Delete(department);
			return RedirectToAction(nameof(Index));
		}
	}
}
