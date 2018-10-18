namespace Bank.OCR.Tests
{
    using System.IO;
    using Bank.OCR.Infrastructure.Utilities.File;
    using Moq.AutoMock;
    using Shouldly;
    using Xunit;

    public class OCRFileReaderTests
    {
        private readonly OCRFileReader _fileReader;
        private readonly AutoMocker _mocker;

        public OCRFileReaderTests()
        {
            _mocker = new AutoMocker();
            _fileReader = _mocker.CreateInstance<OCRFileReader>();
        }

        [Theory]
        [InlineData(@"C:\Dev\Bank-OCR\Bank.OCR\Bank.OCR.Tests\TestData\TestDataFile.txt", 4)]
        [InlineData("random_nonsense", 0)]
        public void FileReaderAccountEntryFileReturnsCorrectEntryDetails(string filename, int expectedLength)
        {
            var actualResult = _fileReader.ReadFile(filename).Result;

            actualResult.Length.ShouldBe(expectedLength);
        }

        [Fact]
        public void FileReaderEmptyFilenameReturnsEmptyEntryDetails()
        {
            var actualResult = _fileReader.ReadFile(string.Empty).Result;

            actualResult.Length.ShouldBe(0);
        }

        [Fact]
        public void FileWriterAccountsSavedCorrectly()
        {
            var accountNumbers = new string[] {"111111111", "222222222", "333333333"};

            var filename = _fileReader.WriteToFile(accountNumbers).Result;

            File.Exists(filename).ShouldBe(true);

            File.Delete(filename);
        }
    }
}