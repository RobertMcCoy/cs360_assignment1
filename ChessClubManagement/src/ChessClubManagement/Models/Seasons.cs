using System;
using System.Collections.Generic;

namespace ChessClubManagement.Models
{
    public partial class Seasons
    {
        public Seasons()
        {
            Matches = new HashSet<Matches>();
        }

        public int SeasonId { get; set; }
        public string SeasonName { get; set; }

        public virtual ICollection<Matches> Matches { get; set; }
    }
}
