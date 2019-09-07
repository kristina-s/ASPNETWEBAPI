using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public IEnumerable<TicketModel> Tickets { get; set; } = new List<TicketModel>();
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public Role Role { get; set; }
        public UserModel()
        {
            Role = Role.Player;
        }
    }
    
}
