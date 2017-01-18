using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessClubManagement.Controllers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ChessClubManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
            return _context.Matches.Include(m => m.Student1).ThenInclude(s => s.User).Include(m => m.Student2).ThenInclude(s => s.User).Include(s => s.Season).Single(m => m.MatchId == id);
        }

        public string UpdateMatch(Matches match)
        {
            if (match.Student1Score == null || match.Student2Score == null ||
                (match.Student1Score == 1 && !match.Student1Result.Equals("W")) ||
                (match.Student2Score == 1 && !match.Student2Result.Equals("W")) ||
                (match.Student1Score == 0.5m && !match.Student1Result.Equals("D")) ||
                (match.Student2Score == 0.5m && !match.Student2Result.Equals("D")) ||
                (match.Student1Score == 0 && !match.Student1Result.Equals("L")) ||
                (match.Student2Score == 0 && !match.Student2Result.Equals("L")) ||
                (match.Student1Score == 1 && !match.Student1Result.Equals("X")) ||
                (match.Student2Score == 1 && !match.Student2Result.Equals("X")))
            {
                switch (match.Student1Result)
                {
                    case "W":
                        match.Student1Score = 1;
                        break;
                    case "L":
                        match.Student1Score = 0;
                        break;
                    case "D":
                        match.Student1Score = 0.5m;
                        break;
                    case "X":
                        match.Student1Score = 0;
                        break;
                    case null:
                        match.Student1Score = null;
                        break;
                }
                switch (match.Student2Result)
                {
                    case "W":
                        match.Student2Score = 1;
                        break;
                    case "L":
                        match.Student2Score = 0;
                        break;
                    case "D":
                        match.Student2Score = 0.5m;
                        break;
                    case "X":
                        match.Student2Score = 0;
                        break;
                    case null:
                        match.Student2Score = null;
                        break;
                }
            }
            if (match.Student1Score == null && match.Student2Score == null)
                return "Please select the results for both players";
            if (match.Student1Color != string.Empty && match.Student2Color != string.Empty && match.Student1Color.Equals(match.Student2Color)) return "Both players cannot be the same color";
            if (match.Student1Score == 1 && match.Student2Score == 1) return "Both players can not be winners";
            if ((match.Student1Score == null && match.Student2Score != null) ||
                (match.Student2Score == null && match.Student1Score != null)) return "Both players require a score.";
            if ((match.Student1Score == 0.5m && match.Student2Score != 0.5m) ||
                (match.Student2Score == 0.5m && match.Student1Score != 0.5m)) return "Both players require a score.";
            if (match.TotalMoves == null) return "Please enter the total moves for the game.";
            _context.Matches.Update(match);
            var success = _context.SaveChanges();
            if (success > 0) return "Match Updated Successfully";
            return "Error updating match";
        }

        public UserRoleModel GetMatchEditViewModel(int id)
        {
            if (ClaimsPrincipal.Current != null)
            {
                if (ClaimsPrincipal.Current.Identity.IsAuthenticated)
                {
                    if (ClaimsPrincipal.Current.Claims.FirstOrDefault(
                                c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                            .Value.Substring(6) != null)
                    {
                        return new UserRoleModel()
                        {
                            UserRole =
                                _context.Users.Single(u => u.Id.ToString() ==
                                                           ClaimsPrincipal.Current.Claims.FirstOrDefault(
                                                                   c =>
                                                                       c.Type ==
                                                                       "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                                                               .Value.Substring(6)).UserRole.Value
                        };
                    }
                }
            }
            return null;
        }

        public int GetUserRole(string substring)
        {
            if (_context.Users.Any(s => s.Id.ToString() == substring))
            {
                if (_context.Users.Single(s => s.Id.ToString() == substring).UserRole.HasValue)
                {
                    var temp = _context.Users.Single(s => s.Id.ToString().Equals(substring));
                    return _context.Users.Single(s => s.Id.ToString().Equals(substring)).UserRole.Value;
                }
            }
            return 0;
        }
    }
}
