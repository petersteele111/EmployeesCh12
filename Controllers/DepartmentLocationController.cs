using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeesCh12.Data;
using EmployeesCh12.Models;

namespace EmployeesCh12.Controllers
{
    public class DepartmentLocationController : Controller
    {
        private readonly EmployeeContext _context;

        public DepartmentLocationController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: DepartmentLocation
        public async Task<IActionResult> Index()
        {
            var employeeContext = _context.DepartmentLocations.Include(d => d.Department).Include(d => d.Location);
            return View(await employeeContext.ToListAsync());
        }

        // GET: DepartmentLocation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departmentLocation = await _context.DepartmentLocations
                .Include(d => d.Department)
                .Include(d => d.Location)
                .FirstOrDefaultAsync(m => m.DepartmentID == id);
            if (departmentLocation == null)
            {
                return NotFound();
            }

            return View(departmentLocation);
        }

        // GET: DepartmentLocation/Create
        public IActionResult Create()
        {
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "ID", "ID");
            ViewData["LocationID"] = new SelectList(_context.Locations, "ID", "ID");
            return View();
        }

        // POST: DepartmentLocation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepartmentID,LocationID")] DepartmentLocation departmentLocation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(departmentLocation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "ID", "ID", departmentLocation.DepartmentID);
            ViewData["LocationID"] = new SelectList(_context.Locations, "ID", "ID", departmentLocation.LocationID);
            return View(departmentLocation);
        }

        // GET: DepartmentLocation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departmentLocation = await _context.DepartmentLocations.FindAsync(id);
            if (departmentLocation == null)
            {
                return NotFound();
            }
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "ID", "ID", departmentLocation.DepartmentID);
            ViewData["LocationID"] = new SelectList(_context.Locations, "ID", "ID", departmentLocation.LocationID);
            return View(departmentLocation);
        }

        // POST: DepartmentLocation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DepartmentID,LocationID")] DepartmentLocation departmentLocation)
        {
            if (id != departmentLocation.DepartmentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(departmentLocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentLocationExists(departmentLocation.DepartmentID))
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
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "ID", "ID", departmentLocation.DepartmentID);
            ViewData["LocationID"] = new SelectList(_context.Locations, "ID", "ID", departmentLocation.LocationID);
            return View(departmentLocation);
        }

        // GET: DepartmentLocation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departmentLocation = await _context.DepartmentLocations
                .Include(d => d.Department)
                .Include(d => d.Location)
                .FirstOrDefaultAsync(m => m.DepartmentID == id);
            if (departmentLocation == null)
            {
                return NotFound();
            }

            return View(departmentLocation);
        }

        // POST: DepartmentLocation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var departmentLocation = await _context.DepartmentLocations.FindAsync(id);
            _context.DepartmentLocations.Remove(departmentLocation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentLocationExists(int id)
        {
            return _context.DepartmentLocations.Any(e => e.DepartmentID == id);
        }
    }
}
