using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bar_rating.Data;
using Bar_rating.Models;
using Microsoft.AspNetCore.Authorization;

namespace Bar_rating.Controllers
{
    [Authorize(Roles = "Admin, Employee")]
    public class BarsController : Controller
    {
        private readonly AppDbContext _context;

        public BarsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return _context.Bars != null ?
                        View(await _context.Bars.ToListAsync()) :
                        Problem("Entity set 'AppDbContext.Bars'  is null.");
        }

        public async Task<IActionResult> Details(int? id)
        {

            if (id == null || _context.Bars == null)
            {
                return NotFound();
            }

            var bar = await _context.Bars
                .Include(f => f.Reviews)
                .FirstOrDefaultAsync(m => m.Id == id);


            if (bar == null)
            {
                return NotFound();
            }

            return View(bar);
        }

        [Authorize(Roles = "Admin")]
        // GET: Bars/Create
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Name, Description ")] Bars bar)
        {
            if (bar.BarName.Length > 64)
            {
                ModelState.AddModelError(nameof(bar.BarName), "Name must be max 64 symbols");
            }

            if (ModelState.IsValid)
            {
                _context.Add(bar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bar);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bars == null)
            {
                return NotFound();
            }

            var bar = await _context.Bars.FindAsync(id);
            if (bar == null)
            {
                return NotFound();
            }
            return View(bar);
        }



        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, Description ")] Bars bar)
        {
            if (id != bar.Id)
            {
                return NotFound();
            }

            if (bar.BarName.Length > 64)
            {
                ModelState.AddModelError(nameof(bar.BarName), "Name must be max 64 symbols");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BarExists(bar.Id))
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
            return View(bar);
        }


        [Authorize(Roles = "Admin")]
        // GET: Bars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bars == null)
            {
                return NotFound();
            }

            var bar = await _context.Bars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bar == null)
            {
                return NotFound();
            }

            return View(bar);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bars == null)
            {
                return Problem("Entity set 'AppDbContext.Flights'  is null.");
            }
            var bar = await _context.Bars.FindAsync(id);
            if (bar != null)
            {
               
                _context.Bars.Remove(bar);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool BarExists(int id)
        {
            return (_context.Bars?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
