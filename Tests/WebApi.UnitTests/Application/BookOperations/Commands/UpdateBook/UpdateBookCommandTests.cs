using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateBookCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenUndefinedIdAreGiven_InvalidOperationExeption_ShouldBeReturn()
        {
            //arrang (Hazırlık)
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = 4;
            command.Model = new UpdateBookModel(){Title="WhenUndefinedIdAreGiven_InvalidOperationExeption_ShouldBeReturn", PageCount=100, PublishDate=new System.DateTime(1900,01,01), GenreId=1};
            
            //act (Çalıştırma)
            //assert (Doğrulama)
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Güncellenecek kitap bulunamadı!");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeUpdated()
        {
            //arrang (Hazırlık)
            UpdateBookCommand command = new UpdateBookCommand(_context);
            UpdateBookModel model = new UpdateBookModel(){Title="Lord Of The Rings", PageCount=100, PublishDate= DateTime.Now.Date.AddYears(-10), GenreId=1};
            command.BookId = 3;
            command.Model = model;
            
            //act (Çalıştırma)
            FluentActions.Invoking(()=> command.Handle()).Invoke();

            //assert (Doğrulama)
            var book = _context.Books.SingleOrDefault(book => book.Id == command.BookId);
            book.Should().NotBeNull();
            book.PageCount.Should().Be(model.PageCount);
            book.PublishDate.Should().Be(model.PublishDate);
            book.GenreId.Should().Be(model.GenreId);
        }
    }
}