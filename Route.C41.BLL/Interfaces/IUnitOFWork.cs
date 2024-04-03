using Route.C41.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.BLL.Interfaces
{
	public interface IUnitOFWork:IAsyncDisposable
	{

		IGenericRepository<T> Repository<T>() where T : ModelBase;

		Task<int> Complete();
	}
}
