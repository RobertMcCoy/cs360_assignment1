using System.Linq;
using ChessClubManagement.Models;
using ChessClubManagement.ViewModels;

namespace ChessClubManagement.Controllers
{
    internal class HomeRepository
    {
        private readonly ChessClubContext _context;

        public HomeRepository(ChessClubContext context)
        {
            _context = context;
        }

        public void UpdateProfile(UserProfileViewModel viewModel)
        {
            if (viewModel.UserId != null)
            {
                var userToUpdate = _context.Users.Single(s => s.Id == int.Parse(viewModel.UserId));
                userToUpdate.Nickname = viewModel.Name;
                userToUpdate.PhoneNumber = viewModel.PhoneNumber;
                _context.Users.Update(userToUpdate);
                var success = _context.SaveChanges();
            }
        }
    }
}