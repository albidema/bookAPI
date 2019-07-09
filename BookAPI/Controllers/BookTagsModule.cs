
using BookAPI.Models;
using Microsoft.EntityFrameworkCore;
using Nancy;
using System;
using System.Linq;

namespace BookAPI.Controllers
{
    public class BookTagsModule : NancyModule
    {
        private BookAPIContext _appContext = new BookAPIContext(true);

        public BookTagsModule() : base("booktags")
        {

            Get("/", args => {

                var model = _appContext.BookTag.ToList();
                return model;
            });

            Get("/{id}", args =>
            {
                int bookId = args.id;
                var bookTag = _appContext.BookTag.Where(x => x.BookId == bookId).Select(x => new Tag { Id = x.TagId, Name = x.Tag.Name }).ToList();

                if (bookTag == null)
                {
                    return 404;
                }

                return bookTag;

            });

        }
    }
}