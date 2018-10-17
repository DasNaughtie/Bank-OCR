namespace Bank.OCR.Infrastructure.Utilities.Account
{
    public interface IAccountNumberManager
    {
        string ExtractAccountNumberFrom(string[] accountEntry);
    }

    public class AccountNumberManager : IAccountNumberManager
    {
        private const int ColumnsPerDigit = 3;
        private const int AccountNumberLength = 9;

        public string ExtractAccountNumberFrom(string[] accountEntry)
        {
            if (accountEntry == null || accountEntry.Length == 0)
                return string.Empty;

            var accountNumber = string.Empty;

            for (int i = 0; i < AccountNumberLength; i++)
            {
                var digit = new string[ColumnsPerDigit];

                var startingPostion = i * ColumnsPerDigit;

                digit[0] = accountEntry[0].Substring(startingPostion, ColumnsPerDigit);
                digit[1] = accountEntry[1].Substring(startingPostion, ColumnsPerDigit);
                digit[2] = accountEntry[2].Substring(startingPostion, ColumnsPerDigit);

                accountNumber += ConvertToDigit(digit);
            }

            return accountNumber;
        }

        private string ConvertToDigit(string[] digit)
        {
            if (IsEqual(Constants.Zero, digit)) return "0";
            if (IsEqual(Constants.One, digit)) return "1";
            if (IsEqual(Constants.Two, digit)) return "2";
            if (IsEqual(Constants.Three, digit)) return "3";
            if (IsEqual(Constants.Four, digit)) return "4";
            if (IsEqual(Constants.Five, digit)) return "5";
            if (IsEqual(Constants.Six, digit)) return "6";
            if (IsEqual(Constants.Seven, digit)) return "7";
            if (IsEqual(Constants.Eight, digit)) return "8";
            if (IsEqual(Constants.Nine, digit)) return "9";

            return "?";
        }

        private static bool IsEqual(string[] array1, string[] array2)
        {
            if (array1.Length == array2.Length)
            {
                for (int i = 0; i < array1.Length; i++)
                {
                    if (array1[i] != array2[i])
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }
    }
}