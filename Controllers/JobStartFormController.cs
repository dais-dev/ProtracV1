using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Protrac1.Models;
using ProtracV1.Data;
using Microsoft.AspNetCore.Authorization;
using MimeKit;
using MailKit;

namespace ProtracV1.Controllers
{
   // [Authorize(Roles = "Admin")]
    public class JobStartFormController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobStartFormController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: JobStartForm
        public async Task<IActionResult> Index()
        {
            return View(await _context.JobStartForm.ToListAsync());
        }

         // Added methods for Asset App
        public IActionResult CreateJobStartForm()
        {
            return View();
        }
        
        public async Task<IActionResult> ViewProjects()
        {       
            return View(await _context.JobStartForm.ToListAsync());
        }
        public async Task<IActionResult> ViewForms(int? id)
        {
              if (id == null)
            {
                return NotFound();
            }

            var jobStartForm = await _context.JobStartForm.FindAsync(id);
            if (jobStartForm == null)
            {
                return NotFound();
            }
            return View(jobStartForm);

        }

        /// end Added methods


        // GET: JobStartForm/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobStartForm = await _context.JobStartForm
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (jobStartForm == null)
            {
                return NotFound();
            }

            return View(jobStartForm);
        }

        // GET: JobStartForm/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobStartForm/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectId,SerialNumber,ProjectTitle,ProjectDetail,ClientName,ProjectManagerName,TypeofJob,StartDate,EndDate,Sector,Office,Region,SPMName,EstimatedProjectCost,Duration")] JobStartForm jobStartForm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobStartForm);
                await _context.SaveChangesAsync();
                RedirectToAction("SendEmail","Home");
                return RedirectToAction(nameof(Index));
            }
            return View(jobStartForm);
        }

        // GET: JobStartForm/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobStartForm = await _context.JobStartForm.FindAsync(id);
            if (jobStartForm == null)
            {
                return NotFound();
            }
            return View(jobStartForm);
        }

        // POST: JobStartForm/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectId,SerialNumber,ProjectTitle,ProjectDetail,ClientName,ProjectManagerName,TypeofJob,StartDate,EndDate,Sector,Office,Region,SPMName,EstimatedProjectCost,Duration")] JobStartForm jobStartForm)
        {
            if (id != jobStartForm.ProjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobStartForm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobStartFormExists(jobStartForm.ProjectId))
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
            return View(jobStartForm);
        }

        // GET: JobStartForm/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobStartForm = await _context.JobStartForm
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (jobStartForm == null)
            {
                return NotFound();
            }

            return View(jobStartForm);
        }

        // POST: JobStartForm/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobStartForm = await _context.JobStartForm.FindAsync(id);
            if (jobStartForm != null)
            {
                _context.JobStartForm.Remove(jobStartForm);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobStartFormExists(int id)
        {
            return _context.JobStartForm.Any(e => e.ProjectId == id);
        }
    }
}
