using System;
using System.Collections.Generic;

namespace ChessClubManagement.Models
{
    public partial class Users
    {
        public Users()
        {
            Students = new HashSet<Students>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Nickname { get; set; }
        public string PhoneNumber { get; set; }

        public virtual ICollection<Students> Students { get; set; }
    }
}
