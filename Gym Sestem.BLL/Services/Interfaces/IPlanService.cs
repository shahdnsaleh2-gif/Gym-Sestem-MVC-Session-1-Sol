using Gym_Sestem.BLL.ViewModels.PlansViewModels;
using Gym_Sestem.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Sestem.BLL.Services.Interfaces
{
    public interface IPlanService
    {
        Task<IEnumerable<Plan>> GetAllAsync(CancellationToken ct = default);
        Task<Plan?> GetDetailsAsync(int id, CancellationToken ct = default);
        Task<PlanToUpdateViewModel?> GetForEditAsync(int id, CancellationToken ct = default);

        Task<string?> UpdateAsync(PlanToUpdateViewModel model, CancellationToken ct = default);
        Task<string?> ToggleActivationAsync(int id, CancellationToken ct = default);
    }
}
