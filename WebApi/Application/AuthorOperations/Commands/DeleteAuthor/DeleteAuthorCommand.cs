using System.Collections.Generic;
using System.Linq;
using WebApi;
using WebApi.DbOperations;
using WebApi.Common;
using System;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Application.AuthorOperations.Commands
{
    public class DeleteAuthorCommand
    {
        public int AuthorId { get; set; }
        private readonly IBookStoreDbContext _dbcontext;

        public DeleteAuthorCommand(IBookStoreDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public void Handle()
        {
            var author = _dbcontext.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if (author is null)
                throw new InvalidOperationException("Silinecek yazar bulunamadı!");

            if (_dbcontext.Books.Any(x => x.AuthorId == author.Id))
                throw new InvalidOperationException("Kitabı yayında olan bir yazar silinemez!");

            _dbcontext.Authors.Remove(author);
            _dbcontext.SaveChanges();
        }
    }

    public class DeleteAuthorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
    }
}