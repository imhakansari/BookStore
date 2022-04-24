using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateBookCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationExeption_ShouldBeReturn()
        {
            //arrang (Hazırlık)
            var book = new Book(){Title="Test_WhenAlreadyExistBookTitleIsGiven_InvalidOperationExeption_ShouldBeReturn", PageCount=100, PublishDate=new System.DateTime(1900,01,01), GenreId=1, AuthorId=1 };
            _context.Books.Add(book);
            _context.SaveChanges();

            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.Model = new CreateBookModel(){Title = book.Title};
            //act (Çalıştırma)
            //assert (Doğrulama)
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap zaten mevcut!");
        }

        [Fact]
        public void WhenAValidInputsAreGiven_Book_ShouldBeCreated()
        {
            //arrang (Hazırlık)
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            CreateBookModel model = new CreateBookModel(){Title="Lord Of The Rings", PageCount=100, PublishDate= DateTime.Now.Date.AddYears(-10), GenreId=1};
            command.Model = model;
            
            //act (Çalıştırma)
            FluentActions.Invoking(()=> command.Handle()).Invoke();

            //assert (Doğrulama)
            var book = _context.Books.SingleOrDefault(book => book.Title == model.Title);
            book.Should().NotBeNull();
            book.PageCount.Should().Be(model.PageCount);
            book.PublishDate.Should().Be(model.PublishDate);
            book.GenreId.Should().Be(model.GenreId);
        }
    }
}