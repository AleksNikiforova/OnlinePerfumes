using OnlinePerfumes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinePerfumes.Core.IServices
{
    public interface IApplicationUserService:IService<ApplicationUser>
    {
        void Add(ApplicationUser user);
        void Delete(int id);
        void Update(ApplicationUser user);
        IEnumerable<ApplicationUser> GetAll();
        ApplicationUser Get(int id);
    }
}
