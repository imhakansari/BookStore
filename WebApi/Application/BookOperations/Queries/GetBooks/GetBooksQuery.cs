using System.Collections.Generic;
using System.Linq;
using WebApi;
using WebApi.DbOperations;
using WebApi.Common;
using AutoMapper;
using WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Application.BookOperations.Queries
{
    public class GetBooksQuery
    {
        private readonly IBookStoreDbContext _dbcontext;
        private readonly IMapper _mapper;

        public GetBooksQuery(IBookStoreDbContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        public List<BooksViewModel> Handle()
        {
            var bookList = _dbcontext.Books.Include(x => x.Genre).Include(x=> x.Author).OrderBy(x=>x.Id).ToList<Book>();
            List<BooksViewModel> vm = _mapper.Map<List<BooksViewModel>>(bookList);
            return vm;
        }
    }

    public class BooksViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
    }
}