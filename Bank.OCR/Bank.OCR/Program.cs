namespace Bank.OCR
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            var compositionRoot = new CompositionRoot();

            var bankAccountService = compositionRoot.BankAccountService;

            while (true)
            {
                Console.WriteLine("Please enter filename or 'Quit' to exit:");

                var filaname = Console.ReadLine();

                if (filaname.ToLower() == "quit")
                    return;

                var accountNumbers = bankAccountService.ProcessFile(filaname);

                if (accountNumbers.Length > 0)
                {
                    for (int i = 0; i < accountNumbers.Length; i++)
                    {
                        Console.WriteLine(accountNumbers[i]);
                    }
                }

                Console.WriteLine();
            }
        }
    }
}
