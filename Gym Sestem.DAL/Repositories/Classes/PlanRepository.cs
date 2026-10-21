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
    public class PlanRepository : GenericRepository<Plan>, IPlanRepository
    {
        public PlanRepository(GymDbContext _dbContext) : base(_dbContext)
        {
        }

        public async Task<bool> HasActiveMembershipsAsync(int planId, CancellationToken ct = default)
        {
            return await dbContext.Memberships
                .AnyAsync(m => m.PlanId == planId && m.EndDate > DateTime.Now, ct);
        }
    }
}
