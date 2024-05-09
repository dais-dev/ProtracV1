using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Protrac1.Models;
using ProtracV1.Data;
using SQLitePCL;


namespace ProtracV1.Controllers
{
    public class InputRegisterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InputRegisterController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InputRegister
        public async Task<IActionResult> Index()
        {
            return View(await _context.InputRegister.ToListAsync());
        }

// Added methods for Forms App
        public async Task<IActionResult> CreateInputRegister(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var inputRegister = await _context.InputRegister.FindAsync(id);
            if (inputRegister != null)
            {
                return NotFound();
            }

           var model = new MyViewModel
            {
                Id = id.Value,
                Name = "MyName",
            };
            return View(model);
        }

/// end added methods


        // GET: InputRegister/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inputRegister = await _context.InputRegister
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (inputRegister == null)
            {
                return NotFound();
            }

            return View(inputRegister);
        }


        // chnged to get (int id)
        // GET: InputRegister/Create
        public IActionResult Create(int id)
        {
           // if (id == null)
            //{
            //    return NotFound();
            // }
            // int _prid = id;
            return View();
        }

        // POST: InputRegister/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("ProjectId,SerialNumber,ProjectTitle,DataReceived,ReceivedDate,ReceivedFrom,ProjectManagerName,FilesFormat,NumberofFiles,FitforPurpose,Check,CheckedBy,CheckedDate,Custodian,StoragePath,Remarks")] InputRegister inputRegister)
        {
            if (ModelState.IsValid)
            {
                // Added for takig common fields from JobStartForm

              //  var JobStart = _context.JobStartForm.FirstOrDefault();
                
               //  inputRegister.ProjectManagerName = _context.JobStartForm.First().ProjectManagerName;
                 // inputRegister.SerialNumber = _context.JobStartForm.First().SerialNumber;
                inputRegister.SerialNumber = id;
                //

                _context.Add(inputRegister);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inputRegister);
        }

        // GET: InputRegister/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inputRegister = await _context.InputRegister.FindAsync(id);
            if (inputRegister == null)
            {
                return NotFound();
            }
            return View(inputRegister);
        }

        // POST: InputRegister/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectId,SerialNumber,ProjectTitle,DataReceived,ReceivedDate,ReceivedFrom,ProjectManagerName,FilesFormat,NumberofFiles,FitforPurpose,Check,CheckedBy,CheckedDate,Custodian,StoragePath,Remarks")] InputRegister inputRegister)
        {
            if (id != inputRegister.ProjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inputRegister);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InputRegisterExists(inputRegister.ProjectId))
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
            return View(inputRegister);
        }

        // GET: InputRegister/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inputRegister = await _context.InputRegister
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (inputRegister == null)
            {
                return NotFound();
            }

            return View(inputRegister);
        }

        // POST: InputRegister/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inputRegister = await _context.InputRegister.FindAsync(id);
            if (inputRegister != null)
            {
                _context.InputRegister.Remove(inputRegister);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InputRegisterExists(int id)
        {
            return _context.InputRegister.Any(e => e.ProjectId == id);
        }
    }
}
