namespace Bank.OCR.Infrastructure.Utilities.Account
{
    using System;

    public interface ICheckSumValidator
    {
        string[] ValidateAccountNumbers(string[] accountNumbers);
    }

    public class CheckSumValidator : ICheckSumValidator
    {
        public string[] ValidateAccountNumbers(string[] accountNumbers)
        {
            var result = new string[accountNumbers.Length];

            for (int i = 0; i < accountNumbers.Length; i++)
            {
                var accNo = accountNumbers[i];

                result[i] = accNo + (accNo.Contains('?') ? " ILL" : CheckSumIsValid(accNo) ? string.Empty : " ERR");
            }

            return result;
        }

        private bool CheckSumIsValid(string accountNumber)
        {
            var acArray = accountNumber.ToCharArray();
            var total = 0;

            for (int i = 0; i < acArray.Length; i++)
            {
                var position = i + 1;
                var digitChar = acArray[acArray.Length - position];

                if (!char.IsDigit(digitChar))
                {
                    return false;
                }

                var digit = (int)char.GetNumericValue(digitChar);
                total += digit * position;
            }

            return total % Constants.CheckSumValidator == 0;
        }
    }
}