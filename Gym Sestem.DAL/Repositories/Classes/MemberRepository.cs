using Gym_Sestem.DAL.Contexts;
using Gym_Sestem.DAL.Entities;
using Gym_Sestem.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Sestem.DAL.Repositories.Classes
{
    public class MemberRepository : GenericRepository<Member>, IMemberRepository
    {
        public MemberRepository(GymDbContext _dbContext) : base(_dbContext)
        {
        }
        public async Task<Member?> GetWithDetailsAsync(int id, CancellationToken ct = default)
        {

            return await dbContext.Members
                .AsNoTracking()
                .Include(m => m.HealthRecord)
                .Include(m => m.Memberships)
                    .ThenInclude(ms => ms.Plan)
                .FirstOrDefaultAsync(m => m.Id == id, ct);
        }

        public async Task<bool> IsEmailTakenAsync(string email, int? excludeId = null, CancellationToken ct = default)
        {
            return await dbContext.Members
                .AnyAsync(m => m.Email == email && (!excludeId.HasValue || m.Id != excludeId.Value), ct);
        }

        public async Task<bool> IsPhoneTakenAsync(string phone, int? excludeId = null, CancellationToken ct = default)
        {
            return await dbContext.Members
                .AnyAsync(m => m.Phone == phone && (!excludeId.HasValue || m.Id != excludeId.Value), ct);
        }

        public async Task<bool> HasBookingsAsync(int memberId, CancellationToken ct = default)
        {
            return await dbContext.Bookings
                .AnyAsync(b => b.MemberId == memberId && b.Session.EndDate > DateTime.Now, ct);
        }
    }
}
