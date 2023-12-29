using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarServiceMVCApp.Data;
using CarServiceMVCApp.Models;

namespace CarServiceMVCApp.Controllers
{
    public class RepairJobsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RepairJobsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RepairJobs
        public async Task<IActionResult> Index()
        {
            return View(await _context.RepairJobs.ToListAsync());
        }

        // GET: RepairJobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repairJob = await _context.RepairJobs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (repairJob == null)
            {
                return NotFound();
            }

            return View(repairJob);
        }

        // GET: RepairJobs/Create
        public async Task<IActionResult> Create([FromQuery] int carId)
        {
            var car = await _context.Cars.FirstOrDefaultAsync(c => c.Id ==  carId);

            var repairJob = new RepairJob()
            {
                Car = car,
                RepairName = "",
                StartDate = DateTime.Now,
                EndDate = null
            };

            return View(repairJob);
        }

        // POST: RepairJobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartDate,EndDate,RepairName,Description,Completed,Car")] RepairJob repairJob)
        {

            if (ModelState.IsValid)
            {
                _context.Add(repairJob);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(repairJob);
        }

        // GET: RepairJobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repairJob = await _context.RepairJobs.FindAsync(id);
            if (repairJob == null)
            {
                return NotFound();
            }
            return View(repairJob);
        }

        // POST: RepairJobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartDate,EndDate,RepairName,Description,Completed")] RepairJob repairJob)
        {
            if (id != repairJob.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(repairJob);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RepairJobExists(repairJob.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(repairJob);
        }

        // GET: RepairJobs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repairJob = await _context.RepairJobs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (repairJob == null)
            {
                return NotFound();
            }

            return View(repairJob);
        }

        // POST: RepairJobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var repairJob = await _context.RepairJobs.FindAsync(id);
            if (repairJob != null)
            {
                _context.RepairJobs.Remove(repairJob);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RepairJobExists(int id)
        {
            return _context.RepairJobs.Any(e => e.Id == id);
        }
    }
}
