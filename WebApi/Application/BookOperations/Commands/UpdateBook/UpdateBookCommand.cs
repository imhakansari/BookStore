using System.Collections.Generic;
using System.Linq;
using WebApi;
using WebApi.DbOperations;
using WebApi.Common;
using System;

namespace WebApi.Application.BookOperations.Commands
{
    public class UpdateBookCommand
    {
        public int BookId { get; set; }
        public UpdateBookModel Model {get;set;}
        private readonly IBookStoreDbContext _dbcontext;

        public UpdateBookCommand(IBookStoreDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public void Handle()
        {
            var book = _dbcontext.Books.SingleOrDefault(x => x.Id == BookId);
            if (book is null)
                throw new InvalidOperationException("Güncellenecek kitap bulunamadı!");

            book.Title  = Model.Title;
            book.PublishDate = Model.PublishDate;
            book.PageCount = Model.PageCount;
            book.GenreId = Model.GenreId;

            _dbcontext.SaveChanges();
        }
    }

    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}