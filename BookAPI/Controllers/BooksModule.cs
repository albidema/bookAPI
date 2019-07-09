
using BookAPI.Models;
using Microsoft.EntityFrameworkCore;
using Nancy;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookAPI.Controllers
{
    public class BooksModule : NancyModule
    {
        private BookAPIContext _appContext = new BookAPIContext(true);
       
        public BooksModule() : base("books")
        {

            Get("/", args => {
                
                var model = _appContext.Book.ToList();
                return model;
            });

            Get("/{id}", args =>
            {
                int bookId = args.id;
                var model = _appContext.Book.Where(x => x.Id == bookId).FirstOrDefault();

                if (model == null)
                {
                    return 404;
                }
                   
                return model;
            });

            Delete("/{id}", args => {

                int id = args.id;
                var book = _appContext.Book.Where( x => x.Id == id).FirstOrDefault();

                if (book == null)
                {
                    return 404;
                }

                _appContext.Book.Remove(book);

                try {
                    _appContext.SaveChanges();
                    
                }
                catch {
                    if (!BookExists(id))
                    {
                        return 404;
                    }
                    else
                    {
                        return 400;
                    }
                }
                
                return 200;
            });

            Post("/", args => {
                int bookId = args.id;

                var book = this.Bind<Book>();

                try
                {
                    _appContext.Book.Add(book);

                     _appContext.SaveChanges();
                }
                catch {
                    return 400;
                }


                return 201;
            });

            Put("/{id}", args => 
            {
                int bookId = args.id;
                
                var book = this.Bind<Book>();

                if (bookId != book.Id)
                {
                    return 400;
                }

                _appContext.Entry(book).State = EntityState.Modified;

                var tagsToRemove = _appContext.BookTag.Where(x => x.BookId == bookId);

                if (tagsToRemove.Count() > 0)
                {
                    _appContext.BookTag.RemoveRange(tagsToRemove);
                    _appContext.SaveChanges();
                }

                if (book.BookTag.Count > 0)
                {
                    var bookTags = new HashSet<BookTag>();
                    foreach (var bt in book.BookTag)
                    {
                        bookTags.Add(new BookTag
                        {
                            BookId = bookId,
                            TagId = bt.TagId
                        });
                    }
                    _appContext.BookTag.AddRange(bookTags);
                }

                try
                {
                     _appContext.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(bookId))
                    {
                        return 404;
                    }
                    else
                    {
                        return 400;
                    }
                }

                return 201;

            });

            
        }

        private bool BookExists(int id)
        {
            return _appContext.Book.Any(e => e.Id == id);
        }
    }
}