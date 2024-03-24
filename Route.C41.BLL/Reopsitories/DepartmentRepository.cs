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
	public class DepartmentRepository : GenericRepository<Department>,IDepartmentRepository
	{
        public DepartmentRepository(ApplicationDbContext dbContext):base(dbContext)
        {
            
        }

    }
}
