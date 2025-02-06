﻿using OnlinePerfumes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePerfumes.Core
{
    public interface IService<T> where T : class 
    {
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(int id);
        Task<T>GetById(int id);
        IQueryable<T> GetAll();
        Task<List<T>> Find(Expression<Func<T, bool>> filter);
    }
}
