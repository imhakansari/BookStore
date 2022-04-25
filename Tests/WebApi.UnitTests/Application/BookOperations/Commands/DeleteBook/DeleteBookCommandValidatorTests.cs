using System;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands;
using Xunit;

namespace Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidatorTest : IClassFixture<CommonTestFixture>
    {

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(int bookId)
        {
            //arrang (Hazırlık)
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId = bookId;

            //act (Çalıştırma)
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            //assert (Doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidIdIsGiven_Validator_ShouldBeReturnSuccess()
        {
            //arrang (Hazırlık)
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId = 3;

            //act (Çalıştırma)
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            //assert (Doğrulama)
            result.Errors.Count.Should().Be(0);
        }
    }
}