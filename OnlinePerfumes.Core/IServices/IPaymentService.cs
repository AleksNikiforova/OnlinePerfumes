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
        void Add(Payment payment);
        void Delete(int id);
        void Update(Payment payment);
        IEnumerable<Payment> GetAll();
        Payment Get(int id);
    }
}
