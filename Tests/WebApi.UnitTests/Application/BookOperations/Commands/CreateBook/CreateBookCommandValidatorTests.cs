using System;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands;
using Xunit;

namespace Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTest : IClassFixture<CommonTestFixture>
    {

        [Theory]
        [InlineData("Lord Of The Rings", 0, 0)]
        [InlineData("Lord Of The Rings", 0, 1)]
        [InlineData("Lord Of The Rings", 100, 0)]
        [InlineData("", 0, 0)]
        [InlineData("", 100, 1)]
        [InlineData("", 0, 1)]
        [InlineData("Lor", 100, 1)]
        [InlineData("Lord", 100, 0)]
        [InlineData("Lord", 0, 1)]
        [InlineData(" ", 100, 1)]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId)
        {
            //arrang (Hazırlık)
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel(){
                Title = title,
                PageCount = pageCount,
                PublishDate = DateTime.Now.Date.AddYears(-1),
                GenreId = genreId
            };

            //act (Çalıştırma)
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            //assert (Doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            //arrang (Hazırlık)
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel(){
                Title = "Lord Of The Rings",
                PageCount = 100,
                PublishDate = DateTime.Now.Date,
                GenreId = 1
            };

            //act (Çalıştırma)
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            //assert (Doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldBeReturnSuccess()
        {
            //arrang (Hazırlık)
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel(){
                Title = "Lord Of The Rings",
                PageCount = 100,
                PublishDate = DateTime.Now.Date.AddYears(-2),
                GenreId = 1
            };

            //act (Çalıştırma)
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            //assert (Doğrulama)
            result.Errors.Count.Should().Be(0);
        }
    }
}