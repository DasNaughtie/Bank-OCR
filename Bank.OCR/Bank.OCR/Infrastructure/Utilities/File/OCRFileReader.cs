namespace Bank.OCR.Infrastructure.Utilities.File
{
    using System.IO;

    public interface IOCRFileReader
    {
        string[] ReadFile(string filename);
    }

    public class OCRFileReader : IOCRFileReader
    {
        private const int MaximumAccountEntries = 500;
        private const int RowsPerAccountEntry = 4;

        public string[] ReadFile(string filename)
        {
            if (string.IsNullOrEmpty(filename))
                return new string[] { };

            if (!File.Exists(filename))
                return new string[] { };

            var accountEntries = new string[RowsPerAccountEntry * MaximumAccountEntries];

            accountEntries = File.ReadAllLines(filename);

            return accountEntries;
        }
    }
}