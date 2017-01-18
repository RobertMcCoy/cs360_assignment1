using System.Collections.Generic;
using ChessClubManagement.Models;

namespace ChessClubManagement.ViewModels
{
    public class StandingsViewModel
    {
        public List<StudentRankings> Rankings { get; set; } 
        public int? DivisionId { get; set; }
    }
}