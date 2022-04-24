using System.Collections.Generic;
using System.Linq;
using WebApi;
using WebApi.DbOperations;
using WebApi.Common;
using System;

namespace WebApi.Application.AuthorOperations.Commands
{
    public class UpdateAuthorCommand
    {
        public int AuthorId { get; set; }
        public UpdateAuthorModel Model {get;set;}
        private readonly IBookStoreDbContext _dbcontext;

        public UpdateAuthorCommand(IBookStoreDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public void Handle()
        {
            var genre = _dbcontext.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if (genre is null)
                throw new InvalidOperationException("Güncellenecek yazar bulunamadı!");

            if (_dbcontext.Authors.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Surname == Model.Surname && x.BirthDate == Model.BirthDate && x.Id != AuthorId))
                throw new InvalidOperationException("Aynı isimli bir yazar zaten mevcut!");

            genre.Name  = string.IsNullOrEmpty(Model.Name.Trim()) ? genre.Name : Model.Name;
            genre.Surname  = string.IsNullOrEmpty(Model.Surname.Trim()) ? genre.Surname : Model.Surname;
            genre.BirthDate  = Model.BirthDate;

            _dbcontext.SaveChanges();
        }
    }

    public class UpdateAuthorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
    }
}