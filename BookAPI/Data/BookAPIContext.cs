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
        public BookAPIContext (DbContextOptions<BookAPIContext> options)
            : base(options)
        {
        }

        public DbSet<BookAPI.Models.Book> Book { get; set; }

        public DbSet<BookAPI.Models.Tag> Tag { get; set; }

        public DbSet<BookAPI.Models.BookTag> BookTag { get; set; }
    }
}
