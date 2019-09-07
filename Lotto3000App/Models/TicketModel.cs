using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class TicketModel
    {
        //we should get this by token, when token validation done
        public int UserId { get; set; }
        public int SessionId { get; set; }
        public IEnumerable<int> TicketCombination { get; set; }
    }
}
