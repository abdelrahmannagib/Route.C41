using Route.C41.BLL.Interfaces;
using Route.C41.BLL.Reopsitories;
using Route.C41.DAL.Data;
using Route.C41.DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.BLL
{
	public class UnitOfWork : IUnitOFWork
	{
		private readonly ApplicationDbContext _dbContext;

		public IEmployeeRepository EmployeeRepository { get; set ; } = null;
		public IDepartmentRepository DepartmentRepository { get ; set ; } = null;
		private Hashtable _repostories;

		public UnitOfWork(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
			//EmployeeRepository =  new EmployeeRepository(_dbContext);
			//DepartmentRepository = new DepartmentRepository(_dbContext);
			_repostories = new Hashtable();
		}



		public IGenericRepository<T> Repository<T>() where T : ModelBase
		{
			var key = typeof(T).Name;
			if (!_repostories.ContainsKey(key))
			{

				if (key == nameof(Employee))
				{

					var repository = new EmployeeRepository(_dbContext);
					_repostories.Add(key, repository);
				}
				else
				{
					var repository = new GenericRepository<T>(_dbContext);
					_repostories.Add(key, repository);


				}


			}
			return _repostories[key] as IGenericRepository<T>;
		}


		public int Complete()
		{

			return _dbContext.SaveChanges();

		}

		public void Dispose()
		{
			_dbContext.Dispose();
		}
	}
}
