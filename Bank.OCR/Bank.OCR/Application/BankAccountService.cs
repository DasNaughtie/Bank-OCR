namespace Bank.OCR.Application
{
    using Infrastructure.Utilities.Account;
    using Infrastructure.Utilities.File;

    public class BankAccountService
    {
        private readonly IOCRFileReader _fileReader;
        private readonly IAccountNumberManager _accountNumberManager;

        public BankAccountService(IOCRFileReader fileReader, IAccountNumberManager accountNumberManager)
        {
            _fileReader = fileReader;
            _accountNumberManager = accountNumberManager;
        }

        public string[] ProcessFile(string filename)
        {
            var accountEntries = _fileReader.ReadFile(filename).Result;
            var counter = 0;
            var accountNumbers = new string[accountEntries.Length / Constants.RowsPerAccountEntry];

            for (int i = 0; i < accountEntries.Length; i += Constants.RowsPerAccountEntry)
            {
                var accountEntry = new string[Constants.RowsPerDigit];
                accountEntry[0] = accountEntries[i];
                accountEntry[1] = accountEntries[i + 1];
                accountEntry[2] = accountEntries[i + 2];
                accountNumbers[counter] = _accountNumberManager.ExtractAccountNumberFrom(accountEntry);
                counter++;
            }

            return accountNumbers;
        }
    }
}