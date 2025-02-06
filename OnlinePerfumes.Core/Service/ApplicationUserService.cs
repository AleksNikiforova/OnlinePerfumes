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
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IRepository<ApplicationUser> _repo; 
        public ApplicationUserService(IRepository<ApplicationUser> repo)
        {
            _repo = repo;
        }

        public async Task Add(ApplicationUser user)
        {
            await _repo.Add(user);
        }

        public async Task Delete(int id)
        {
            await _repo.Delete(id);
        }

        public async Task<List<ApplicationUser>> Find(Expression<Func<ApplicationUser, bool>> filter)
        {
           return await _repo.Find(filter);
        }

        public IQueryable<ApplicationUser> GetAll()
        {
            return _repo.GetAll();
        }

        public async Task<ApplicationUser> GetById(int id)
        {
           return await _repo.GetById(id);
        }

        public async Task Update(ApplicationUser user)
        {
            await _repo.Update(user);
        }

       
    }
}
