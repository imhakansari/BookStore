using System.Collections.Generic;
using System.Linq;
using WebApi;
using WebApi.DbOperations;
using WebApi.Common;
using System;
using AutoMapper;

namespace WebApi.Application.AuthorOperations.Queries
{
    public class GetAuthorDetailQuery
    {
        public int AuthorId { get; set; }
        private readonly BookStoreDbContext _dbcontext;
        private readonly IMapper _mapper;

        public GetAuthorDetailQuery(BookStoreDbContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        public AuthorDetailViewModel Handle()
        {
            var Author = _dbcontext.Authors.SingleOrDefault(x => x.Id == AuthorId);

            if (Author is null)
                throw new InvalidOperationException("Yazar bulunamadı!");

            AuthorDetailViewModel vm = _mapper.Map<AuthorDetailViewModel>(Author);
            return vm;
        }
    }

    public class AuthorDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string BirthDate { get; set; }
    }
}