namespace Bank.OCR.Tests
{
    using Infrastructure.Utilities.Account;
    using Infrastructure.Utilities.File;
    using Moq;
    using Moq.AutoMock;
    using System;
    using System.Linq;
    using Xunit;
    using TestStack.BDDfy;
    using Moq.Language.Flow;
    using Bank.OCR.Application;

    public class BankAccountServiceTests
    {
        private readonly AutoMocker _mocker;
        private readonly Mock<IOCRFileReader> _fileReader;
        private readonly Mock<IAccountNumberManager> _accountNumberManager;
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
        private string[] AccountNumbers = new[]
        {
            "123456789",
            "000000000",
            "111111111",
            "999999999"
        };
        private string _filename;
        private string[] _result;

        public BankAccountServiceTests()
        {
            _mocker = new AutoMocker();
            _fileReader = _mocker.GetMock<IOCRFileReader>();
            _accountNumberManager = _mocker.GetMock<IAccountNumberManager>();
            _accountService = _mocker.CreateInstance<BankAccountService>();
        }

        [Theory]
        [InlineData(@"C:\Dev\Bank.OCR\Bank.OCR.Tests\MultiAccountTestDataFile.txt", 4)]
        public void Test(string filename, int expectedEntries)
        {
            this.Given(x => x.GivenAFileName(filename))
                .And(x => x.GivenAFileReader())
                .And(x => x.GivenAnAccountNumberManager())
                .When(x => x.WhenICallProcessFile())
                .Then(x => x.ThenTheFileReaderIsCalledCorrectly())
                .And(x => x.ThenTheAccountNumberManagerIsCalledCorrectly())
                .And(x => x.ThenTheResultIsAsExpected())
                .BDDfy();
        }

        private void GivenAFileName(string filename)
        {
            _filename = filename;
        }

        private void GivenAFileReader()
        {
            _fileReader.Setup(x => x.ReadFile(_filename)).Returns(AccountEntries);
        }

        private void GivenAnAccountNumberManager()
        {
            _accountNumberManager.SetupSequence(x => x.ExtractAccountNumberFrom(It.IsAny<string[]>()))
                .Returns(AccountNumbers[0])
                .Returns(AccountNumbers[1])
                .Returns(AccountNumbers[2])
                .Returns(AccountNumbers[3]);
        }

        private void WhenICallProcessFile()
        {
            _result = _accountService.ProcessFile(_filename);
        }

        private void ThenTheFileReaderIsCalledCorrectly()
        {
            _fileReader.Verify(x => x.ReadFile(It.IsAny<string>()), Times.Once);
        }

        private void ThenTheAccountNumberManagerIsCalledCorrectly()
        {
            _accountNumberManager.Verify(x => x.ExtractAccountNumberFrom(It.IsAny<string[]>()), Times.Exactly(4));
        }

        private void ThenTheResultIsAsExpected()
        {
            for (int i = 0; i < _result.Length; i++)
            {
                _result[i].SequenceEqual(AccountNumbers[i]);
            }
        }
    }
}