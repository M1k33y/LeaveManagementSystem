using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeaveManagementSystem.Web.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Web.Controllers
{
    public class LeaveTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeaveTypesController(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var leaveTypes = await _context.LeaveTypes.ToListAsync();
            return View(leaveTypes);
        }



        // GET: LeaveTypesController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LeaveTypesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,NumberOfDays")] LeaveType leaveType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(leaveType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(leaveType);


        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var leaveType = await _context.LeaveTypes.FirstOrDefaultAsync(m => m.Id == id);

            if (leaveType == null)
                return NotFound();

            return View(leaveType);
        }

        // GET: LeaveTypesController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var leaveType = await _context.LeaveTypes.FindAsync(id);

            if (leaveType == null)
                return NotFound();

            return View(leaveType);
        }

        // POST: LeaveTypesController/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,NumberOfDays")] LeaveType leaveType)
        {
            if (id != leaveType.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leaveType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveTypeExists(leaveType.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(leaveType);
        }
        private bool LeaveTypeExists(int id)
        {
            return _context.LeaveTypes.Any(e => e.Id == id);
        }

        // GET: LeaveTypesController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var leaveType = await _context.LeaveTypes.FirstOrDefaultAsync(m => m.Id == id);

            if (leaveType == null)
                return NotFound();

            return View(leaveType);
        }

        // POST: LeaveTypesController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leaveType = await _context.LeaveTypes.FindAsync(id);
            if (leaveType != null)
            {
                _context.LeaveTypes.Remove(leaveType);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
