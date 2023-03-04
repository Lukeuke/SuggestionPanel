using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SuggestionPanel.Domain.Models;
using SuggestionPanel.UI.Data;

namespace SuggestionPanel.UI.Controllers
{
    [Route("[controller]")]
    public class ValueStreamController : Controller
    {
        private readonly ApplicationContext _context;

        public ValueStreamController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: ValueStream
        public async Task<IActionResult> Index()
        {
              return _context.ValueStreams != null ? 
                          View(await _context.ValueStreams.ToListAsync()) :
                          Problem("Entity set 'ApplicationContext.ValueStreams'  is null.");
        }

        // GET: ValueStream/Details/5
        [Route("Details/{id?}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ValueStreams == null)
            {
                return NotFound();
            }

            var valueStream = await _context.ValueStreams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (valueStream == null)
            {
                return NotFound();
            }

            return View(valueStream);
        }

        // GET: ValueStream/Create
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: ValueStream/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,AreaName")] ValueStream valueStream)
        {
            if (ModelState.IsValid)
            {
                _context.Add(valueStream);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(valueStream);
        }

        // GET: ValueStream/Edit/5
        [Route("Edit/{id?}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ValueStreams == null)
            {
                return NotFound();
            }

            var valueStream = await _context.ValueStreams.FindAsync(id);
            if (valueStream == null)
            {
                return NotFound();
            }
            return View(valueStream);
        }

        // POST: ValueStream/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Edit/{id?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,AreaName")] ValueStream valueStream)
        {
            if (id != valueStream.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(valueStream);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ValueStreamExists(valueStream.Id))
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
            return View(valueStream);
        }

        // GET: ValueStream/Delete/5
        [Route("Delete/{id?}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ValueStreams == null)
            {
                return NotFound();
            }

            var valueStream = await _context.ValueStreams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (valueStream == null)
            {
                return NotFound();
            }

            return View(valueStream);
        }

        // POST: ValueStream/Delete/5
        [Route("Delete/{id?}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ValueStreams == null)
            {
                return Problem("Entity set 'ApplicationContext.ValueStreams'  is null.");
            }
            var valueStream = await _context.ValueStreams.FindAsync(id);
            if (valueStream != null)
            {
                _context.ValueStreams.Remove(valueStream);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ValueStreamExists(int id)
        {
          return (_context.ValueStreams?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
