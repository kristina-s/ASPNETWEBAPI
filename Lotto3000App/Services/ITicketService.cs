using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace Services
{
    public interface ITicketService
    {
        void CreateTicket(TicketModel model);
        IEnumerable<TicketModel> GetAll();
    }
}
