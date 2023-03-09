using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SuggestionPanel.Application.Data;
using SuggestionPanel.Application.Services.SMTP;
using SuggestionPanel.Domain.DTOs;
using SuggestionPanel.Domain.Models;

namespace SuggestionPanel.UI.Controllers
{
    public class SuggestionController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly ISMTPService _smtpService;

        public SuggestionController(ApplicationContext context, ISMTPService smtpService)
        {
            _context = context;
            _smtpService = smtpService;
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

        [Route("Suggestions/Accepted")]
        [Authorize(Roles = "Admin,Committee")]
        public async Task<IActionResult> Accepted()
        {
            var suggestions = _context.Suggestions.Include(s => s.Cost).Include(s => s.SignedTo).Include(s => s.SubmissionOwner).Where(x => x.Accepted == true);
            return View(await suggestions.ToListAsync());
        }

        [Route("Suggestions/Archive")]
        [Authorize(Roles = "Admin,Committee")]
        public async Task<IActionResult> Archive()
        {
            var suggestions = _context.Suggestions.Include(s => s.Cost).Include(s => s.SignedTo).Include(s => s.SubmissionOwner).Where(x => x.Archive == true);
            return View(await suggestions.ToListAsync());
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
                    Delete = false,
                    ToCommittee = false
                });

                _smtpService.SendEmail(new MailRequestDto
                {
                    ToAddress = signedTo.Email,
                    Subject = "You have got new suggestion",
                    Body = @$"
                                <p align-center>Problem: {suggestion.Problem} </p>
                                <hr />
                                <p align-center>Solution: {suggestion.Solution} </p>
                             "
                });

                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            ViewData["CostId"] = new SelectList(_context.Costs, "Id", "Value", suggestion.CostId);
            return RedirectToAction("Index", "Home");
        }

        // GET: Suggestion/Edit/5
        [Authorize()]
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
                Solution = suggestion.Solution,
                ImplementationDesc = suggestion.ImplementationDesc
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
                    suggestionModel.ToCommittee = true;

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
                return RedirectToAction(nameof(Index), new { Number = HttpContext.User.Identity.Name });
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
