using System.Collections.Generic;
using ChessClubManagement.Models;

namespace ChessClubManagement.ViewModels
{
    public class MatchesViewModel
    {
        public List<Matches> Matches { get; set; }
        public int UserRole { get; set; }
    }
}