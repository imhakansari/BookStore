using System.Collections.Generic;
using System.Linq;
using WebApi;
using WebApi.DbOperations;
using WebApi.Common;
using System;

namespace WebApi.Application.GenreOperations.Commands
{
    public class UpdateGenreCommand
    {
        public int GenreId { get; set; }
        public UpdateGenreModel Model {get;set;}
        private readonly IBookStoreDbContext _dbcontext;

        public UpdateGenreCommand(IBookStoreDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public void Handle()
        {
            var genre = _dbcontext.Genres.SingleOrDefault(x => x.Id == GenreId);
            if (genre is null)
                throw new InvalidOperationException("Güncellenecek kitap türü bulunamadı!");

            if (_dbcontext.Genres.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Id != GenreId))
                throw new InvalidOperationException("Aynı isimli bir kitap türü zaten mevcut!");

            genre.Name  = string.IsNullOrEmpty(Model.Name.Trim()) ? genre.Name : Model.Name;
            genre.IsActive = Model.IsActive;

            _dbcontext.SaveChanges();
        }
    }

    public class UpdateGenreModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}