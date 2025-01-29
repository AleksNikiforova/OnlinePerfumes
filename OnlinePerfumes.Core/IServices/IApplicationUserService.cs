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
        Task Add(ApplicationUser user);
        Task Delete(int id);
        Task Update(ApplicationUser user);
       Task<IEnumerable<ApplicationUser>> GetAll();
        Task<ApplicationUser> GetById(int id);
    }
}
