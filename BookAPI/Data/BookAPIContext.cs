using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookAPI.Models;

namespace BookAPI.Models
{
    public class BookAPIContext : DbContext
    {
        private readonly bool console;
        public BookAPIContext (DbContextOptions<BookAPIContext> options)
            : base(options)
        {
        }

        public BookAPIContext(bool cons) : base()
        {
            console = cons;
        }

        public DbSet<BookAPI.Models.Book> Book { get; set; }

        public DbSet<BookAPI.Models.Tag> Tag { get; set; }

        public DbSet<BookAPI.Models.BookTag> BookTag { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (console)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=BookAPI;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }
    }
}
