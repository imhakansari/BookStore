using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Queries;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace Application.BookOperations.Commands.GetBookDetail
{
    public class GetBookDetailQueryTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBookDetailQueryTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenUndefinedIdIsGiven_InvalidOperationExeption_ShouldBeReturn()
        {
            //arrang (Hazırlık)
            GetBookDetailQuery command = new GetBookDetailQuery(_context, _mapper);
            command.BookId = 4;
            
            //act (Çalıştırma)
            //assert (Doğrulama)
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap bulunamadı!");
        }

        [Fact]
        public void WhenValidIdIsGiven_Book_ShouldBeGetDetail()
        {
            //arrang (Hazırlık)
            GetBookDetailQuery command = new GetBookDetailQuery(_context, _mapper);
            command.BookId = 3;
            
            //act (Çalıştırma)
            FluentActions.Invoking(()=> command.Handle()).Invoke();

            //assert (Doğrulama)
            var book = _context.Books.SingleOrDefault(book => book.Id == command.BookId);
            book.Should().NotBeNull();
        }
    }
}