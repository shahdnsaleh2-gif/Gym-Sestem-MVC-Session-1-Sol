using Gym_Sestem.BLL.Services.Interfaces;
using Gym_Sestem.BLL.ViewModels.MembersViewModels;
using Gym_Sestem.DAL.Entities;
using Gym_Sestem.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Sestem.BLL.Services.Classes
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository memberRepository;

        public MemberService(IMemberRepository _memberRepository)
        {
            memberRepository = _memberRepository;
        }

        public async Task<IEnumerable<MemberViewModel>> GetAllAsync(CancellationToken ct = default)
        {
            var members = await memberRepository.GetAll(false, ct);
            return members.Select(MapToViewModel);
        }

        public async Task<MemberViewModel?> GetDetailsAsync(int id, CancellationToken ct = default)
        {
            var member = await memberRepository.GetWithDetailsAsync(id, ct);
            return member is null ? null : MapToViewModel(member);
        }

        public async Task<MemberToUpdateViewModel?> GetForEditAsync(int id, CancellationToken ct = default)
        {
            var member = await memberRepository.GetById(id, ct);
            if (member is null) return null;

            return new MemberToUpdateViewModel
            {
                Id = member.Id,
                Email = member.Email,
                Phone = member.Phone,
                BuildingNumber = member.Address.BuildingNumber,
                City = member.Address.City,
                Street = member.Address.Street
            };
        }

        public async Task<string?> CreateAsync(CreateMemberViewModel model, CancellationToken ct = default)
        {
            if (await memberRepository.IsEmailTakenAsync(model.Email, null, ct))
                return "This email is already registered.";

            if (await memberRepository.IsPhoneTakenAsync(model.Phone, null, ct))
                return "This phone number is already registered.";

            var member = new Member
            {
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone,
                DateOfBirth = model.DateOfBirth,
                Gender = model.Gender,
                Address = new Address
                {
                    BuildingNumber = model.BuildingNumber,
                    City = model.City,
                    Street = model.Street
                },
                HealthRecord = new HealthRecord
                {
                    Height = model.HealthRecordViewModel.Height,
                    Weight = model.HealthRecordViewModel.Weight,
                    BloodType = model.HealthRecordViewModel.BloodType,
                    Note = model.HealthRecordViewModel.Note
                }
            };

            memberRepository.Add(member);
            await memberRepository.CompleteAsync();
            return null;
        }

        public async Task<string?> UpdateAsync(MemberToUpdateViewModel model, CancellationToken ct = default)
        {
            var member = await memberRepository.GetById(model.Id, ct);
            if (member is null)
                return "Member not found.";

            if (await memberRepository.IsEmailTakenAsync(model.Email, model.Id, ct))
                return "This email is already used by another member.";

            if (await memberRepository.IsPhoneTakenAsync(model.Phone, model.Id, ct))
                return "This phone number is already used by another member.";

            member.Email = model.Email;
            member.Phone = model.Phone;
            member.Address.BuildingNumber = model.BuildingNumber;
            member.Address.City = model.City;
            member.Address.Street = model.Street;

            memberRepository.Update(member);
            await memberRepository.CompleteAsync();
            return null;
        }

        public async Task<string?> DeleteAsync(int id, CancellationToken ct = default)
        {
            if (await memberRepository.HasBookingsAsync(id, ct))
                return "Cannot delete a member with active bookings.";

            memberRepository.Delete(id);
            await memberRepository.CompleteAsync();
            return null;
        }

        private static MemberViewModel MapToViewModel(Member member)
        {
            var latestMembership = member.Memberships
                .OrderByDescending(m => m.EndDate)
                .FirstOrDefault();

            return new MemberViewModel
            {
                Id = member.Id,
                Photo = member.Photo,
                Name = member.Name,
                Email = member.Email,
                Phone = member.Phone,
                Gender = member.Gender.ToString(),
                DateOfBirth = member.DateOfBirth.ToString("yyyy-MM-dd"),
                Address = $"{member.Address.Street}, {member.Address.City} (Bldg {member.Address.BuildingNumber})",
                Height = member.HealthRecord?.Height,
                Weight = member.HealthRecord?.Weight,
                BloodType = member.HealthRecord?.BloodType,
                Note = member.HealthRecord?.Note,
                PlanName = latestMembership?.Plan?.Name,
                MembershipStartDate = latestMembership?.CreatedAt.ToString("yyyy-MM-dd"),
                MembershipEndDate = latestMembership?.EndDate.ToString("yyyy-MM-dd")
            };
        }
    }
}
