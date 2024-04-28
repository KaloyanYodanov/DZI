using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using BarRating.Data.Entity;
using BarRating.Data;

namespace Bar_rating.Controllers
{
    [Authorize(Roles = "Admin, Employee")]
    public class BarsController : Controller
    {
        private readonly BarRatingDbContext _context;

        public BarsController(BarRatingDbContext context)
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
                //.Include(f => f.Reservations)
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
        public async Task<IActionResult> Create([Bind("Id, Name, Description ")] Bar bar)
        {
            if (bar.Name.Length > 64)
            {
                ModelState.AddModelError(nameof(bar.Name), "Name must be max 64 symbols");
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
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, Description ")] Bar bar)
        {
            if (id != bar.Id)
            {
                return NotFound();
            }

            if (bar.Name.Length > 64)
            {
                ModelState.AddModelError(nameof(bar.Name), "Name must be max 64 symbols");
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

            var flight = await _context.Bars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
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
