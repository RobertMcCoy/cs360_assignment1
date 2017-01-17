using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ChessClubManagement.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChessClubManagement.Models
{
    public class StudentRepository
    {
        private readonly ChessClubContext _context;

        public StudentRepository(ChessClubContext context)
        {
            _context = context;
        }

        public List<Users> GetStudents()
        {
            return _context.Users.ToList();
        }

        public StudentEditViewModel GetStudentEditVmById(int id)
        {
            StudentEditViewModel studentEditViewModel = new StudentEditViewModel()
            {
                User = _context.Users.SingleOrDefault(s => s.Id == id),
                StudentUsers = _context.Students.Where(s => s.UserId == id).Include(s => s.Division).ThenInclude(s => s.Season).ToList(),
                MatchHistory = _context.Matches.Where(s => s.Student1.UserId == id || s.Student2.UserId == id).Include(s => s.Student1).ThenInclude(s => s.User).Include(s => s.Student2).ThenInclude(s => s.User).ToList()
            };
            return studentEditViewModel;
        }

        public List<SelectListItem> GetDivisionsBySeason(int id)
        {
            return _context.Divisions.Where(d => d.SeasonId == id).Select(d => new SelectListItem()
            {
                Text = d.DivisionName,
                Value = d.DivisionId.ToString()
            }).ToList();
        }

        public List<SelectListItem> GetSeasonDropDown()
        {
            var list = _context.Seasons.Select(s => new SelectListItem()
            {
                Text = s.SeasonName,
                Value = s.SeasonId.ToString()
            }).ToList();
            list.Insert(0, new SelectListItem() { Text = "Select Season", Value = "Select Season" });
            return list;
        }

        public int SaveStudent(StudentEditViewModel viewModel)
        {
            _context.Users.Update(viewModel.User);
            var success = _context.SaveChanges();
            return success;
        }

        public MemberViewModel GetStudentById(int id)
        {
            MemberViewModel viewModel = new MemberViewModel()
            {
                Member = _context.Users.Single(m => m.Id == id),
                Matches = _context.Matches.Where(m => m.Student1.UserId == id || m.Student2.UserId == id).Include(m => m.Student1).ThenInclude(m => m.User).Include(m => m.Student2).ThenInclude(m => m.User).ToList(),
                Divisions =  _context.Students.Where(s => s.UserId == id).Select(s => new MemberDivision()
                                {
                                    Division = s.Division,
                                    PlayerStats = GetPlayerStats(GetStudentRankings(s.Division.SeasonId), s.StudentId, s.Division.DivisionName)
                                }).ToList()
            };
            return viewModel;
        }

        private Tuple<decimal, int, int, int, int> GetPlayerStats(List<StudentRankings> getStudentRankings, int? sStudentId, string divName)
        {
            //Tuple: Points, DivRank, TotalPlayersInDiv, OverallRank, TotalPlayersInSeason
            int overallCounter = 0;
            int divisionCounter = 0;
            decimal points = 0;
            int divRank = 0;
            int overallRank = 0;
            foreach (var ranking in getStudentRankings)
            {
                if (ranking.Id == sStudentId)
                {
                    points = ranking.Total;
                    divRank = divisionCounter;
                    overallRank = overallCounter;
                }
                if (ranking.DivisionName.Equals(divName))
                {
                    divisionCounter++;
                }
                overallCounter++;
            }
            return Tuple.Create(points, divRank, divisionCounter, overallRank, overallCounter);
        }

        private List<StudentRankings> GetStudentRankings(int? seasonId)
        {
            var command = _context.Database.GetDbConnection().CreateCommand();
            command.CommandText = "dbo.StudentRankings";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@SeasonId", SqlDbType.Int) { Value = seasonId });
            if (command.Connection.State != ConnectionState.Open)
            {
                command.Connection.Open();
            }
            var reader = command.ExecuteReader();
            var rankings = new List<StudentRankings>();
            while (reader.Read())
            {
                rankings.Add(new StudentRankings()
                {
                    Id = reader.GetInt32(0),
                    DivisionName = reader.GetString(2),
                    Nickname = reader.GetString(1),
                    Total = reader.GetDecimal(3)
                });
            }
            return rankings.OrderBy(r => r.Total).ThenBy(r => r.Nickname).ToList();
        }

        public string AddStudentToDivision(StudentEditViewModel viewModel)
        {
            if (viewModel.NewUserStudent.DivisionId == 0 || viewModel.NewUserStudent.DivisionId == null) return "Invalid division selection";
            if (
                _context.Students.Any(
                    s => s.User.Id == viewModel.User.Id && s.DivisionId == viewModel.NewUserStudent.DivisionId))
                return "Division already assigned to this Member";
            if (
                _context.Students.Where(s => s.UserId.Value == viewModel.User.Id)
                    .Include(s => s.Division)
                    .Any(s => s.Division.SeasonId == viewModel.NewDivisionSeasonId))
                return "Already assigned to a division for that season";
            viewModel.NewUserStudent.UserId = viewModel.User.Id;
            _context.Students.Add(viewModel.NewUserStudent);
            if (_context.SaveChanges() > 0) return "Added to division successfully";
            return "Error adding student to division";
        }
    }
}
