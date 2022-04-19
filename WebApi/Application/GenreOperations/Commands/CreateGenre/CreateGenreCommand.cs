using System.Collections.Generic;
using System.Linq;
using WebApi;
using WebApi.DbOperations;
using WebApi.Common;
using System;
using AutoMapper;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Commands
{
    public class CreateGenreCommand
    {
        public CreateGenreModel Model {get;set;}
        private readonly BookStoreDbContext _dbcontext;
        private readonly IMapper _mapper;

        public CreateGenreCommand(BookStoreDbContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var genre = _dbcontext.Genres.SingleOrDefault(x => x.Name == Model.Name);
            if (genre is not null)
                throw new InvalidOperationException("Kitap türü zaten mevcut!");

            genre = new Genre();
            genre.Name = Model.Name;
            _dbcontext.Genres.Add(genre);
            _dbcontext.SaveChanges();
        }
    }

    public class CreateGenreModel
    {
        public string Name { get; set; }
    }
}