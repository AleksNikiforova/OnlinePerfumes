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
    public class PaymentService : IPaymentService
    {
        private readonly IRepository<Payment> _repo;
        public PaymentService(IRepository<Payment> repo)
        {
            _repo = repo;
        }

        public async Task Add(Payment payment)
        {
            await _repo.Add(payment);
        }

        public async Task Delete(int id)
        {
           await _repo.Delete(id);
        }

        public async Task<List<Payment>> Find(Expression<Func<Payment, bool>> filter)
        {
            return await _repo.Find(filter);
        }

        public IQueryable<Payment> GetAll()
        {
           return _repo.GetAll();
        }

        public async Task<Payment> GetById(int id)
        {
            return await _repo.GetById(id);
        }

        public async Task Update(Payment payment)
        {
            await _repo.Update(payment);
        }
    }
}
