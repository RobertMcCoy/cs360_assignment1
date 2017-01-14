using System;
using System.Collections.Generic;

namespace ChessClubManagement.Models
{
    public partial class Students
    {
        public Students()
        {
            MatchesStudent1 = new HashSet<Matches>();
            MatchesStudent2 = new HashSet<Matches>();
        }

        public int StudentId { get; set; }
        public int? DivisionId { get; set; }
        public int? UserId { get; set; }

        public virtual ICollection<Matches> MatchesStudent1 { get; set; }
        public virtual ICollection<Matches> MatchesStudent2 { get; set; }
        public virtual Divisions Division { get; set; }
        public virtual Users User { get; set; }
    }
}
