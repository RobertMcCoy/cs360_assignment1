using System;
using System.Collections.Generic;

namespace ChessClubManagement.Models
{
    public partial class Matches
    {
        public int MatchId { get; set; }
        public int Student1Id { get; set; }
        public int Student2Id { get; set; }
        public DateTime? MatchDate { get; set; }
        public int SeasonId { get; set; }
        public decimal? Student1Score { get; set; }
        public decimal? Student2Score { get; set; }
        public string Student1Result { get; set; }
        public string Student2Result { get; set; }
        public int? TotalMoves { get; set; }
        public string Student1Color { get; set; }
        public string Student2Color { get; set; }

        public virtual Seasons Season { get; set; }
        public virtual Students Student1 { get; set; }
        public virtual Students Student2 { get; set; }
    }
}
