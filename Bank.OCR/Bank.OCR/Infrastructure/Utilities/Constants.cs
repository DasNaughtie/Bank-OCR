namespace Bank.OCR.Infrastructure.Utilities
{
    public class Constants
    {
        public static string[] Zero = new[] { " _ ", "| |", "|_|" };
        public static string[] One = new[] { "   ", "  |", "  |" };
        public static string[] Two = new[] { " _ ", " _|", "|_ " };
        public static string[] Three = new[] { " _ ", " _|", " _|" };
        public static string[] Four = new[] { "   ", "|_|", "  |" };
        public static string[] Five = new[] { " _ ", "|_ ", " _|" };
        public static string[] Six = new[] { " _ ", "|_ ", "|_|" };
        public static string[] Seven = new[] { " _ ", "  |", "  |" };
        public static string[] Eight = new[] { " _ ", "|_|", "|_|" };
        public static string[] Nine = new[] { " _ ", "|_|", " _|" };

        public const int RowsPerAccountEntry = 4;
        public const int RowsPerDigit = 3;
        public const int CheckSumValidator = 11;
        public const int MaximumAccountEntries = 500;
    }
}