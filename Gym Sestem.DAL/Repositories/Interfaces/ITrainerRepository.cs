using Gym_Sestem.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Sestem.DAL.Repositories.Interfaces
{
    public interface ITrainerRepository : IGenericRepository<Trainer>
    {
        Task<bool> IsEmailTakenAsync(string email, int? excludeId = null, CancellationToken ct = default);
        Task<bool> IsPhoneTakenAsync(string phone, int? excludeId = null, CancellationToken ct = default);

        Task<bool> HasSessionsAsync(int trainerId, CancellationToken ct = default);
    }
}
