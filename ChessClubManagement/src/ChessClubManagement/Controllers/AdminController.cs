using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessClubManagement.Models;
using ChessClubManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ChessClubManagement.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly AdminRepository _repository;

        public AdminController(ChessClubContext context)
        {
            _repository = new AdminRepository(context);
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ScheduleMatches()
        {
            ViewBag.SeasonDropDown = _repository.GetSeasonDropDown();
            return View();
        }

        [HttpGet]
        public IActionResult CreateMatch()
        {
            ViewBag.SeasonDropDown = _repository.GetSeasonDropDown();
            return View();
        }

        [HttpPost]
        public IActionResult CreateMatch(Matches newMatch)
        {
            Tuple<string, int> createResponse = _repository.CreateMatch(newMatch);
            TempData["Result"] = createResponse.Item1;
            if (TempData["Result"].Equals("Match Created Successfully"))
            {
                return RedirectToAction("Match", "Matches", new { id = createResponse.Item2 });
            }
            else
            {
                return View(newMatch);
            }
        }

        [HttpPost]
        public ActionResult GetDivisionsBySeason(int id)
        {
            return Json(_repository.GetDivisionsBySeason(id));
        }

        [HttpPost]
        public IActionResult GetStudentsInDivision(int divisionId)
        {
            return Json(_repository.GetStudentsInDivision(divisionId));
        }

        [HttpPost]
        public IActionResult GetStudentsInSeason(int seasonId)
        {
            return Json(_repository.GetStudentsInSeason(seasonId));
        }

        public IActionResult GenerateSchedules(GenerateMatchesViewModel viewModel)
        {
            TempData["Results"] = _repository.GenerateSchedules(viewModel);
            if (TempData["Results"].Equals("Season Created Successfully")) return RedirectToAction("Index", "Matches", new {id = viewModel.SeasonId, date = viewModel.StartingDate.Value.ToString("MM-dd-yyyy")});
            return RedirectToAction("ScheduleMatches");
        }

        public IActionResult AddNewSeason(AdminSeasonViewModel viewModel)
        {
            int newSeasonId = _repository.AddNewSeason(viewModel);
            return RedirectToAction("EditSeason", new { id = newSeasonId });
        }

        public IActionResult Seasons()
        {
            return View(_repository.GetAdminSeasonViewModel());
        }

        public IActionResult EditSeason(int id)
        {
            return View(_repository.GetAdminSeasonEditViewModel(id));
        }

        public IActionResult AddNewDivision(AdminSeasonEditViewModel viewModel)
        {
            _repository.AddNewDivision(viewModel);
            return RedirectToAction("EditSeason", new { id = viewModel.CurrentSeasonId });
        }

        public IActionResult DeleteDivision(int id, int seasonId)
        {
            _repository.DeleteDivision(id);
            return RedirectToAction("EditSeason", new { id = seasonId });
        }
    }
}
