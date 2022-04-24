using System.Collections.Generic;
using System.Linq;
using WebApi;
using WebApi.DbOperations;
using WebApi.Common;
using System;

namespace WebApi.Application.GenreOperations.Commands
{
    public class DeleteGenreCommand
    {
        public int GenreId { get; set; }
        private readonly IBookStoreDbContext _dbcontext;

        public DeleteGenreCommand(IBookStoreDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public void Handle()
        {
            var genre = _dbcontext.Genres.SingleOrDefault(x => x.Id == GenreId);
            if (genre is null)
                throw new InvalidOperationException("Silinecek kitap türü bulunamadı!");

            _dbcontext.Genres.Remove(genre);
            _dbcontext.SaveChanges();
        }
    }

    public class DeleteGenreModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}