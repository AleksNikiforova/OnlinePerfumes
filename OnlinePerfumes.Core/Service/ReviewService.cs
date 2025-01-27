using OnlinePerfumes.Core.IServices;
using OnlinePerfumes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePerfumes.Core.Service
{
    public class ReviewService:IReviewService
    {
    
        public void Add(Review review)
        {
            throw new NotImplementedException();
        }

        public void Update(Review review)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Review> IReviewService.GetAll()
        {
            throw new NotImplementedException();
        }

        Review IReviewService.Get(int id)
        {
            throw new NotImplementedException();
        }

        public Review GetById(int id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Review> IService<Review>.GetAll()
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
