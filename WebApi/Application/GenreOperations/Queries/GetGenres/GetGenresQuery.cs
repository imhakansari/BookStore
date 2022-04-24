using System.Collections.Generic;
using System.Linq;
using WebApi.DbOperations;
using AutoMapper;

namespace WebApi.Application.GenreOperations.Queries
{
    public class GetGenresQuery
    {
        private readonly IBookStoreDbContext _dbcontext;
        private readonly IMapper _mapper;

        public GetGenresQuery(IBookStoreDbContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        public List<GenresViewModel> Handle()
        {
            var GenreList = _dbcontext.Genres.Where(x=>x.IsActive).OrderBy(x=>x.Id);
            List<GenresViewModel> vm = _mapper.Map<List<GenresViewModel>>(GenreList);
            return vm;
        }
    }

    public class GenresViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}