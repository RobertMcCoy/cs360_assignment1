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
        public int? MatchWinner { get; set; }
        public int SeasonId { get; set; }

        public virtual Seasons Season { get; set; }
        public virtual Students Student1 { get; set; }
        public virtual Students Student2 { get; set; }
    }
}
