using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessClubManagement.Models;

namespace ChessClubManagement.ViewModels
{
    public class StudentEditViewModel
    {
        public Students Student { get; set; }
        public List<Matches> MatchHistory { get; set; }
    }
}
