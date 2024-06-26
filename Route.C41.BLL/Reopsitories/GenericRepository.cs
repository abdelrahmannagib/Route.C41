﻿using Microsoft.EntityFrameworkCore;
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
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
    {
        private protected readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Add(T entity)
        {
            //throw new NotImplementedException();
            _dbContext.Add(entity);
            return _dbContext.SaveChanges();
        }
        public int Update(T entity)
        {
            //throw new NotImplementedException();
            _dbContext.Update(entity);
            return _dbContext.SaveChanges();
        }
        public int Delete(T entity)
        {
            //throw new NotImplementedException();
            _dbContext.Remove(entity);
            return _dbContext.SaveChanges();
        }

        public async Task<T> GetAsync(int id)
        {
            //throw new NotImplementedException();
            //var dept= _dbContext.Departments.Where(d=>d.ID==id).FirstOrDefault();
            //return _dbContext.Departments.Find(id);
            return await _dbContext.FindAsync<T>(id);
            //return dept;
        }

        public IEnumerable<T> GetAllAsync()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>)_dbContext.Employees.Include(E => E.Department).AsNoTracking().ToList();
            }
            else
            {
                return _dbContext.Set<T>().AsNoTracking().ToList();
            }
        }


    }
}
