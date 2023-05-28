using APP.Core.Entities;
using APP.Core.Interfaces;
using APP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace APP.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext context;

        public GenericRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task<int> CountAsync()
        =>await context.Set<T>().CountAsync();

        public async Task DeleteAsync(int id)
        {
            var foundEntity =await context.Set<T>().FindAsync(id);
            context.Set<T>().Remove(foundEntity);
            await context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        => await context.Set<T>().AsNoTracking().ToListAsync();



        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            var query = context.Set<T>().AsQueryable();

            //Apply includes
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        => await context.Set<T>().FindAsync(id);

        public async Task<T> GetAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = context.Set<T>().Where(x=>x.Id==id);

            //Apply includes
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(int id, T entity)
        {
            var foundEntity = await context.Set<T>().FindAsync(id);
            context.Set<T>().Update(foundEntity);
            await context.SaveChangesAsync();
        }
    }
}
