using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessClubManagement.Models
{
    public class MatchesRepository
    {
        private readonly ChessClubContext _context;

        public MatchesRepository(ChessClubContext context)
        {
            _context = context;
        }

        public List<Matches> GetMatches()
        {
            return _context.Matches.Include(m => m.Student1).Include(m => m.Student2).OrderBy(m => m.MatchDate).ThenBy(m => m.Student1.StudentFname).ToList();
        }
    }
}
