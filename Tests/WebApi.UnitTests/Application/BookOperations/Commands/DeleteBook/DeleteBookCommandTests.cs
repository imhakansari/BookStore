using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteBookCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenUndefinedIdIsGiven_InvalidOperationExeption_ShouldBeReturn()
        {
            //arrang (Hazırlık)
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = 4;
            
            //act (Çalıştırma)
            //assert (Doğrulama)
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinecek kitap bulunamadı!");
        }

        [Fact]
        public void WhenValidIdIsGiven_Book_ShouldBeDeleted()
        {
            //arrang (Hazırlık)
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = 3;
            
            //act (Çalıştırma)
            FluentActions.Invoking(()=> command.Handle()).Invoke();

            //assert (Doğrulama)
            var book = _context.Books.SingleOrDefault(book => book.Id == command.BookId);
            book.Should().BeNull();
        }
    }
}