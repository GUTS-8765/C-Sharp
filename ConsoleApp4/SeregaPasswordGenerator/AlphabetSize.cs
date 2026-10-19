using System;

namespace ConsoleApp4
{
    internal class AlphabetSize
    {
        public int GetAlphabetSize(string? p)
        {
            if (string.IsNullOrEmpty(p))
            {
                return 0;
            }

            bool hasLowerAscii = false;
            bool hasUpperAscii = false;
            bool hasDigits = false;
            bool hasOtherAscii = false;
            bool hasNonAscii = false;

            foreach (char c in p)
            {
                if (c <= 127)
                {
                    if (c >= 'a' && c <= 'z')
                    {
                        hasLowerAscii = true;
                    }
                    else if (c >= 'A' && c <= 'Z')
                    {
                        hasUpperAscii = true;
                    }
                    else if (c >= '0' && c <= '9')
                    {
                        hasDigits = true;
                    }
                    else
                    {
                        hasOtherAscii = true;
                    }
                }
                else
                {
                    hasNonAscii = true;
                }
            }

            int totalSize = 0;

            if (hasLowerAscii) totalSize += 26;
            if (hasUpperAscii) totalSize += 26;
            if (hasDigits) totalSize += 10;
            if (hasOtherAscii) totalSize += 33;
            if (hasNonAscii) totalSize += 66;

            return totalSize;
        }
    
    }
}
