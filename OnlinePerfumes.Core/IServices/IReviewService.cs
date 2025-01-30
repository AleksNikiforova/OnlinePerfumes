using OnlinePerfumes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePerfumes.Core.IServices
{
    public interface IReviewService:IService<Review>
    {
        Task Add(Review review);
        Task Delete(int id);
        Task Update(Review review);
        Task<List<Review>> GetAll();
        Task<Review> GetById(int id);
        Task<List<Review>> Find(Expression<Func<Review, bool>> filter);
    }
}
