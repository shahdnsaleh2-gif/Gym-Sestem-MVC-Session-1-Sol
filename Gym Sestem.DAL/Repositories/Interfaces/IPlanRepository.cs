using Gym_Sestem.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Sestem.DAL.Repositories.Interfaces
{
    public interface IPlanRepository : IGenericRepository<Plan>
    {
        Task<bool> HasActiveMembershipsAsync(int planId, CancellationToken ct = default);
    }
}
