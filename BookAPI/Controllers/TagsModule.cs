
using BookAPI.Models;
using Microsoft.EntityFrameworkCore;
using Nancy;
using System;
using System.Linq;

namespace BookAPI.Controllers
{
    public class TagsModule : NancyModule
    {
        private BookAPIContext _appContext = new BookAPIContext(true);

        public TagsModule() : base("tags")
        {

            
            Get("/SayHello2/{name}", args => $"Hello {args.name}");

            Get("/", args => {

                var model = _appContext.Tag.ToList();
                return model;
            });

            Get("/{id}", args =>
            {
                int bookId = args.id;
                var model = _appContext.Book.Where(x => x.Id == bookId).ToList();
                return model;

            });

        }
    }
}