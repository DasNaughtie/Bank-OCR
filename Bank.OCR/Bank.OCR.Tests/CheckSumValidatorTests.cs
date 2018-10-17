namespace Bank.OCR.Tests
{
    using Infrastructure.Utilities.Account;
    using Moq.AutoMock;
    using Shouldly;
    using Xunit;

    public class CheckSumValidatorTests
    {
        private readonly AutoMocker _mocker;
        private readonly CheckSumValidator _checkSumValidator;

        public CheckSumValidatorTests()
        {
            _mocker = new AutoMocker();
            _checkSumValidator = _mocker.CreateInstance<CheckSumValidator>();
        }

        [Theory]
        [InlineData("711111111", true)]
        [InlineData("123456789", true)]
        [InlineData("490867715", true)]
        [InlineData("888888888", false)]
        [InlineData("490067715", false)]
        [InlineData("012345678", false)]
        public void CheckSumValidatesAccountNumberCorrectly(string accountNumber, bool expectedOutcome)
        {
            var actualOutcome = _checkSumValidator.CheckSumIsValid(accountNumber);

            actualOutcome.ShouldBe(expectedOutcome);
        }
    }
}