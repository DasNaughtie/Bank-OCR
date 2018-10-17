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
            var fileReader = new OCRFileReader();
            var accountNumberManager = new AccountNumberManager();

            BankAccountService = new BankAccountService(fileReader, accountNumberManager);
        }
    }
}