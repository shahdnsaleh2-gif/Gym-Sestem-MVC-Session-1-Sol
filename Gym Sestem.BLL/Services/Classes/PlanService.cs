using Gym_Sestem.BLL.Services.Interfaces;
using Gym_Sestem.BLL.ViewModels.PlansViewModels;
using Gym_Sestem.DAL.Entities;
using Gym_Sestem.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Sestem.BLL.Services.Classes
{
    public class PlanService : IPlanService
    {
        private readonly IPlanRepository planRepository;

        public PlanService(IPlanRepository _planRepository)
        {
            planRepository = _planRepository;
        }

        public async Task<IEnumerable<Plan>> GetAllAsync(CancellationToken ct = default)
        {
            return await planRepository.GetAll(false, ct);
        }

        public async Task<Plan?> GetDetailsAsync(int id, CancellationToken ct = default)
        {
            return await planRepository.GetById(id, ct);
        }

        public async Task<PlanToUpdateViewModel?> GetForEditAsync(int id, CancellationToken ct = default)
        {
            var plan = await planRepository.GetById(id, ct);
            if (plan is null) return null;

            return new PlanToUpdateViewModel
            {
                Id = plan.Id,
                Name = plan.Name,
                Duration = plan.Duration,
                Price = plan.Price,
                Description = plan.Description
            };
        }

        public async Task<string?> UpdateAsync(PlanToUpdateViewModel model, CancellationToken ct = default)
        {
            var plan = await planRepository.GetById(model.Id, ct);
            if (plan is null)
                return "Plan not found.";

            if (await planRepository.HasActiveMembershipsAsync(model.Id, ct))
                return "Cannot update a plan that has active memberships.";

            plan.Duration = model.Duration;
            plan.Price = model.Price;
            plan.Description = model.Description;

            planRepository.Update(plan);
            await planRepository.CompleteAsync();
            return null;
        }

        public async Task<string?> ToggleActivationAsync(int id, CancellationToken ct = default)
        {
            var plan = await planRepository.GetById(id, ct);
            if (plan is null)
                return "Plan not found.";

            if (plan.IsActive && await planRepository.HasActiveMembershipsAsync(id, ct))
                return "Cannot deactivate a plan that has active memberships.";

            plan.IsActive = !plan.IsActive;
            planRepository.Update(plan);
            await planRepository.CompleteAsync();
            return null;
        }
    }
}
