using System.Collections.Generic;

namespace ChessClubManagement.Models
{
    public class AdminSeasonEditViewModel
    {
        public int CurrentSeasonId { get; set; }
        public Divisions NewDivision { get; set; }
        public List<Divisions> SeasonDivisions { get; set; }
    }
}