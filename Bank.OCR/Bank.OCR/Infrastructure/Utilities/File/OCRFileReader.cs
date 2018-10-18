namespace Bank.OCR.Infrastructure.Utilities.File
{
    using Bank.OCR.Infrastructure.Utilities.Account;
    using System;
    using System.IO;
    using System.Threading.Tasks;

    public interface IOCRFileReader
    {
        Task<string[]> ReadFile(string filename);
        Task<string> WriteToFile(string[] accountNumbers);
    }

    public class OCRFileReader : IOCRFileReader
    {
        private string _basePath;

        public OCRFileReader()
        {
        }

        public OCRFileReader(string basePath)
        {
            _basePath = basePath;
        }

        public async Task<string[]> ReadFile(string filename)
        {
            if (string.IsNullOrEmpty(filename))
                return new string[] { };

            if (!File.Exists(filename))
                return new string[] { };

            var accountEntries = new string[Constants.RowsPerAccountEntry * Constants.MaximumAccountEntries];

            accountEntries = await File.ReadAllLinesAsync(filename);

            return accountEntries;
        }

        public async Task<string> WriteToFile(string[] accountNumbers)
        {
            if (accountNumbers.Length == 0)
                return string.Empty;

            var filename = GenerateFilename();

            await File.WriteAllLinesAsync(filename, accountNumbers);

            return filename;
        }

        private string GenerateFilename()
        {
            return $"{_basePath ?? Directory.GetCurrentDirectory()}\\AccountNumbers_{DateTime.Now.ToString("yyyyMMddhhmmss")}.txt";
        }
    }
}