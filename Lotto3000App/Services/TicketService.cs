using DataAccess;
using DataModels;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services
{
    public class TicketService : ITicketService
    {
        private readonly IRepository<Ticket> _ticketRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Session> _sessionRepository;


        public TicketService(IRepository<Ticket> ticketRepository, IRepository<User> userRepository, IRepository<Session> sessionRepository)
        {
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
            _sessionRepository = sessionRepository;
        }

        public void CreateTicket(TicketModel model)
        {
            var combination = model.TicketCombination;

            var allUnique = combination.GroupBy(x => x).All(g => g.Count() == 1);
            if (!allUnique) throw new Exception("There are duplicates!");

            var validNumbers = combination.Where(x => x >= 1 && x <= 37);
            if (validNumbers.Count() != 7) throw new Exception("Number invalid!");

            var currentSession = _sessionRepository.GetAll().SingleOrDefault(x => x.IsClosed == false);

            var ticket = new Ticket()
            {
                SessionId = currentSession.Id,
                TicketCombination = combination,
                UserId = model.UserId
            };

            _ticketRepository.Add(ticket);
        }

        public IEnumerable<TicketModel> GetAll()
        {
            return _ticketRepository.GetAll().Select(x =>
                new TicketModel()
                {
                    TicketCombination = x.TicketCombination,
                    UserId = x.UserId,
                    SessionId = x.SessionId
                }
                );
        }
    }
}
