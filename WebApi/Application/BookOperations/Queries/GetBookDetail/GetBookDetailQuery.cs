using System.Collections.Generic;
using System.Linq;
using WebApi;
using WebApi.DbOperations;
using WebApi.Common;
using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Application.BookOperations.Queries
{
    public class GetBookDetailQuery
    {
        public int BookId { get; set; }
        private readonly IBookStoreDbContext _dbcontext;
        private readonly IMapper _mapper;

        public GetBookDetailQuery(IBookStoreDbContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        public BookDetailViewModel Handle()
        {
            var book = _dbcontext.Books.Include(x => x.Genre).Include(x=> x.Author).Where(x => x.Id == BookId).SingleOrDefault();

            if (book is null)
                throw new InvalidOperationException("Kitap bulunamad─▒!");

            BookDetailViewModel vm = _mapper.Map<BookDetailViewModel>(book);
            return vm;
        }
    }

    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
}