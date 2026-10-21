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
    public class TrainerRepository : GenericRepository<Trainer>, ITrainerRepository
    {
        public TrainerRepository(GymDbContext _dbContext) : base(_dbContext)
        {
        }

        public async Task<bool> IsEmailTakenAsync(string email, int? excludeId = null, CancellationToken ct = default)
        {
            return await dbContext.Trainers
                .AnyAsync(t => t.Email == email && (!excludeId.HasValue || t.Id != excludeId.Value), ct);
        }

        public async Task<bool> IsPhoneTakenAsync(string phone, int? excludeId = null, CancellationToken ct = default)
        {
            return await dbContext.Trainers
                .AnyAsync(t => t.Phone == phone && (!excludeId.HasValue || t.Id != excludeId.Value), ct);
        }

        public async Task<bool> HasSessionsAsync(int trainerId, CancellationToken ct = default)
        {
            return await dbContext.Sessions
                .AnyAsync(s => s.TrainerId == trainerId && s.EndDate > DateTime.Now, ct);
        }
    }
}
