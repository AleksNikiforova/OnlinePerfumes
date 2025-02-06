using OnlinePerfumes.Core.IServices;
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
        public async  Task Add(Review review)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Review>> Find(Expression<Func<Review, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Review> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Review> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task Update(Review review)
        {
            throw new NotImplementedException();
        }

        
    }
}
