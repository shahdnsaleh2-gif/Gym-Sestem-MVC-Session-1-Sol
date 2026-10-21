using Gym_Sestem.BLL.ViewModels.TrainersViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Sestem.BLL.Services.Interfaces
{
    public interface ITrainerService
    {
        Task<IEnumerable<TrainerViewModel>> GetAllAsync(CancellationToken ct = default);
        Task<TrainerViewModel?> GetDetailsAsync(int id, CancellationToken ct = default);
        Task<TrainerToUpdateViewModel?> GetForEditAsync(int id, CancellationToken ct = default);

        Task<string?> CreateAsync(CreateTrainerViewModel model, CancellationToken ct = default);
        Task<string?> UpdateAsync(TrainerToUpdateViewModel model, CancellationToken ct = default);
        Task<string?> DeleteAsync(int id, CancellationToken ct = default);
    }
}
