using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using ChessClubManagement.Models;
using ChessClubManagement.ViewModels;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ChessClubManagement.Models
{
    internal class AdminRepository
    {
        private ChessClubContext _context;

        public AdminRepository(ChessClubContext context)
        {
            _context = context;
        }

        public List<SelectListItem> GetSeasonDropDown()
        {
            var list = _context.Seasons.Select(s => new SelectListItem()
            {
                Text = s.SeasonName,
                Value = s.SeasonId.ToString()
            }).ToList();
            list.Insert(0, new SelectListItem() {Text="Select Season", Value="Select Season"});
            return list;
        }

        public List<string> GetDivisionsBySeason(int id)
        {
            return _context.Divisions.Where(d => d.SeasonId == id).Select(d => d.DivisionName).ToList();
        }

        public Tuple<string, int> CreateMatch(Matches newMatch)
        {
            string response = string.Empty;
            if (newMatch.SeasonId == 0) response = "Season not selected";
            if (newMatch.Student1Id == 0 || newMatch.Student2Id == 0 || newMatch.Student1Id == newMatch.Student2Id) response = "Invalid Student Selection";
            if (newMatch.MatchDate == null) response = "Date not selected";
            if (response == string.Empty) {
                _context.Matches.Add(newMatch);
                if (_context.SaveChanges() > 0)
                {
                    response = "Match Created Successfully";
                }
            }  
            Tuple<string, int> ret = Tuple.Create(response, newMatch.MatchId);
            return ret;
        }

        public List<SelectListItem> GetStudentsInSeason(int seasonId)
        {
            return _context.Students.Include(s => s.Division).Where(s => s.Division.SeasonId == seasonId).Include(s => s.User).Select(s => new SelectListItem()
            {
                Text = s.User.Nickname + " - Division: " + s.Division.DivisionName,
                Value = s.StudentId.ToString()
            }).ToList();
        }

        public List<SelectListItem> GetStudentsInDivision(int divisionId)
        {
            return _context.Students.Where(s => s.DivisionId == _context.Students.Single(r => r.StudentId == divisionId).DivisionId).Include(s => s.User).Include(s => s.Division).Select(s => new SelectListItem()
            {
                Text = s.User.Nickname + " - Division: " + s.Division.DivisionName,
                Value = s.StudentId.ToString()
            }).ToList();
        }

        public string GenerateSchedules(GenerateMatchesViewModel viewModel)
        {
            var returnValue = "";
            if (viewModel.SeasonId != null && viewModel.StartingDate != null)
            {
                var dateRange = new List<DateTime> { viewModel.StartingDate.Value };
                for (var i = 0; i < 14; i++)
                {
                    dateRange.Add(dateRange[i].AddDays(7)); //Generate 15 saturdays
                }
                var existingMatchDates = _context.Matches.Select(m => m.MatchDate).Distinct();
                var doesMatchAlreadyExist = dateRange.Any(date1 => existingMatchDates.Contains(date1));
                if (doesMatchAlreadyExist)
                {
                    returnValue += "Season already exists that has dates in this range.";
                }
                else
                {
                    foreach (var division in _context.Divisions.Where(d => d.SeasonId == viewModel.SeasonId).Select(d => d.DivisionId))
                    {
                        var command = _context.Database.GetDbConnection().CreateCommand();
                        command.CommandText = "dbo.CreateScheduleByDivision";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@SeasonId", SqlDbType.Int) { Value = viewModel.SeasonId });
                        command.Parameters.Add(new SqlParameter("@DivisionId", SqlDbType.Int) { Value = division });
                        command.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.Date) { Value = viewModel.StartingDate });
                        if (command.Connection.State != ConnectionState.Open)
                        {
                            command.Connection.Open();
                        }
                        command.ExecuteNonQuery();
                    }
                    returnValue += "Season Created Successfully";
                }
            }
            else
            {
                returnValue += "Invalid Input";
            }
            return returnValue;
        }
    }
}