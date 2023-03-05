using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuggestionPanel.Application.Data;
using SuggestionPanel.Domain.Models;

namespace SuggestionPanel.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    public class HumanResourcesController : Controller
    {
        private readonly ApplicationContext _context;

        public HumanResourcesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: HumanResources
        public async Task<IActionResult> Index()
        {
              return _context.HumanResources != null ? 
                          View(await _context.HumanResources.ToListAsync()) :
                          Problem("Entity set 'ApplicationContext.HumanResources'  is null.");
        }

        // GET: HumanResources/Details/5
        [Route("Details/{id?}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.HumanResources == null)
            {
                return NotFound();
            }

            var humanResources = await _context.HumanResources
                .FirstOrDefaultAsync(m => m.Id == id);
            if (humanResources == null)
            {
                return NotFound();
            }

            return View(humanResources);
        }

        // GET: HumanResources/Create
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: HumanResources/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,CardNumber")] HumanResources humanResources)
        {
            if (ModelState.IsValid)
            {
                _context.Add(humanResources);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(humanResources);
        }

        // GET: HumanResources/Edit/5
        [Route("Edit/{id?}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.HumanResources == null)
            {
                return NotFound();
            }

            var humanResources = await _context.HumanResources.FindAsync(id);
            if (humanResources == null)
            {
                return NotFound();
            }
            return View(humanResources);
        }

        // POST: HumanResources/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Edit/{id?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,CardNumber")] HumanResources humanResources)
        {
            if (id != humanResources.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(humanResources);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HumanResourcesExists(humanResources.Id))
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
            return View(humanResources);
        }

        // GET: HumanResources/Delete/5
        [Route("Delete/{id?}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.HumanResources == null)
            {
                return NotFound();
            }

            var humanResources = await _context.HumanResources
                .FirstOrDefaultAsync(m => m.Id == id);
            if (humanResources == null)
            {
                return NotFound();
            }

            return View(humanResources);
        }

        // POST: HumanResources/Delete/5
        [Route("Delete/{id?}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.HumanResources == null)
            {
                return Problem("Entity set 'ApplicationContext.HumanResources'  is null.");
            }
            var humanResources = await _context.HumanResources.FindAsync(id);
            if (humanResources != null)
            {
                _context.HumanResources.Remove(humanResources);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HumanResourcesExists(int id)
        {
          return (_context.HumanResources?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
