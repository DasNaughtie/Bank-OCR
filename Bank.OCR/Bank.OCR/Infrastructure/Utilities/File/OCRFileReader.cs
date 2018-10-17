namespace Bank.OCR.Infrastructure.Utilities.File
{
    using Bank.OCR.Infrastructure.Utilities.Account;
    using System.IO;
    using System.Threading.Tasks;

    public interface IOCRFileReader
    {
        Task<string[]> ReadFile(string filename);
    }

    public class OCRFileReader : IOCRFileReader
    {
        private const int MaximumAccountEntries = 500;

        public async Task<string[]> ReadFile(string filename)
        {
            if (string.IsNullOrEmpty(filename))
                return new string[] { };

            if (!File.Exists(filename))
                return new string[] { };

            var accountEntries = new string[Constants.RowsPerAccountEntry * MaximumAccountEntries];

            accountEntries = await File.ReadAllLinesAsync(filename);

            return accountEntries;
        }
    }
}