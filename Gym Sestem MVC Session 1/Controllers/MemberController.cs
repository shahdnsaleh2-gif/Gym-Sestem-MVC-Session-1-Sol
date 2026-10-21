using Gym_Sestem.BLL.Services.Interfaces;
using Gym_Sestem.BLL.ViewModels.MembersViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Sestem_MVC_Session_1.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService memberService;

        public MemberController(IMemberService _memberService)
        {
            memberService = _memberService;
        }

        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var members = await memberService.GetAllAsync(ct);
            return View(members);
        }

        public async Task<IActionResult> Details(int id, CancellationToken ct)
        {
            var member = await memberService.GetDetailsAsync(id, ct);
            if (member is null)
                return NotFound();

            return View(member);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateMemberViewModel model, CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return View(model);

            var error = await memberService.CreateAsync(model, ct);
            if (error is not null)
            {
                ModelState.AddModelError(string.Empty, error);
                return View(model);
            }

            TempData["Success"] = "Member created successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id, CancellationToken ct)
        {
            var model = await memberService.GetForEditAsync(id, ct);
            if (model is null)
                return NotFound();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MemberToUpdateViewModel model, CancellationToken ct)
        {
            if (id != model.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(model);

            var error = await memberService.UpdateAsync(model, ct);
            if (error is not null)
            {
                ModelState.AddModelError(string.Empty, error);
                return View(model);
            }

            TempData["Success"] = "Member updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var member = await memberService.GetDetailsAsync(id, ct);
            if (member is null)
                return NotFound();

            return View(member);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken ct)
        {
            var error = await memberService.DeleteAsync(id, ct);
            if (error is not null)
            {
                TempData["Error"] = error;
                return RedirectToAction(nameof(Delete), new { id });
            }

            TempData["Success"] = "Member deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
