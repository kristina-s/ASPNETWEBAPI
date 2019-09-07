using DataAccess;
using DataModels;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services
{
    public class WinnersService : IWinnersService
    {
        private readonly IRepository<Winner> _winnersRepository;
        private readonly IRepository<Session> _sessionRepository;
        private readonly IRepository<Ticket> _ticketRepository;
        private readonly IRepository<User> _userRepository;
        public WinnersService(IRepository<Winner> winnersRepository, 
                                IRepository<Session> sessionRepository,
                                IRepository<Ticket> ticketRepository, 
                                IRepository<User> userRepository)
        {
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
            _sessionRepository = sessionRepository;
            _winnersRepository = winnersRepository;
        }

        

        
        public IEnumerable<WinnerModel> GetWinners(int sessionId)
        {
            
            return _winnersRepository.GetAll().Where(x => x.SessionId == sessionId)
                .Select(x => 
                    new WinnerModel
                    {
                        Id = x.Id,
                        SessionId = x.SessionId,
                        Fullname = x.Fullname,
                        TicketCombination = x.TicketCombination
                    });
        }
        
    }
}
