using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessClubManagement.Models
{
    public class StudentRankings
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string DivisionName { get; set; }
        public decimal Total { get; set; }
        public int UserId { get; set; }
    }
}
