using Gym_Sestem.DAL.Contexts;
using Gym_Sestem.DAL.Entities;
using Gym_Sestem.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Sestem.DAL.Repositories.Classes
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity, new()
    {
        protected readonly GymDbContext dbContext;
        protected readonly DbSet<TEntity> dbSet;
        public GenericRepository(GymDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<TEntity>();
        }   
        public void Add(TEntity item)
        {
            dbContext.Set<TEntity>().Add(item);
        }

        public async Task<int> CompleteAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var Item = dbContext.Set<TEntity>().FirstOrDefault(p => p.Id == id);
            if (Item is not null)
                dbContext.Set<TEntity>().Remove(Item);
        }

        public async Task<IEnumerable<TEntity>> GetAll(bool isTracked, CancellationToken ct = default)
        {
            var Items = isTracked ? dbContext.Set<TEntity>() : dbContext.Set<TEntity>().AsNoTracking();
            return await Items.ToListAsync();
        }

        public async Task<TEntity?> GetById(int id, CancellationToken ct = default)
        {
            var Item = await dbContext.Set<TEntity>().FirstOrDefaultAsync(p => p.Id == id);
            return Item;
        }

        public void Update(TEntity item)
        {
            dbContext.Set<TEntity>().Update(item);
        }
    }
}
