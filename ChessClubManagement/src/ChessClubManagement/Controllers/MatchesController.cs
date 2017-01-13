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

        public IActionResult Index()
        {
            return View(_repository.GetMatches());
        }
    }
}