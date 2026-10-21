using Gym_Sestem.BLL.ViewModels.MembersViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Sestem.BLL.Services.Interfaces
{
    public interface IMemberService
    {
        Task<IEnumerable<MemberViewModel>> GetAllAsync(CancellationToken ct = default);
        Task<MemberViewModel?> GetDetailsAsync(int id, CancellationToken ct = default);
        Task<MemberToUpdateViewModel?> GetForEditAsync(int id, CancellationToken ct = default);

        Task<string?> CreateAsync(CreateMemberViewModel model, CancellationToken ct = default);
        Task<string?> UpdateAsync(MemberToUpdateViewModel model, CancellationToken ct = default);
        Task<string?> DeleteAsync(int id, CancellationToken ct = default);
    }
}
