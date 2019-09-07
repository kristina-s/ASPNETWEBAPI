using DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class WinnerRepository :IRepository<Winner>
    {
        private readonly LottoDbContext _context;
        public WinnerRepository(LottoDbContext context)
        {
            _context = context;
        }

        public void Add(Winner entity)
        {
            _context.Winners.Add(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Winner> GetAll()
        {
            return _context.Winners;
        }

        public void Update(Winner entity)
        {
            _context.Winners.Update(entity);
            _context.SaveChanges();
        }
    }
}
