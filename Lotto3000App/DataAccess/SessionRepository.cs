using DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class SessionRepository : IRepository<Session>
    {
        private readonly LottoDbContext _context;
        public SessionRepository(LottoDbContext context)
        {
            _context = context;
        }

        public void Add(Session entity)
        {
            _context.Sessions.Add(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Session> GetAll()
        {
            return _context.Sessions;
        }

        public void Update(Session entity)
        {
            _context.Sessions.Update(entity);
            _context.SaveChanges();
        }
    }
}
