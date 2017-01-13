using System;
using System.Collections.Generic;

namespace ChessClubManagement.Models
{
    public partial class Divisions
    {
        public Divisions()
        {
            Students = new HashSet<Students>();
        }

        public int DivisionId { get; set; }
        public string DivisionName { get; set; }
        public int SeasonId { get; set; }

        public virtual ICollection<Students> Students { get; set; }
        public virtual Seasons Season { get; set; }
    }
}
