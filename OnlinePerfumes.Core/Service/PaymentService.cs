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
    public class PaymentService : IPaymentService
    {
        public async Task Add(Payment payment)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Payment>> Find(Expression<Func<Payment, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Payment>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Payment> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task Update(Payment payment)
        {
            throw new NotImplementedException();
        }
    }
}
