using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class RegisterModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int TicketId { get; set; }
        public Role Role { get; set; }
        public RegisterModel()
        {
            Role = Role.Player;
        }
    }
    public enum Role
    {
        Admin = 1,
        Player = 2
    }
}
