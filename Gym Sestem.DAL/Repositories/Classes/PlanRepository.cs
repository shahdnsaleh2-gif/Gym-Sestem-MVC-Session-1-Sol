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
    public class PlanRepository : IPlanRepository
    {
        private readonly GymDbContext dbContext;
        public PlanRepository(GymDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public async Task<IEnumerable<Plan>> GetAll(bool isTracked, CancellationToken ct = default)
        {
            var plans = isTracked ? dbContext.Plans : dbContext.Plans.AsNoTracking();
            return await plans.ToListAsync();
        }
        public async Task<Plan?> GetById(int id, CancellationToken ct = default)
        {
            var plan = await dbContext.Plans.FirstOrDefaultAsync(p => p.Id == id);
            return plan;
        }

        public void Add(Plan plan)
        {
            dbContext.Add(plan);
        }

        public void Update(Plan plan)
        {
            dbContext.Update(plan);
        }

        public void Delete(int id)
        {
            var plan = dbContext.Plans.FirstOrDefault(p => p.Id == id);
            if (plan is not null)
                dbContext.Plans.Remove(plan);
        }

        public async Task<int> CompleteAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

    }
}
