namespace Bank.OCR.Tests
{
    using Infrastructure.Utilities.Account;
    using Moq.AutoMock;
    using Shouldly;
    using System.Collections;
    using System.Collections.Generic;
    using Xunit;

    public class CheckSumValidatorTests
    {
        private readonly AutoMocker _mocker;
        private readonly ICheckSumValidator _checkSumValidator;
        private string[] _actualAccountNumbers;

        public CheckSumValidatorTests()
        {
            _mocker = new AutoMocker();
            _checkSumValidator = _mocker.CreateInstance<CheckSumValidator>();
        }

        [Theory]
        [ClassData(typeof(CheckSumValidatorTestData))]
        public void ValidateAccountNumbersReturnsExpected(string[] accountNumbers, string[] expectedAccountNumbers)
        {
            _actualAccountNumbers = _checkSumValidator.ValidateAccountNumbers(accountNumbers);

            for (int i = 0; i < _actualAccountNumbers.Length; i++)
            {
                _actualAccountNumbers[i].ShouldBe(expectedAccountNumbers[i]);
            }

            _actualAccountNumbers.Length.ShouldBe(expectedAccountNumbers.Length);
        }
    }

    public class CheckSumValidatorTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new[]
                {
                    "711111111",
                    "123456789",
                    "490867715",
                    "888888888",
                    "490067715",
                    "012345678"
                },
                new[]
                {
                    "711111111",
                    "123456789",
                    "490867715",
                    "888888888 ERR",
                    "490067715 ERR",
                    "012345678 ERR"
                }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}