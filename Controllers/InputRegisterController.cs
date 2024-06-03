using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProtracV1.Models;
using ProtracV1.Data;
using SQLitePCL;
using Microsoft.AspNetCore.Identity;
using ExcelDataReader;

using ProtracV1.Areas.Identity.Data;

namespace ProtracV1.Controllers
{
   // [Authorize(Roles = "Admin")]
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

            // model is For Passing the Id (ProjectId) to the View

            var model = new MyViewModel
            {
                Id = id.Value,
                Name = "MyName",
              
            };
               
            var inputRegister = await _context.InputRegister.FindAsync(id);
            if (inputRegister != null)
            {
                await Details(id);
                model.Exists = true;
                return View(model);
      
            }

            else {
                model.Exists = false;
                return View(model);
            }
        }
////
////
///
    [HttpPost]
    public async Task<IActionResult> Import(IFormFile file)
    {
        if (file == null || file.Length <= 0)
        {
            return BadRequest("Upload a file.");
        }

        if (!Path.GetExtension(file.FileName).Equals(".xls", StringComparison.OrdinalIgnoreCase))
        {
            return BadRequest("Not support file extension.");
        }

        var list = new List<InputRegister>();

        using (var stream = new MemoryStream())
        {
            await file.CopyToAsync(stream);
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                while (reader.Read()) //Each row of the file
                {
                    list.Add(new InputRegister { 
                        // Assuming your model has two properties for example
                        ProjectId = Convert.ToInt32(reader.GetValue(0)),
                        SerialNumber =  Convert.ToInt32(reader.GetValue(1)),
                        DataReceived = reader.GetValue(2).ToString(),
                        ReceivedFrom = reader.GetValue(3).ToString(),
                    });
                }
            }
        }

        await _context.AddRangeAsync(list);
        await _context.SaveChangesAsync();

        return Ok("Data imported successfully");
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


        // chnged Create to get (int id). also changed the method to async
        // GET: InputRegister/Create
        public async Task<IActionResult> Create(int id)
        {
           // if (id == null)
            //{
            //    return NotFound();
            // }

            var inputRegister = await _context.InputRegister.FindAsync(id);
            if (inputRegister != null)
            {
                return View(inputRegister);
            }

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

                  //  This was only for test before id was passed to View  
                  //  var JobStart = _context.JobStartForm.FirstOrDefault();
                  // inputRegister.SerialNumber = _context.JobStartForm.First().SerialNumber;

                inputRegister.ProjectId = id; 
                inputRegister.SerialNumber = id;
                // SerialNumber = id  just to display the id, to be changed later on

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
