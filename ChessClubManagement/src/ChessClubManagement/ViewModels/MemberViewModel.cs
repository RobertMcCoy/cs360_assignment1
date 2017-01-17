using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessClubManagement.Models;

namespace ChessClubManagement.ViewModels
{
    public class MemberViewModel
    {
        public Users Member { get; set; }
        public List<Matches> Matches { get; set; }
        public List<MemberDivision> Divisions { get; set; }
    }

    public class MemberDivision
    {
        public Divisions Division { get; set; }
        public Tuple<decimal, int, int, int, int> PlayerStats { get; set; }
    }
}
