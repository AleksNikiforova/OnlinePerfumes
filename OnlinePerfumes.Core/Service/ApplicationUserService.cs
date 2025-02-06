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
    public class ApplicationUserService : IApplicationUserService
    {
        public async Task Add(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ApplicationUser>> Find(Expression<Func<ApplicationUser, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ApplicationUser> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationUser> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task Update(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

       
    }
}
