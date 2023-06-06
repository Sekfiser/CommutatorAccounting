using System.Diagnostics;
using CommutatorAccounting.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CommutatorAccounting.Controllers
{
    public class CommutatorController : Controller
    {
        ApplicationContext db;

        private readonly ILogger<CommutatorController> _logger;

        public CommutatorController(ILogger<CommutatorController> logger, ApplicationContext db)
        {
            _logger = logger;
            this.db = db;
        }

        public async Task<IActionResult> List()
        {
            ViewBag.Commutators = await db.Commutators.ToListAsync();
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCommutator(Commutator commutator)
        {
            db.Commutators.Add(commutator);
            await db.SaveChangesAsync();
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Commutator commutator = new Commutator { Id = id.Value };
                db.Entry(commutator).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("List");
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Commutator? commutator = await db.Commutators.FirstOrDefaultAsync(p => p.Id == id);
                if (commutator != null) return View(commutator);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Commutator commutator)
        {
            db.Commutators.Update(commutator);
            await db.SaveChangesAsync();
            return RedirectToAction("List");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}