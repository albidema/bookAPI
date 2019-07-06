using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAPI.Models
{
    public class Tag
    {
        public Tag()
        {
            BookTag = new HashSet<BookTag>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<BookTag> BookTag { get; set; }
    }
}
