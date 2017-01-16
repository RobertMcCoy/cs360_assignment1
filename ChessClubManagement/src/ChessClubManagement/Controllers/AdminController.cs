using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessClubManagement.Models;
using ChessClubManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ChessClubManagement.Controllers
{
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

        [HttpPost]
        public ActionResult GetDivisionsBySeason(int id)
        {
            return Json(_repository.GetDivisionsBySeason(id));
        }

        public IActionResult GenerateSchedules(GenerateMatchesViewModel viewModel)
        {
            TempData["Results"] = _repository.GenerateSchedules(viewModel);
            if (TempData["Results"].Equals("Season Created Successfully")) return RedirectToAction("Index", "Matches", new {id = viewModel.SeasonId, date = viewModel.StartingDate.Value.ToString("MM-dd-yyyy")});
            return RedirectToAction("ScheduleMatches");
        }
    }
}
