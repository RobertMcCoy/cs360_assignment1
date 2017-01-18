using System;
using System.Globalization;
using ChessClubManagement.Models;
using ChessClubManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChessClubManagement.Controllers
{
    public class MembersController : Controller
    {
        private readonly StudentRepository _repository;
        public MembersController(ChessClubContext context)
        {
            _repository = new StudentRepository(context);
        }

        public IActionResult Standings(StandingsViewModel viewModel)
        {
            ViewBag.SeasonDropDown = _repository.GetSeasonDropDown();
            return View(new StandingsViewModel() { Rankings = _repository.GetStandings(viewModel.DivisionId), DivisionId = viewModel.DivisionId});
        }

        public IActionResult Index()
        {
            return View(_repository.GetStudents());
        }

        [Authorize(Roles = "admin")]
        public IActionResult Edit(int id)
        {
            ViewBag.SeasonDropDown = _repository.GetSeasonDropDown();
            return View(_repository.GetStudentEditVmById(id));
        }

        [HttpPost]
        public ActionResult GetDivisionsBySeason(int id)
        {
            return Json(_repository.GetDivisionsBySeason(id));
        }

        [Authorize(Roles = "admin")]
        public IActionResult Update(StudentEditViewModel viewModel)
        {
            TempData["SaveSuccess"] = _repository.SaveStudent(viewModel) > 0 ? "Success" : "Fail";
            return RedirectToAction("Edit", new { id = viewModel.User.Id });
        }

        public IActionResult Member(int id)
        {
            return View(_repository.GetStudentById(id));
        }

        public IActionResult AddToDivision(StudentEditViewModel viewModel)
        {
            TempData["Result"] = _repository.AddStudentToDivision(viewModel);
            return RedirectToAction("Edit", new { id = viewModel.User.Id });
        }

        public IActionResult RemoveDivisionFromUser(int id, StudentEditViewModel viewModel)
        {
            TempData["Result"] = _repository.RemoveDivisionFromStudent(id);
            return RedirectToAction("Edit", viewModel.User.Id);
        }
    }
}