﻿using Route.C41.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.BLL.Interfaces
{
    public interface IGenericRepository<T> where T : ModelBase
    {
        IEnumerable<T> GetAllAsync();
        Task<T> GetAsync(int id);
        int Add(T entity);
        int Update(T entity);
        int Delete(T entity);
    }
}
