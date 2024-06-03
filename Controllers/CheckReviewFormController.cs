using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProtracV1.Models;
using ProtracV1.Data;

namespace ProtracV1.Controllers
{
    public class CheckReviewFormController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CheckReviewFormController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CheckReviewForm
        public async Task<IActionResult> Index()
        {
            return View(await _context.CheckReviewForm.ToListAsync());
        }

        // GET: CheckReviewForm/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkReviewForm = await _context.CheckReviewForm
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (checkReviewForm == null)
            {
                return NotFound();
            }

            return View(checkReviewForm);
        }

        // chnged Create to get (int id). also changed the method to async
        // GET: CheckReviewForm/Create
        
        public async Task<IActionResult> Create(int id)
        {
          //  added to check exists
            var checkReviewForm = await _context.CheckReviewForm.FindAsync(id);
            if (checkReviewForm != null)
            {
                return View(checkReviewForm);
            }

            return View();
        }

        // POST: CheckReviewForm/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("ProjectId,SerialNumber,ProjectTitle,ActivityNumber,ActivityName,Objectives,ReferenceStandards,FileName,SpecificQualityIssues,Completion,CheckedBy,CheckedByDate,ApprovedBy,ApprovedByDate,ActionTaken")] CheckReviewForm checkReviewForm)
        {
            if (ModelState.IsValid)
            {
                // Added for takig common fields from JobStartForm

                  //  This was only for test before id was passed to View  
                  //  var JobStart = _context.JobStartForm.FirstOrDefault();
                  // inputRegister.SerialNumber = _context.JobStartForm.First().SerialNumber;

                checkReviewForm.ProjectId = id; 
                checkReviewForm.SerialNumber = id;
                // SerialNumber = id  just to display the id, to be changed later on


                _context.Add(checkReviewForm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(checkReviewForm);
        }

        // GET: CheckReviewForm/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkReviewForm = await _context.CheckReviewForm.FindAsync(id);
            if (checkReviewForm == null)
            {
                return NotFound();
            }
            return View(checkReviewForm);
        }

        // POST: CheckReviewForm/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectId,SerialNumber,ProjectTitle,ActivityNumber,ActivityName,Objectives,ReferenceStandards,FileName,SpecificQualityIssues,Completion,CheckedBy,CheckedByDate,ApprovedBy,ApprovedByDate,ActionTaken")] CheckReviewForm checkReviewForm)
        {
            if (id != checkReviewForm.ProjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(checkReviewForm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CheckReviewFormExists(checkReviewForm.ProjectId))
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
            return View(checkReviewForm);
        }

        // GET: CheckReviewForm/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkReviewForm = await _context.CheckReviewForm
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (checkReviewForm == null)
            {
                return NotFound();
            }

            return View(checkReviewForm);
        }

        // POST: CheckReviewForm/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var checkReviewForm = await _context.CheckReviewForm.FindAsync(id);
            if (checkReviewForm != null)
            {
                _context.CheckReviewForm.Remove(checkReviewForm);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CheckReviewFormExists(int id)
        {
            return _context.CheckReviewForm.Any(e => e.ProjectId == id);
        }
    }
}
