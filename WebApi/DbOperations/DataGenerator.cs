using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Common;
using WebApi.Entities;

namespace WebApi.DbOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                // Look for any book.
                if (context.Books.Any())
                {
                    return;   // Data was already seeded
                }

                context.Authors.AddRange(
                    new Author{
                        Name = "Erdal",
                        Surname = "Demirkıran",
                        BirthDate = new DateTime(2001, 06, 12)
                    },
                    new Author{
                        Name = "Frank",
                        Surname = "Herbert",
                        BirthDate = new DateTime(1920, 10, 08)
                    },
                    new Author{
                        Name = "John Ronald Reuel",
                        Surname = "Tolkien",
                        BirthDate = new DateTime(1892, 05, 15)
                    },
                    new Author{
                        Name = "Jane",
                        Surname = "Austen",
                        BirthDate = new DateTime(1932, 06, 27)
                    }
                );

                context.Genres.AddRange(
                    new Genre{
                        Name = "Personal Growth"
                    },
                    new Genre{
                        Name = "Science Fiction"
                    },
                    new Genre{
                        Name = "Romance"
                    },
                    new Genre{
                        Name = "Fantastic"
                    }
                );

                context.Books.AddRange(
                   new Book
                   {
                       //Id = 1,
                       Title = "Leon Startup",
                       GenreId = 1,
                       AuthorId = 1,
                       PageCount = 100,
                       PublishDate = new DateTime(2001, 06, 12)
                   },
                   new Book
                   {
                       //Id = 2,
                       Title = "Dune",
                       GenreId = 3,
                       AuthorId = 2,
                       PageCount = 1250,
                       PublishDate = new DateTime(2007, 06, 12)
                   },
                   new Book
                   {
                       //Id = 3,
                       Title = "Hobbit",
                       GenreId = 4,
                       AuthorId = 3,
                       PageCount = 1850,
                       PublishDate = new DateTime(2005, 06, 12)
                   });

                context.SaveChanges();
            }
        }
    }
}
