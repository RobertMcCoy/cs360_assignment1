using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
