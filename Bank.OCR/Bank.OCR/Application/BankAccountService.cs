namespace Bank.OCR.Application
{
    using System;
    using Infrastructure.Utilities.Account;
    using Infrastructure.Utilities.File;

    public class BankAccountService
    {
        private readonly IOCRFileReader _fileReader;
        private readonly IAccountNumberManager _accountNumberManager;

        private const int RowsPerDigit = 3;

        public BankAccountService(IOCRFileReader fileReader, IAccountNumberManager accountNumberManager)
        {
            _fileReader = fileReader;
            _accountNumberManager = accountNumberManager;
        }

        public string[] ProcessFile(string filename)
        {
            var accountEntries = _fileReader.ReadFile(filename);
            var counter = 0;
            var accountNumbers = new string[accountEntries.Length / 4];

            for (int i = 0; i < accountEntries.Length; i += RowsPerDigit + 1)
            {
                var accountEntry = new string[RowsPerDigit];
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