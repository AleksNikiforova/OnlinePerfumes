using OnlinePerfumes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePerfumes.Core.IServices
{
    public interface IPaymentService:IService<Payment>
    {
        Task Add(Payment payment);
        Task Delete(int id);
        Task Update(Payment payment);
        Task<IEnumerable<Payment>> GetAll();
        Task<Payment> GetById(int id);
    }
}
