using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface ISessionService
    {
        void CreateSession(SessionModel model);
        int GetCurrentSession();
        void CloseSession(int currentSessionId);
        void AddWinnersByThisSession(int sessionId);
        string GetUserName(int userId);
        IEnumerable<TicketModel> GetWinningTickets(int sessionId);


    }
}
