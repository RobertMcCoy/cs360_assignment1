using ChessClubManagement.Models;
using ChessClubManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ChessClubManagement.Controllers
{
    public class StudentsController : Controller
    {
        private readonly StudentRepository _repository;
        public StudentsController(ChessClubContext context)
        {
            _repository = new StudentRepository(context);
        }

        public IActionResult Index()
        {
            return View(_repository.GetStudents());
        }

        public IActionResult Edit(int id)
        {
            return View(_repository.GetStudentEditVmById(id));
        }

        public IActionResult Update(StudentEditViewModel viewModel)
        {
            TempData["SaveSuccess"] = _repository.SaveStudent(viewModel) > 0 ? "Success" : "Fail";
            return RedirectToAction("Edit", new { id = viewModel.Student.StudentId });
        }
    }
}