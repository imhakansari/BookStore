using System.Collections.Generic;
using System.Linq;
using WebApi;
using WebApi.DbOperations;
using WebApi.Common;
using System;
using AutoMapper;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Commands
{
    public class CreateAuthorCommand
    {
        public CreateAuthorModel Model {get;set;}
        private readonly BookStoreDbContext _dbcontext;
        private readonly IMapper _mapper;

        public CreateAuthorCommand(BookStoreDbContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var author = _dbcontext.Authors.SingleOrDefault(x => x.Name == Model.Name && x.Surname == Model.Surname && x.BirthDate == Model.BirthDate);
            if (author is not null)
                throw new InvalidOperationException("Yazar zaten mevcut!");

            author = _mapper.Map<Author>(Model);
            _dbcontext.Authors.Add(author);
            _dbcontext.SaveChanges();
        }
    }

    public class CreateAuthorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
    }
}