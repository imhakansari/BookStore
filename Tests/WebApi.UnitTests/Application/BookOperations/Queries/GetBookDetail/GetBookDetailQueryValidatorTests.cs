using System;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Queries;
using Xunit;

namespace Application.BookOperations.Commands.GetBookDetail
{
    public class GetBookDetailQueryValidatorTest : IClassFixture<CommonTestFixture>
    {

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(int bookId)
        {
            //arrang (Hazırlık)
            GetBookDetailQuery command = new GetBookDetailQuery(null, null);
            command.BookId = bookId;

            //act (Çalıştırma)
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            var result = validator.Validate(command);

            //assert (Doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidIdIsGiven_Validator_ShouldBeReturnSuccess()
        {
            //arrang (Hazırlık)
            GetBookDetailQuery command = new GetBookDetailQuery(null, null);
            command.BookId = 3;

            //act (Çalıştırma)
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            var result = validator.Validate(command);

            //assert (Doğrulama)
            result.Errors.Count.Should().Be(0);
        }
    }
}