using Gym_Sestem.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Gym_Sestem_MVC_Session_1.Controllers
{
    public class PlanController : Controller
    {
        private readonly IPlanRepository planRepository;
        public PlanController(IPlanRepository _planRepository)
        {
            planRepository = _planRepository;
        }
        public async Task<IActionResult> Index(CancellationToken token)
        {
            var plans = await planRepository.GetAll(false, token);
            return View(plans);
        }
        public async Task<IActionResult> Details(int id, CancellationToken token)
        {
            var plan = await planRepository.GetById(id, token);
            if (plan == null)
                RedirectToAction(nameof(Index));
            return View(plan);
        }
    }
}
