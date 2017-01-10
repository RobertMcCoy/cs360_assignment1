using ChessClubManagement.Models;
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
    }
}