using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessClubManagement.Models;

namespace ChessClubManagement.ViewModels
{
    public class StudentEditViewModel
    {
        public Users User { get; set; }
        public List<Students> StudentUsers { get; set; }
        public List<Matches> MatchHistory { get; set; }
        public Students NewUserStudent { get; set; }
        public int NewDivisionSeasonId { get; set; }
        public int UserRole { get; set; }
    }
}
