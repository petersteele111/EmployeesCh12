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
    public class BenefitController : Controller
    {
        private readonly EmployeeContext _context;

        public BenefitController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: Benefit
        public async Task<IActionResult> Index()
        {
            var employeeContext = _context.Benefits.Include(b => b.Employee);
            return View(await employeeContext.ToListAsync());
        }

        // GET: Benefit/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var benefits = await _context.Benefits
                .Include(b => b.Employee)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (benefits == null)
            {
                return NotFound();
            }

            return View(benefits);
        }

        // GET: Benefit/Create
        public IActionResult Create()
        {
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "ID", "ID");
            return View();
        }

        // POST: Benefit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,EmployeeID,Category,Dental,Vision,Health,LifeIns")] Benefits benefits)
        {
            if (ModelState.IsValid)
            {
                _context.Add(benefits);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "ID", "ID", benefits.EmployeeID);
            return View(benefits);
        }

        // GET: Benefit/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var benefits = await _context.Benefits.FindAsync(id);
            if (benefits == null)
            {
                return NotFound();
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "ID", "ID", benefits.EmployeeID);
            return View(benefits);
        }

        // POST: Benefit/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,EmployeeID,Category,Dental,Vision,Health,LifeIns")] Benefits benefits)
        {
            if (id != benefits.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(benefits);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BenefitsExists(benefits.ID))
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
            ViewData["EmployeeID"] = new SelectList(_context.Employees, "ID", "ID", benefits.EmployeeID);
            return View(benefits);
        }

        // GET: Benefit/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var benefits = await _context.Benefits
                .Include(b => b.Employee)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (benefits == null)
            {
                return NotFound();
            }

            return View(benefits);
        }

        // POST: Benefit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var benefits = await _context.Benefits.FindAsync(id);
            _context.Benefits.Remove(benefits);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BenefitsExists(int id)
        {
            return _context.Benefits.Any(e => e.ID == id);
        }
    }
}
