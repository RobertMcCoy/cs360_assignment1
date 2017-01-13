using ChessClubManagement.Models;
using ChessClubManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ChessClubManagement.Controllers
{
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
            return View(_repository.GetMatches());
        }

        [HttpGet]
        public IActionResult Match(int id)
        {
            return View(_repository.GetMatchById(id));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(_repository.GetMatchById(id));
        }

        [HttpGet]
        public IActionResult Save(Matches match)
        {
            _repository.UpdateMatch(match);
            return RedirectToAction("Match", new { id = match.MatchId });
        }
    }
}