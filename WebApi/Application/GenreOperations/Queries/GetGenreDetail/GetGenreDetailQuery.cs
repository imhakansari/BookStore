using System.Collections.Generic;
using System.Linq;
using WebApi;
using WebApi.DbOperations;
using WebApi.Common;
using System;
using AutoMapper;

namespace WebApi.Application.GenreOperations.Queries
{
    public class GetGenreDetailQuery
    {
        public int GenreId { get; set; }
        private readonly BookStoreDbContext _dbcontext;
        private readonly IMapper _mapper;

        public GetGenreDetailQuery(BookStoreDbContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        public GenreDetailViewModel Handle()
        {
            var Genre = _dbcontext.Genres.SingleOrDefault(x => x.IsActive && x.Id == GenreId);

            if (Genre is null)
                throw new InvalidOperationException("Kitap türü bulunamadı!");

            GenreDetailViewModel vm = _mapper.Map<GenreDetailViewModel>(Genre);
            return vm;
        }
    }

    public class GenreDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}