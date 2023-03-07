using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuggestionPanel.Application.Data;
using SuggestionPanel.Domain.Enums;
using SuggestionPanel.UI.Models;
using System.Diagnostics;

namespace SuggestionPanel.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.User.IsInRole(ERoles.ValueStreamResponsibility.ToString()))
            {
                ViewData["SuggestionsCount"] = _context.Suggestions
                    .Include(x => x.SignedTo)
                    .Where(x => x.SignedTo.Number == HttpContext.User.Identity!.Name! && x.ToCommittee == false)
                    .Count();
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}