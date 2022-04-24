using System;
using WebApi.DbOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
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
        }
    }
}