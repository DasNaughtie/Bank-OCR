namespace Bank.OCR.Tests
{
    using Moq.AutoMock;
    using System.Collections;
    using System.Collections.Generic;
    using Shouldly;
    using Xunit;
    using Bank.OCR.Infrastructure.Utilities.Account;

    public class AccountNumberManagerTests
    {
        private readonly AutoMocker _mocker;
        private readonly AccountNumberManager _accountManager;

        public AccountNumberManagerTests()
        {
            _mocker = new AutoMocker();
            _accountManager = _mocker.CreateInstance<AccountNumberManager>();
        }

        [Theory]
        [ClassData(typeof(AccountEntryTestData))]
        public void AccountNumberManagerReturnsExpectedAccountNo(string[] accountEntry, string expectedAccountNo)
        {
            var actualAccountNumber = _accountManager.ExtractAccountNumberFrom(accountEntry);

            actualAccountNumber.ShouldBe(expectedAccountNo);
        }
    }

    public class AccountEntryTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new[]
                {
                    "    _  _     _  _  _  _  _ ",
                    "  | _| _||_||_ |_   ||_||_|",
                    "  ||_  _|  | _||_|  ||_| _|",
                    ""
                },
                "123456789"
            };

            yield return new object[]
            {
                new[]
                {
                    " _  _  _  _  _  _  _  _  _ ",
                    "| || || || || || || || || |",
                    "|_||_||_||_||_||_||_||_||_|",
                    ""
                },
                "000000000"
            };

            yield return new object[]
            {
                new[]
                {
                    "                           ",
                    "  |  |  |  |  |  |  |  |  |",
                    "  |  |  |  |  |  |  |  |  |",
                    ""
                },
                "111111111"
            };

            yield return new object[]
            {
                new[]
                {
                    " _  _  _  _  _  _  _  _  _ ",
                    "|_||_||_||_||_||_||_||_||_|",
                    " _| _| _| _| _| _| _| _| _|",
                    ""
                },
                "999999999"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}