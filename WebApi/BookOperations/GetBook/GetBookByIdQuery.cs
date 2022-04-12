using System.Collections.Generic;
using System.Linq;
using WebApi;
using WebApi.DbOperations;
using WebApi.Common;
using System;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBookByIdQuery
    {
        public int BookId { get; set; }
        private readonly BookStoreDbContext _dbcontext;

        public GetBookByIdQuery(BookStoreDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public BookViewModel Handle()
        {
            var book = _dbcontext.Books.Where(x => x.Id == BookId).SingleOrDefault();

            if (book is null)
                throw new InvalidOperationException("Kitap bulunamadı!");

            BookViewModel vm = new BookViewModel();
            vm.Title = book.Title;
            vm.PageCount = book.PageCount;
            vm.PublishDate = ((GenreEnum)book.GenreId).ToString();
            vm.Genre = book.PublishDate.Date.ToString("dd/MM/yyyy");

            return vm;
        }
    }

    public class BookViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }
}