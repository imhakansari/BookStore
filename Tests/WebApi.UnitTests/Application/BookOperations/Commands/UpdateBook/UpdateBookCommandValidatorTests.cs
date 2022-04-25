using System;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands;
using Xunit;

namespace Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTest : IClassFixture<CommonTestFixture>
    {

        [Theory]
        [InlineData(4, "Lord Of The Rings", 0, 0)]
        [InlineData(4, "Lord Of The Rings", 0, 1)]
        [InlineData(4, "Lord Of The Rings", 100, 0)]
        [InlineData(0, "", 0, 0)]
        [InlineData(0, "", 100, 1)]
        [InlineData(0, "", 0, 1)]
        [InlineData(3, "Lor", 100, 1)]
        [InlineData(3, "Lord", 100, 0)]
        [InlineData(3, "Lord", 0, 1)]
        [InlineData(4, " ", 100, 1)]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(int bookId, string title, int pageCount, int genreId)
        {
            //arrang (Hazırlık)
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.BookId = bookId;
            command.Model = new UpdateBookModel(){
                Title = title,
                PageCount = pageCount,
                PublishDate = DateTime.Now.Date.AddYears(-1),
                GenreId = genreId
            };

            //act (Çalıştırma)
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            //assert (Doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            //arrang (Hazırlık)
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.BookId = 3;
            command.Model = new UpdateBookModel(){
                Title = "Lord Of The Rings",
                PageCount = 100,
                PublishDate = DateTime.Now.Date,
                GenreId = 1
            };

            //act (Çalıştırma)
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            //assert (Doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldBeReturnSuccess()
        {
            //arrang (Hazırlık)
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.BookId = 3;
            command.Model = new UpdateBookModel(){
                Title = "Lord Of The Rings",
                PageCount = 100,
                PublishDate = DateTime.Now.Date.AddYears(-2),
                GenreId = 1
            };

            //act (Çalıştırma)
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            //assert (Doğrulama)
            result.Errors.Count.Should().Be(0);
        }
    }
}