using System.Collections.Generic;
using System.Linq;
using WebApi;
using WebApi.DbOperations;
using WebApi.Common;
using System;
using AutoMapper;
using WebApi.Entities;

namespace WebApi.Application.BookOperations.Commands
{
    public class CreateBookCommand
    {
        public CreateBookModel Model {get;set;}
        private readonly IBookStoreDbContext _dbcontext;
        private readonly IMapper _mapper;

        public CreateBookCommand(IBookStoreDbContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var book = _dbcontext.Books.SingleOrDefault(x => x.Title == Model.Title);
            if (book is not null)
                throw new InvalidOperationException("Kitap zaten mevcut!");

            book = _mapper.Map<Book>(Model);
            _dbcontext.Books.Add(book);
            _dbcontext.SaveChanges();
        }
    }

    public class CreateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}