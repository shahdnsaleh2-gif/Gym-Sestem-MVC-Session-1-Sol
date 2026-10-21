using Gym_Sestem.BLL.Services.Interfaces;
using Gym_Sestem.BLL.ViewModels.PlansViewModels;
using Gym_Sestem.DAL.Entities;
using Gym_Sestem.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Gym_Sestem_MVC_Session_1.Controllers
{
    public class PlanController : Controller
    {
        private readonly IPlanService planService;

        public PlanController(IPlanService _planService)
        {
            planService = _planService;
        }

        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var plans = await planService.GetAllAsync(ct);
            return View(plans);
        }

        public async Task<IActionResult> Details(int id, CancellationToken ct)
        {
            var plan = await planService.GetDetailsAsync(id, ct);
            if (plan is null)
                return RedirectToAction(nameof(Index));

            return View(plan);
        }

        public async Task<IActionResult> Edit(int id, CancellationToken ct)
        {
            var model = await planService.GetForEditAsync(id, ct);
            if (model is null)
                return NotFound();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PlanToUpdateViewModel model, CancellationToken ct)
        {
            if (id != model.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(model);

            var error = await planService.UpdateAsync(model, ct);
            if (error is not null)
            {
                ModelState.AddModelError(string.Empty, error);
                return View(model);
            }

            TempData["Success"] = "Plan updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Activate(int id, CancellationToken ct)
        {
            var error = await planService.ToggleActivationAsync(id, ct);
            TempData[error is null ? "Success" : "Error"] = error ?? "Plan Status Changed";
            return RedirectToAction(nameof(Index));
        }
    }
}
