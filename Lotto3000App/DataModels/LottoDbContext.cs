using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace DataModels
{
    public class LottoDbContext : DbContext
    {
        public LottoDbContext(DbContextOptions options) :  base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Winner> Winners { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(
                Encoding.ASCII.GetBytes("admin"));
            var hashedPassword = Encoding.ASCII.GetString(md5data);

            builder.Entity<User>()
                .HasData(
                new User()
                {
                    Id = 1,
                    Firstname = "Jon",
                    Lastname = "Snow",
                    Username = "admin",
                    Password = hashedPassword
                });
            
        }

    }
}
