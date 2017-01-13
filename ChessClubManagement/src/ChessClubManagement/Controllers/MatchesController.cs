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

        public IActionResult Index(int id = 0, string date = "")
        {
            ViewBag.id = id;
            ViewBag.date = date;
            ViewBag.SeasonIdList = _repository.GetSeasonIds();
            ViewBag.ListOfMatchDates = _repository.GetListOfMatchDatesPerSeason();
            return View(_repository.GetMatches());
        }
    }
}