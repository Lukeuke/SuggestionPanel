﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SuggestionPanel.Application.Data;
using SuggestionPanel.Domain.DTOs;
using SuggestionPanel.Domain.Models;

namespace SuggestionPanel.UI.Controllers
{
    public class SuggestionController : Controller
    {
        private readonly ApplicationContext _context;

        public SuggestionController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Suggestion
        public async Task<IActionResult> Index(string? number)
        {
            var isAdmin = HttpContext.User.IsInRole("Admin");

            if (number is null && !isAdmin)
            {
                return Unauthorized();
            }

            if(number != HttpContext.User.Identity.Name)
                return Unauthorized();

            if (isAdmin)
            {
                var suggestions = _context.Suggestions.Include(s => s.Cost).Include(s => s.SignedTo).Include(s => s.SubmissionOwner);
                return View(await suggestions.ToListAsync());
            }

            var applicationContext = _context.Suggestions
                .Include(s => s.Cost)
                .Include(s => s.SignedTo)
                .Include(s => s.SubmissionOwner)
                .Where(x => x.SignedTo.Number == number);

            applicationContext = applicationContext.Where(x => x.Delete == false);

            applicationContext = applicationContext.Where(x => x.ImplementationDate == null);

            return View(await applicationContext.ToListAsync());
        }

        // GET: Suggestion/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Suggestions == null)
            {
                return NotFound();
            }

            var suggestion = await _context.Suggestions
                .Include(s => s.Cost)
                .Include(s => s.SignedTo)
                .Include(s => s.SubmissionOwner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suggestion == null)
            {
                return NotFound();
            }

            return View(suggestion);
        }

        // GET: Suggestion/Create
        public IActionResult Create()
        {
            ViewData["CostId"] = new SelectList(_context.Costs, "Id", "Value");
            ViewData["ValueStreams"] = new SelectList(_context.ValueStreams, "Id", "AreaName");
            return View();
        }

        // POST: Suggestion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SuggestionRequest suggestion)
        {
            if (ModelState.IsValid)
            {
                suggestion.Cost = _context.Costs.First(x => x.Id == suggestion.CostId);
                var signedTo = _context.ValueStreamResponsibilities.First(x => x.ValueStreamId == suggestion.ValueStreamId);

                _context.Add(new Suggestion
                {
                    StationNumber = suggestion.StationNumber,
                    Cost = suggestion.Cost,
                    SignedTo = signedTo,
                    DateOfSubmission = DateTime.Now,
                    Problem = suggestion.Problem,
                    Solution = suggestion.Solution,
                    IsCardAnomaly = suggestion.IsCardAnomaly,
                    SubmissionOwnerId = 1,
                    Delete = false
                });
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CostId"] = new SelectList(_context.Costs, "Id", "Value", suggestion.CostId);
            return RedirectToAction(nameof(Index));
        }

        // GET: Suggestion/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Suggestions == null)
            {
                return NotFound();
            }

            var suggestion = await _context.Suggestions.FindAsync(id);


            if (suggestion == null)
            {
                return NotFound();
            }

            var suggestionReuqest = new SuggestionReviewRequest
            {
                Id = suggestion.Id,
                StationNumber = suggestion.StationNumber,
                IsCardAnomaly = suggestion.IsCardAnomaly,
                DateOfSubmission = suggestion.DateOfSubmission,
                Problem = suggestion.Problem,
                Solution = suggestion.Solution
            };

            return View(suggestionReuqest);
        }

        // POST: Suggestion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SuggestionReviewRequest suggestion)
        {
            if (id != suggestion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var suggestionModel = await _context.Suggestions.FirstOrDefaultAsync(x => x.Id == suggestion.Id);

                    suggestionModel!.IsCardAnomaly = suggestion.IsCardAnomaly;
                    suggestionModel.ImplementationDesc = suggestion.ImplementationDesc;
                    suggestionModel.PropositionDate = suggestion.PropositionDate;
                    suggestionModel.ImplementationDate = suggestion.ImplementationDate;
                    suggestionModel.Delete = suggestion.Delete;

                    _context.Update(suggestionModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SuggestionExists(suggestion.Id))
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
            return RedirectToAction(nameof(Index), new { Number = HttpContext.User.Identity.Name });
        }

        // GET: Suggestion/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Suggestions == null)
            {
                return NotFound();
            }

            var suggestion = await _context.Suggestions
                .Include(s => s.Cost)
                .Include(s => s.SignedTo)
                .Include(s => s.SubmissionOwner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suggestion == null)
            {
                return NotFound();
            }

            return View(suggestion);
        }

        // POST: Suggestion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Suggestions == null)
            {
                return Problem("Entity set 'ApplicationContext.Suggestions'  is null.");
            }
            var suggestion = await _context.Suggestions.FindAsync(id);
            if (suggestion != null)
            {
                _context.Suggestions.Remove(suggestion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SuggestionExists(int id)
        {
          return (_context.Suggestions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
