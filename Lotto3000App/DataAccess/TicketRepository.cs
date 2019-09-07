using DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class TicketRepository : IRepository<Ticket>
    {
        private readonly LottoDbContext _context;
        public TicketRepository(LottoDbContext context)
        {
            _context = context;
        }

        public void Add(Ticket entity)
        {
            _context.Tickets.Add(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Ticket> GetAll()
        {
            return _context.Tickets;
        }

        public void Update(Ticket entity)
        {
            _context.Tickets.Update(entity);
            _context.SaveChanges();
        }
    }
}
