using System;
using System.Collections.Generic;

namespace ChessClubManagement.Models
{
    public partial class Seasons
    {
        public Seasons()
        {
            Divisions = new HashSet<Divisions>();
            Matches = new HashSet<Matches>();
        }

        public int SeasonId { get; set; }
        public string SeasonName { get; set; }
        public DateTime? Wk1 { get; set; }
        public DateTime? Wk2 { get; set; }
        public DateTime? Wk3 { get; set; }
        public DateTime? Wk4 { get; set; }
        public DateTime? Wk5 { get; set; }
        public DateTime? Wk6 { get; set; }
        public DateTime? Wk7 { get; set; }
        public DateTime? Wk8 { get; set; }
        public DateTime? Wk9 { get; set; }
        public DateTime? Wk10 { get; set; }
        public DateTime? Wk11 { get; set; }
        public DateTime? Wk12 { get; set; }
        public DateTime? Wk13 { get; set; }
        public DateTime? Wk14 { get; set; }
        public DateTime? Wk15 { get; set; }

        public virtual ICollection<Divisions> Divisions { get; set; }
        public virtual ICollection<Matches> Matches { get; set; }
    }
}
