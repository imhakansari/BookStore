using System.Collections.Generic;
using System.Linq;
using WebApi;
using WebApi.DbOperations;
using WebApi.Common;
using System;

namespace WebApi.Application.BookOperations.Commands
{
    public class DeleteBookCommand
    {
        public int BookId { get; set; }
        private readonly IBookStoreDbContext _dbcontext;

        public DeleteBookCommand(IBookStoreDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public void Handle()
        {
            var book = _dbcontext.Books.SingleOrDefault(x => x.Id == BookId);
            if (book is null)
                throw new InvalidOperationException("Silinecek kitap bulunamadı!");

            _dbcontext.Books.Remove(book);
            _dbcontext.SaveChanges();
        }
    }

    public class DeleteBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}