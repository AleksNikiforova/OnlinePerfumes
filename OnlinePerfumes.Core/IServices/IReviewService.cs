using OnlinePerfumes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePerfumes.Core.IServices
{
    public interface IReviewService:IService<Review>
    {
        void Add(Review review);
        void Delete(int id);
        void Update(Review review);
        IEnumerable<Review> GetAll();
        Review Get(int id);
    }
}
