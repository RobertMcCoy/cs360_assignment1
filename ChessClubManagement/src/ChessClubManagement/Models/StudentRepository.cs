using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ChessClubManagement.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChessClubManagement.Models
{
    public class StudentRepository
    {
        private readonly ChessClubContext _context;

        public StudentRepository(ChessClubContext context)
        {
            _context = context;
        }

        public List<Students> GetStudents()
        {
            return _context.Students.ToList();
        }

        public StudentEditViewModel GetStudentEditVmById(int id)
        {
            StudentEditViewModel studentEditViewModel = new StudentEditViewModel()
            {
                Student = _context.Students.SingleOrDefault(s => s.StudentId == id),
                MatchHistory = _context.Matches.Where(s => s.Student1Id == id || s.Student2Id == id).Include(s => s.Student1).Include(s => s.Student2).ToList()
            };
            return studentEditViewModel;
        }

        public int SaveStudent(StudentEditViewModel viewModel)
        {
            _context.Students.Update(viewModel.Student);
            var success = _context.SaveChanges();
            return success;
        }
    }
}
