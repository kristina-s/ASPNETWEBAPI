using DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class UserRepository : IRepository<User>
    {
        private readonly LottoDbContext _context;
        public UserRepository(LottoDbContext context)
        {
            _context = context;
        }

        public void Add(User entity)
        {
            _context.Users.Add(entity);
            _context.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public void Update(User entity)
        {
            _context.Users.Update(entity);
            _context.SaveChanges();
        }
    }
}
