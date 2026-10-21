using Gym_Sestem.BLL.Services.Interfaces;
using Gym_Sestem.BLL.ViewModels.TrainersViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Sestem_MVC_Session_1.Controllers
{
    public class TrainerController : Controller
    {
        private readonly ITrainerService trainerService;

        public TrainerController(ITrainerService _trainerService)
        {
            trainerService = _trainerService;
        }

        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var trainers = await trainerService.GetAllAsync(ct);
            return View(trainers);
        }

        public async Task<IActionResult> Details(int id, CancellationToken ct)
        {
            var trainer = await trainerService.GetDetailsAsync(id, ct);
            if (trainer is null)
                return NotFound();

            return View(trainer);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTrainerViewModel model, CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return View(model);

            var error = await trainerService.CreateAsync(model, ct);
            if (error is not null)
            {
                ModelState.AddModelError(string.Empty, error);
                return View(model);
            }

            TempData["Success"] = "Trainer created successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id, CancellationToken ct)
        {
            var model = await trainerService.GetForEditAsync(id, ct);
            if (model is null)
                return NotFound();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TrainerToUpdateViewModel model, CancellationToken ct)
        {
            if (id != model.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(model);

            var error = await trainerService.UpdateAsync(model, ct);
            if (error is not null)
            {
                ModelState.AddModelError(string.Empty, error);
                return View(model);
            }

            TempData["Success"] = "Trainer updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var trainer = await trainerService.GetDetailsAsync(id, ct);
            if (trainer is null)
                return NotFound();

            return View(trainer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken ct)
        {
            var error = await trainerService.DeleteAsync(id, ct);
            if (error is not null)
            {
                TempData["Error"] = error;
                return RedirectToAction(nameof(Delete), new { id });
            }

            TempData["Success"] = "Trainer deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
