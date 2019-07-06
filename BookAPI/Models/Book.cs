﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAPI.Models
{
    public class Book
    {
        public Book()
        {
            BookTag = new HashSet<BookTag>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

        public virtual ICollection<BookTag> BookTag { get; set; }
    }
}
