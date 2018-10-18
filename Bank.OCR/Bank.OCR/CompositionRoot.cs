namespace Bank.OCR
{
    using Bank.OCR.Application;
    using Infrastructure.Utilities.Account;
    using Infrastructure.Utilities.File;

    public class CompositionRoot
    {
        public BankAccountService BankAccountService { get; protected set; }

        public CompositionRoot()
        {
            Compose();
        }

        private void Compose()
        {
            var basePath = @"c:\Dev";

            var fileReader = new OCRFileReader(basePath);

            var accountNumberManager = new AccountNumberManager();
            var checkSumValidator = new CheckSumValidator();

            BankAccountService = new BankAccountService(fileReader, accountNumberManager, checkSumValidator);
        }
    }
}