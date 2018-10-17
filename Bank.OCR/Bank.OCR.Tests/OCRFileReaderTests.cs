namespace Bank.OCR.Tests
{
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
        [InlineData(@"C:\Dev\Bank.OCR\Bank.OCR.Tests\TestDataFile.txt", 4)]
        [InlineData("random_nonsense", 0)]
        public void FileReaderSingleAccountEntryFileReturnsCorrectEntryDetails(string filename, int expectedLength)
        {
            var actualResult = _fileReader.ReadFile(filename);

            actualResult.Length.ShouldBe(expectedLength);
        }

        [Fact]
        public void FileReaderEmptyFilenameReturnsEmptyEntryDetails()
        {
            var actualResult = _fileReader.ReadFile(string.Empty);

            actualResult.Length.ShouldBe(0);
        }
    }
}