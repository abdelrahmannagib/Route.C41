using Microsoft.AspNetCore.Mvc;
using Route.C41.BLL.Interfaces;

namespace Route.C41.PL.Controllers
{
	public class DepartmentController : Controller
	{
		private readonly IDepartmentRepositery _departmentRepository;

		public DepartmentController(IDepartmentRepositery departmentRepository)
		{
			_departmentRepository = departmentRepository;
		}
		public IActionResult Index()
		{

			return View();
		}
	}
}
