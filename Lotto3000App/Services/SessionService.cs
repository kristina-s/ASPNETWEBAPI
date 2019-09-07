using DataAccess;
using DataModels;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services
{
    public class SessionService :ISessionService
    {
        private readonly IRepository<Session> _sessionRepository;
        private readonly IRepository<Ticket> _ticketRepository;
        private readonly IRepository<Winner> _winnersRepository;
        private readonly IRepository<User> _userRepository;
        public SessionService(IRepository<User> userRepository, IRepository<Session> sessionRepository, IRepository<Ticket> ticketRepository, IRepository<Winner> winnersRepository) 
        {
            _sessionRepository = sessionRepository;
            _ticketRepository = ticketRepository;
            _winnersRepository = winnersRepository;
            _userRepository = userRepository;
        }

        public void CloseSession(int currentSessionId)
        {
            var currentSession = _sessionRepository.GetAll().SingleOrDefault(x => x.Id == currentSessionId);
            var random = new Random();
            var winningCombo = Enumerable.Range(1, 37).OrderBy(x => random.Next()).Take(8);
            currentSession.WinningCombination = winningCombo;
            currentSession.IsClosed = true;
            _sessionRepository.Update(currentSession);
        }

        public void CreateSession(SessionModel model)
        {
            var session = _sessionRepository.GetAll().SingleOrDefault(x => x.Id == model.Id);
            Session newSession = new Session
            {
                Id = session.Id,
                Created = session.Created,
                WinningCombination = session.WinningCombination,
                IsClosed = session.IsClosed
            };
            _sessionRepository.Add(newSession);
        }

        public int GetCurrentSession()
        {
            var currentSession = _sessionRepository.GetAll().SingleOrDefault(x => x.IsClosed == false);
            return currentSession.Id;
        }

        public IEnumerable<TicketModel> GetWinningTickets(int sessionId)
        {
            var sessionTickets = _ticketRepository.GetAll()
                .Where(x => x.SessionId == sessionId)
                .Select(x =>
                new TicketModel()
                {
                    TicketCombination = x.TicketCombination,
                    UserId = x.UserId,
                    SessionId = x.SessionId
                }
                );
            var sessionNumbers = _sessionRepository.GetAll().SingleOrDefault(x => x.Id == sessionId).WinningCombination;
            return sessionTickets.Where(x => x.TicketCombination.Intersect(sessionNumbers).Count() > 3);
        }

        public void AddWinnersByThisSession(int sessionId)
        {
            var sessionNumbers = _sessionRepository.GetAll().SingleOrDefault(x => x.Id == sessionId).WinningCombination;

            var winners = GetWinningTickets(sessionId)
                .Select(x =>
                new Winner
                {
                    Fullname = GetUserName(x.UserId),
                    TicketCombination = x.TicketCombination,
                    SessionId = x.SessionId
                    
                }); ;
            foreach (var item in winners)
            {
                var prize = SetPrize(item.TicketCombination.Intersect(sessionNumbers).Count());
                item.Prize = (int)prize;
                _winnersRepository.Add(item);
            }

        }
        public string GetUserName(int userId)
        {
            var user = _userRepository.GetAll().SingleOrDefault(x => x.Id == userId);
            return $"{user.Firstname} {user.Lastname}";
        }

        public Prize SetPrize(int count)
        {
            switch (count)
            {
                case 7:
                    return Prize.Car;
                case 6:
                    return Prize.Vacation;
                case 5:
                    return Prize.Tv;
                case 4:
                    return Prize.GiftCard;
                default:
                    return Prize.NoPrize;

            };
        }
    }
}
