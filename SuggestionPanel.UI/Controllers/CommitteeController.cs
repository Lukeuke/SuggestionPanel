using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SuggestionPanel.Application.Data;
using SuggestionPanel.Domain.DTOs;

namespace SuggestionPanel.UI.Controllers
{
    //[Authorize(Roles = "Committee")]
    public class CommitteeController : Controller
    {
        private readonly ApplicationContext _context;

        public CommitteeController(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var suggestions = _context.Suggestions.Include(x => x.SubmissionOwner).Include(x => x.Cost).Include(x => x.SignedTo);

            suggestions.Where(x => x.Delete == true || x.ImplementationDate != null);
            return View(suggestions.Where(x => x.ToCommittee == true && x.ReviewDate == null).ToList());
        }

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

            var suggestionReuqest = new SuggestionCommitteeReviewRequest
            {
                Id = suggestion.Id,
                StationNumber = suggestion.StationNumber,
                IsCardAnomaly = suggestion.IsCardAnomaly,
                DateOfSubmission = suggestion.DateOfSubmission,
                Problem = suggestion.Problem,
                Solution = suggestion.Solution,
                ImplementationDesc = suggestion.ImplementationDesc,
                PropositionDate = suggestion.PropositionDate,
                ImplementationDate = suggestion.ImplementationDate,
                Delete = suggestion.Delete,
                Money = suggestion.Money,
                Points = suggestion.Points
            };

            ViewData["Points"] = new SelectList(new List<int>
            {
                2, 5, 10, 15, 20, 25, 30, 35, 40, 50
            });

            return View(suggestionReuqest);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SuggestionCommitteeReviewRequest suggestion)
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

                    suggestionModel!.Money = suggestion.Money;
                    suggestionModel.Points = suggestion.Points;
                    suggestionModel.ReviewDate = DateTime.Now;

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

        private bool SuggestionExists(int id)
        {
            return (_context.Suggestions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
