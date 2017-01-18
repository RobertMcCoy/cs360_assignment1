using System.Linq;
using System.Security.Claims;
using ChessClubManagement.Models;
using ChessClubManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChessClubManagement.Controllers
{
    [Authorize]
    public class MatchesController : Controller
    {
        private readonly MatchesRepository _repository;

        public MatchesController(ChessClubContext context)
        {
            _repository = new MatchesRepository(context);
        }

        [HttpGet]
        public IActionResult Index(int id = 0, string date = "")
        {
            ViewBag.id = id;
            ViewBag.date = date;
            ViewBag.SeasonIdList = _repository.GetSeasonIds();
            ViewBag.ListOfMatchDates = _repository.GetListOfMatchDatesPerSeason();
            if (User.Claims != null)
            {
                return View(new MatchesViewModel()
                {
                    Matches = _repository.GetMatches(),
                    UserRole =
                        _repository.GetUserRole(
                            User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value.Substring(6))
                });
            }
            return View(new MatchesViewModel()
            {
                Matches = _repository.GetMatches(),
                UserRole = 0
            });
        }

        [HttpGet]
        public IActionResult Match(int id)
        {
            return View(_repository.GetMatchById(id));
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            if (User.Claims != null)
            {
                var userRole =
                    _repository.GetUserRole(
                        User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value.Substring(6));
                if (userRole > 0) return View(_repository.GetMatchById(id));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Save(Matches match)
        {
            TempData["Result"] = _repository.UpdateMatch(match);
            return RedirectToAction("Edit", new { id = match.MatchId });
        }
    }
}