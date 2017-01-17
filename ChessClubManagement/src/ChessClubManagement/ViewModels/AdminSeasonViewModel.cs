using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessClubManagement.Models;

namespace ChessClubManagement.ViewModels
{
    public class AdminSeasonViewModel
    {
        public string NewSeasonName { get; set; }
        public List<Seasons> Seasons { get; set; }
    }
}
