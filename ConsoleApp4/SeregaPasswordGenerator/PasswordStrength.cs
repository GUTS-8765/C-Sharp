using System;

namespace ConsoleApp4
{
    internal class PasswordStrength
    {
        AlphabetSize alphabetSize = new AlphabetSize();

        List<string> lowPasswords = new List<string>
        {
            "qwerty", "qwerty123", "123456", "12345678", "1qaz2wsx3edc",
            "111111", "123123", "admin", "password", "112233",
            "123456789", "1234567890", "admin123", "12345", "000000",
            "00000000", "abcde", "abcdef", "gfhjkm123", "zxcvbnm"
        };

        public double GetPasswordStrength(string p)
        {
            foreach(string s in lowPasswords)
            {
                if(p == s)
                {
                    return 0;
                }
            }

            int size = alphabetSize.GetAlphabetSize(p);
            int length = p.Length;

            return length * Math.Log2(size);
        }

        public string GetPasswordStrengthString(double bits)
        {
            if (bits < 28)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                return "Очень слабый";
            }
            else if (bits < 36)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                return "Слабый";
            }
            else if (bits < 60)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                return "Средний";
            }
            else if (bits < 128)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                return "Сильный";
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                return "Очень сильный";
            }
        }
    }
}
