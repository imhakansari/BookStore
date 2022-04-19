using System.Collections.Generic;
using System.Linq;
using WebApi.DbOperations;
using AutoMapper;
using System;

namespace WebApi.Application.AuthorOperations.Queries
{
    public class GetAuthorsQuery
    {
        private readonly BookStoreDbContext _dbcontext;
        private readonly IMapper _mapper;

        public GetAuthorsQuery(BookStoreDbContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        public List<AuthorsViewModel> Handle()
        {
            var AuthorList = _dbcontext.Authors.OrderBy(x=>x.Id);
            List<AuthorsViewModel> vm = _mapper.Map<List<AuthorsViewModel>>(AuthorList);
            return vm;
        }
    }

    public class AuthorsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string BirthDate { get; set; }
    }
}