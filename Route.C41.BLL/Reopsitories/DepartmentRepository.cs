using Microsoft.EntityFrameworkCore;
using Route.C41.BLL.Interfaces;
using Route.C41.DAL.Data;
using Route.C41.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.BLL.Reopsitories
{
	internal class DepartmentRepository : IDepartmentRepositery
	{
		private readonly ApplicationDbContext _dbContext;

		public DepartmentRepository(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public int Add(Department entity)
		{
			//throw new NotImplementedException();
			 _dbContext.Add(entity);
			return _dbContext.SaveChanges();
		}
		public int Update(Department entity)
		{
			//throw new NotImplementedException();
			_dbContext.Update(entity);
			return _dbContext.SaveChanges();
		}
		public int Delete(Department entity)
		{
			//throw new NotImplementedException();
			_dbContext.Remove(entity);
			return _dbContext.SaveChanges();
		}

		public Department Get(int id)
		{
			//throw new NotImplementedException();
			//var dept= _dbContext.Departments.Where(d=>d.ID==id).FirstOrDefault();
			//return _dbContext.Departments.Find(id);
			return _dbContext.Find<Department>(id);
			//return dept;
		}

		public IEnumerable<Department> GetAll()
		{
			return _dbContext.Departments.AsNoTracking().ToList();
		}

		
	}
}
