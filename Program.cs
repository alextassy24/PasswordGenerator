using System.Text;
using static System.Console;

namespace PasswordGenerator
{
    public class Program
    {
        private static readonly string NumberPattern = "0123456789";
        private static readonly string LowercaseLettersPattern = "abcdefghijklmnopqrstuvwxyz";
        private static readonly string UppercaseLettersPattern = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly string SpecialCharactersPattern = "!@#$%^&*()-_=+[{]};:'\",<.>/?";

        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                ShowHelp();
                return;
            }

            int length = 8;
            bool hasNumbers = false,
                hasLower = false,
                hasUpper = false,
                hasSpecial = false;

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-len"
                        when i + 1 < args.Length
                            && int.TryParse(args[++i], out length)
                            && length > 0:
                        break;
                    case "-n":
                        hasNumbers = true;
                        break;
                    case "-l":
                        hasLower = true;
                        break;
                    case "-u":
                        hasUpper = true;
                        break;
                    case "-s":
                        hasSpecial = true;
                        break;
                    default:
                        ShowHelp();
                        return;
                }
            }

            if (!hasNumbers && !hasLower && !hasUpper && !hasSpecial)
            {
                WriteLine("Specify at least one character type: -n, -l, -u, -s");
                return;
            }

            string password = GeneratePassword(length, hasNumbers, hasLower, hasUpper, hasSpecial);
            WriteLine(password);
        }

        private static void ShowHelp() =>
            WriteLine("Usage: passGenerator -len <length> [-n] [-l] [-u] [-s]");

        private static string GeneratePassword(
            int length,
            bool hasNumbers,
            bool hasLower,
            bool hasUpper,
            bool hasSpecial
        )
        {
            if (length <= 0)
                return string.Empty;

            StringBuilder sb = new();
            Random random = new();

            string[] patterns =
            {
                hasNumbers ? NumberPattern : "",
                hasLower ? LowercaseLettersPattern : "",
                hasUpper ? UppercaseLettersPattern : "",
                hasSpecial ? SpecialCharactersPattern : ""
            };

            for (int i = 0; i < length; i++)
            {
                string pattern = patterns[random.Next(patterns.Length)];
                if (string.IsNullOrEmpty(pattern))
                {
                    i--;
                    continue;
                }
                sb.Append(pattern[random.Next(pattern.Length)]);
            }

            return sb.ToString();
        }
    }
}
