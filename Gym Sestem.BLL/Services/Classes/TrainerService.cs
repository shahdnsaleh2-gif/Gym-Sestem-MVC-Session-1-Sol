using Gym_Sestem.BLL.Services.Interfaces;
using Gym_Sestem.BLL.ViewModels.TrainersViewModels;
using Gym_Sestem.DAL.Entities;
using Gym_Sestem.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Sestem.BLL.Services.Classes
{
    public class TrainerService : ITrainerService
    {
        private readonly ITrainerRepository trainerRepository;

        public TrainerService(ITrainerRepository _trainerRepository)
        {
            trainerRepository = _trainerRepository;
        }

        public async Task<IEnumerable<TrainerViewModel>> GetAllAsync(CancellationToken ct = default)
        {
            var trainers = await trainerRepository.GetAll(false, ct);
            return trainers.Select(t => MapToViewModel(t, detailed: false));
        }

        public async Task<TrainerViewModel?> GetDetailsAsync(int id, CancellationToken ct = default)
        {
            var trainer = await trainerRepository.GetById(id, ct);
            return trainer is null ? null : MapToViewModel(trainer, detailed: true);
        }

        public async Task<TrainerToUpdateViewModel?> GetForEditAsync(int id, CancellationToken ct = default)
        {
            var trainer = await trainerRepository.GetById(id, ct);
            if (trainer is null) return null;

            return new TrainerToUpdateViewModel
            {
                Id = trainer.Id,
                Name = trainer.Name,
                DateOfBirth = trainer.DateOfBirth,
                Gender = trainer.Gender,
                Email = trainer.Email,
                Phone = trainer.Phone,
                BuildingNumber = trainer.Address.BuildingNumber,
                City = trainer.Address.City,
                Street = trainer.Address.Street,
                Specialty = trainer.specialize
            };
        }

        public async Task<string?> CreateAsync(CreateTrainerViewModel model, CancellationToken ct = default)
        {
            if (await trainerRepository.IsEmailTakenAsync(model.Email, null, ct))
                return "This email is already registered.";

            if (await trainerRepository.IsPhoneTakenAsync(model.Phone, null, ct))
                return "This phone number is already registered.";

            var trainer = new Trainer
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
                specialize = model.Specialty
            };

            trainerRepository.Add(trainer);
            await trainerRepository.CompleteAsync();
            return null;
        }

        public async Task<string?> UpdateAsync(TrainerToUpdateViewModel model, CancellationToken ct = default)
        {
            var trainer = await trainerRepository.GetById(model.Id, ct);
            if (trainer is null)
                return "Trainer not found.";

            if (await trainerRepository.IsEmailTakenAsync(model.Email, model.Id, ct))
                return "This email is already used by another trainer.";

            if (await trainerRepository.IsPhoneTakenAsync(model.Phone, model.Id, ct))
                return "This phone number is already used by another trainer.";

            trainer.Email = model.Email;
            trainer.Phone = model.Phone;
            trainer.Address.BuildingNumber = model.BuildingNumber;
            trainer.Address.City = model.City;
            trainer.Address.Street = model.Street;
            trainer.specialize = model.Specialty;

            trainerRepository.Update(trainer);
            await trainerRepository.CompleteAsync();
            return null;
        }

        public async Task<string?> DeleteAsync(int id, CancellationToken ct = default)
        {
            if (await trainerRepository.HasSessionsAsync(id, ct))
                return "Cannot delete a trainer with scheduled sessions.";

            trainerRepository.Delete(id);
            await trainerRepository.CompleteAsync();
            return null;
        }

        private static TrainerViewModel MapToViewModel(Trainer trainer, bool detailed)
        {
            var vm = new TrainerViewModel
            {
                Id = trainer.Id,
                Name = trainer.Name,
                Email = trainer.Email,
                Phone = trainer.Phone,
                Specialty = trainer.specialize.ToString()
            };

            if (detailed)
            {
                vm.Specialty = $"{trainer.specialize} Trainer";
                vm.Gender = trainer.Gender.ToString();
                vm.DateOfBirth = trainer.DateOfBirth.ToString("yyyy-MM-dd");
                vm.Address = $"{trainer.Address.Street}, {trainer.Address.City} (Bldg {trainer.Address.BuildingNumber})";
            }

            return vm;
        }
    }
}
