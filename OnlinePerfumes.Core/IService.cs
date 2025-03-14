using OnlinePerfumes.Models;
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
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        IQueryable<T> GetAll();
        Task<List<T>> Find(Expression<Func<T, bool>> filter);


    }
}
