using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Common;

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

                context.Books.AddRange(
                   new Book
                   {
                       //Id = 1,
                       Title = "Leon Startup",
                       GenreId = (int)GenreEnum.PersonalGrowth,
                       PageCount = 100,
                       PublishDate = new DateTime(2001, 06, 12)
                   },
                   new Book
                   {
                       //Id = 2,
                       Title = "Dune",
                       GenreId = (int)GenreEnum.ScienceFiction,
                       PageCount = 1250,
                       PublishDate = new DateTime(2007, 06, 12)
                   },
                   new Book
                   {
                       //Id = 3,
                       Title = "Hobbit",
                       GenreId = (int)GenreEnum.ScienceFiction,
                       PageCount = 1850,
                       PublishDate = new DateTime(2005, 06, 12)
                   });

                context.SaveChanges();
            }
        }
    }
}
