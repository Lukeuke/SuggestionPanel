using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SuggestionPanel.Domain.DTOs;
using SuggestionPanel.Domain.Models;
using SuggestionPanel.UI.Data;

namespace SuggestionPanel.UI.Controllers
{
    [Route("[controller]")]
    public class ResponsibilityController : Controller
    {
        private readonly ApplicationContext _context;

        public ResponsibilityController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: ResponsibilityController
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.ValueStreamResponsibilities.Include(v => v.ValueStream);
            return View(await applicationContext.ToListAsync());
        }

        // GET: ResponsibilityController/Details/5
        [Route("Details/{id?}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ValueStreamResponsibilities == null)
            {
                return NotFound();
            }

            var valueStreamResponsibility = await _context.ValueStreamResponsibilities
                .Include(v => v.ValueStream)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (valueStreamResponsibility == null)
            {
                return NotFound();
            }

            return View(valueStreamResponsibility);
        }

        // GET: ResponsibilityController/Create
        [Route("Create")]
        public IActionResult Create()
        {
            ViewData["ValueStreamId"] = new SelectList(_context.ValueStreams, "Id", "AreaName");
            return View();
        }

        // POST: ResponsibilityController/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,PasswordHash,Salt,ValueStreamId")] ValueStreamResponsibilityRequest request)
        {
            if (ModelState.IsValid)
            {
                _context.Add(new ValueStreamResponsibility
                {
                    Name = request.Name,
                    Surname = request.Surname,
                    PasswordHash = request.PasswordHash,
                    Salt = string.Empty,
                    ValueStreamId = request.ValueStreamId
                });
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ValueStreamId"] = new SelectList(_context.ValueStreams, "Id", "AreaName", request.ValueStreamId);
            return View(request);
        }

        // GET: ResponsibilityController/Edit/5
        [Route("Edit/{id?}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ValueStreamResponsibilities == null)
            {
                return NotFound();
            }

            var valueStreamResponsibility = await _context.ValueStreamResponsibilities.FindAsync(id);
            if (valueStreamResponsibility == null)
            {
                return NotFound();
            }
            ViewData["ValueStreamId"] = new SelectList(_context.ValueStreams, "Id", "AreaName", valueStreamResponsibility.ValueStreamId);
            return View(valueStreamResponsibility);
        }

        // POST: ResponsibilityController/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,PasswordHash,Salt,ValueStreamId")] ValueStreamResponsibility valueStreamResponsibility)
        {
            if (id != valueStreamResponsibility.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(valueStreamResponsibility);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ValueStreamResponsibilityExists(valueStreamResponsibility.Id))
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
            ViewData["ValueStreamId"] = new SelectList(_context.ValueStreams, "Id", "AreaName", valueStreamResponsibility.ValueStreamId);
            return View(valueStreamResponsibility);
        }

        // GET: ResponsibilityController/Delete/5
        [Route("Delete/{id?}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ValueStreamResponsibilities == null)
            {
                return NotFound();
            }

            var valueStreamResponsibility = await _context.ValueStreamResponsibilities
                .Include(v => v.ValueStream)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (valueStreamResponsibility == null)
            {
                return NotFound();
            }

            return View(valueStreamResponsibility);
        }

        // POST: ResponsibilityController/Delete/5
        [HttpPost("Delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ValueStreamResponsibilities == null)
            {
                return Problem("Entity set 'ApplicationContext.ValueStreamResponsibilities'  is null.");
            }
            var valueStreamResponsibility = await _context.ValueStreamResponsibilities.FindAsync(id);
            if (valueStreamResponsibility != null)
            {
                _context.ValueStreamResponsibilities.Remove(valueStreamResponsibility);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ValueStreamResponsibilityExists(int id)
        {
          return (_context.ValueStreamResponsibilities?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
