namespace Bank.OCR.Infrastructure.Utilities.Account
{
    public class CheckSumValidator
    {
        public bool CheckSumIsValid(string accountNumber)
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