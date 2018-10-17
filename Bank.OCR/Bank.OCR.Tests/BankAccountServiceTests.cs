namespace Bank.OCR.Tests
{
    using Infrastructure.Utilities.Account;
    using Infrastructure.Utilities.File;
    using Moq;
    using Moq.AutoMock;
    using Xunit;
    using TestStack.BDDfy;
    using Bank.OCR.Application;
    using Shouldly;

    public class BankAccountServiceTests
    {
        private readonly AutoMocker _mocker;
        private readonly Mock<IOCRFileReader> _fileReader;
        private readonly Mock<IAccountNumberManager> _accountNumberManager;
        private readonly Mock<ICheckSumValidator> _checkSumValidator;
        private readonly BankAccountService _accountService;

        private string[] AccountEntries = new[]
        {
            "    _  _     _  _  _  _  _ ",
            "  | _| _||_||_ |_   ||_||_|",
            "  ||_  _|  | _||_|  ||_| _|",
            "",
            " _  _  _  _  _  _  _  _  _ ",
            "| || || || || || || || || |",
            "|_||_||_||_||_||_||_||_||_|",
            "",
            "                           ",
            "  |  |  |  |  |  |  |  |  |",
            "  |  |  |  |  |  |  |  |  |",
            "",
            " _  _  _  _  _  _  _  _  _ ",
            "|_||_||_||_||_||_||_||_||_|",
            " _| _| _| _| _| _| _| _| _|",
            ""
        };
        private string[] AccountNumbers = new[] { "123456789", "000000000", "111111111", "999999999" };
        private string[] InvalidAccountNumbers = { "000000051", "49006771? ILL", "1234?678? ILL", "490067715 ERR" };
        private string _filename;
        private string[] _result;

        public BankAccountServiceTests()
        {
            _mocker = new AutoMocker();
            _fileReader = _mocker.GetMock<IOCRFileReader>();
            _accountNumberManager = _mocker.GetMock<IAccountNumberManager>();
            _checkSumValidator = _mocker.GetMock<ICheckSumValidator>();
            _accountService = _mocker.CreateInstance<BankAccountService>();
        }

        [Theory]
        [InlineData(@"C:\Dev\Bank.OCR\Bank.OCR.Tests\MultiAccountTestDataFile.txt", 4)]
        public void BankAccountServiceProcessFileCorrectly(string filename, int expectedEntries)
        {
            this.Given(x => x.GivenAFileName(filename))
                .And(x => x.GivenAFileReader())
                .And(x => x.GivenAnAccountNumberManager())
                .And(x => x.GivenACheckSumValidator())
                .When(x => x.WhenICallProcessFile())
                .Then(x => x.ThenTheFileReaderIsCalledCorrectly())
                .And(x => x.ThenTheAccountNumberManagerIsCalledCorrectly(expectedEntries))
                .And(x => x.ThenTheResultIsAsExpected(expectedEntries))
                .BDDfy();
        }

        [Theory]
        [InlineData(@"C:\Dev\Bank.OCR\Bank.OCR.Tests\CheckSumValidationTestDataFile.txt", 4)]
        public void BankAccountServiceFailedAccountNumbersReturnsCorrectly(string filename, int expectedEntries)
        {
            this.Given(x => x.GivenAFileName(filename))
                .And(x => x.GivenAFileReader())
                .And(x => x.GivenAnAccountNumberManager())
                .And(x => x.GivenACheckSumValidatorReturningFailures())
                .When(x => x.WhenICallProcessFile())
                .Then(x => x.ThenTheFileReaderIsCalledCorrectly())
                .And(x => x.ThenTheAccountNumberManagerIsCalledCorrectly(expectedEntries))
                .And(x => x.ThenTheFailedResultIsAsExpected(expectedEntries))
                .BDDfy();
        }


        private void GivenAFileName(string filename)
        {
            _filename = filename;
        }

        private void GivenAFileReader()
        {
            _fileReader.Setup(x => x.ReadFile(_filename)).ReturnsAsync(AccountEntries);
        }

        private void GivenAnAccountNumberManager()
        {
            _accountNumberManager.SetupSequence(x => x.ExtractAccountNumberFrom(It.IsAny<string[]>()))
                .Returns(AccountNumbers[0])
                .Returns(AccountNumbers[1])
                .Returns(AccountNumbers[2])
                .Returns(AccountNumbers[3]);
        }

        private void GivenACheckSumValidator()
        {
            _checkSumValidator.Setup(x => x.ValidateAccountNumbers(It.IsAny<string[]>())).Returns(AccountNumbers);
        }

        private void GivenACheckSumValidatorReturningFailures()
        {
            _checkSumValidator.Setup(x => x.ValidateAccountNumbers(It.IsAny<string[]>())).Returns(InvalidAccountNumbers);
        }

        private void WhenICallProcessFile()
        {
            _result = _accountService.ProcessFile(_filename);
        }

        private void ThenTheFileReaderIsCalledCorrectly()
        {
            _fileReader.Verify(x => x.ReadFile(It.IsAny<string>()), Times.Once);
        }

        private void ThenTheAccountNumberManagerIsCalledCorrectly(int expectedCount)
        {
            _accountNumberManager.Verify(x => x.ExtractAccountNumberFrom(It.IsAny<string[]>()), Times.Exactly(expectedCount));
        }

        private void ThenTheResultIsAsExpected(int expectedCount)
        {
            _result.Length.ShouldBe(expectedCount);

            for (int i = 0; i < _result.Length; i++)
            {
                _result[i].ShouldBe(AccountNumbers[i]);
            }
        }

        private void ThenTheFailedResultIsAsExpected(int expectedCount)
        {
            _result.Length.ShouldBe(expectedCount);

            for (int i = 0; i < _result.Length; i++)
            {
                _result[i].ShouldBe(InvalidAccountNumbers[i]);
            }
        }
    }
}