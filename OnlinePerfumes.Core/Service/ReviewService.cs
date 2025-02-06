using OnlinePerfumes.Core.IServices;
using OnlinePerfumes.DataAccess.Repository;
using OnlinePerfumes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePerfumes.Core.Service
{
    public class ReviewService : IReviewService
    {
        private readonly IRepository<Review> _repo;
        public ReviewService(IRepository<Review> repo)
        {
            _repo = repo;
        }

        public async  Task Add(Review review)
        {
             await _repo.Add(review);
        }

        public async Task Delete(int id)
        {
            await _repo.Delete(id);
        }

        public async  Task<List<Review>> Find(Expression<Func<Review, bool>> filter)
        {
            return await _repo.Find(filter);
        }

        public IQueryable<Review> GetAll()
        {
            return _repo.GetAll();
        }

        public async Task<Review> GetById(int id)
        {
           return await  _repo.GetById(id);
        }

        public async Task Update(Review review)
        {
            await _repo.Update(review);
        }

        
    }
}
