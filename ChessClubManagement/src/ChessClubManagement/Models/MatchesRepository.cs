using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessClubManagement.Models
{
    public class MatchesRepository
    {
        private readonly ChessClubContext _context;

        public MatchesRepository(ChessClubContext context)
        {
            _context = context;
        }

        public List<Matches> GetMatches()
        {
            return _context.Matches.Include(m => m.Student1).ThenInclude(s => s.User).Include(m => m.Student2).ThenInclude(s => s.User).OrderBy(m => m.MatchDate).ThenBy(m => m.Student1.User.Nickname).ToList();
        }

        public List<int> GetSeasonIds()
        {
            return _context.Seasons.Select(s => s.SeasonId).ToList();
        }

        public List<Tuple<int, List<DateTime>>> GetListOfMatchDatesPerSeason()
        {
            var seasonIds = GetSeasonIds();
            List<Tuple<int, List<DateTime>>> listOfStringsOfMatches = new List<Tuple<int, List<DateTime>>>();
            foreach (var season in seasonIds)
            {
                foreach (var dateTime in _context.Matches.Where(m => m.SeasonId == season)
                    .Select(m => m.MatchDate.Value.Date).Distinct())
                    listOfStringsOfMatches.Add(new Tuple<int, List<DateTime>>(season,
                        new List<DateTime>()
                        {
                            dateTime
                        }));
            }
            return listOfStringsOfMatches;
        }

        public Matches GetMatchById(int id)
        {
            return _context.Matches.Single(m => m.MatchId == id);
        }

        public void UpdateMatch(Matches match)
        {
            _context.Matches.Update(match);
            _context.SaveChanges();
        }
    }
}
