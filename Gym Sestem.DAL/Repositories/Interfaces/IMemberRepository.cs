using Gym_Sestem.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Sestem.DAL.Repositories.Interfaces
{
    public interface IMemberRepository : IGenericRepository<Member>
    {
        Task<Member?> GetWithDetailsAsync(int id, CancellationToken ct = default);

        Task<bool> IsEmailTakenAsync(string email, int? excludeId = null, CancellationToken ct = default);
        Task<bool> IsPhoneTakenAsync(string phone, int? excludeId = null, CancellationToken ct = default);

        Task<bool> HasBookingsAsync(int memberId, CancellationToken ct = default);
    }
}
