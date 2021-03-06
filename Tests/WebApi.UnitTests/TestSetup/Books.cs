using System;
using WebApi.DbOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
            context.Books.AddRange(
                   new Book
                   {
                       Title = "Leon Startup",
                       GenreId = 1,
                       AuthorId = 1,
                       PageCount = 100,
                       PublishDate = new DateTime(2001, 06, 12)
                   },
                   new Book
                   {
                       Title = "Dune",
                       GenreId = 3,
                       AuthorId = 2,
                       PageCount = 1250,
                       PublishDate = new DateTime(2007, 06, 12)
                   },
                   new Book
                   {
                       Title = "Hobbit",
                       GenreId = 4,
                       AuthorId = 3,
                       PageCount = 1850,
                       PublishDate = new DateTime(2005, 06, 12)
                   });
        }
    }
}