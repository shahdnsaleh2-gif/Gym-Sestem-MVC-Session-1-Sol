using Gym_Sestem_MVC_Session_1.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Gym_Sestem_MVC_Session_1.Controllers
{
    public class PlanController : Controller
    {

        private readonly GymDbContext dbcontext = new GymDbContext();
        public async Task<IActionResult> Index()
        {
            var plans = await dbcontext.Plans.ToListAsync();

            return View(plans);
        }

        public async Task<IActionResult> Details(int id)
        {
            var plan = await dbcontext.Plans.FirstOrDefaultAsync(p => p.Id == id);

            if (plan == null)
            {
                RedirectToAction(nameof(Index));
            }

            return View(plan);
        }
    }
}
