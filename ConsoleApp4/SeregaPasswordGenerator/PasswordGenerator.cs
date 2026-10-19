using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ConsoleApp4
{
    internal class PasswordGenerator
    {
        private const string lowercase = "abcdefghijklmnopqrstuvwxyz";
        private const string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string digits = "0123456789";
        private const string specials = "!@#$%^&*()_-+={[}]|:;<,>.?/";

        string totalPool = "";

        List<char> result = new List<char>();

        public string GeneratePassword(int len, bool low, bool up, bool dig, bool spec)
        {
            if (low) AddCharacterGroup(lowercase, ref totalPool, ref result);
            if (up) AddCharacterGroup(uppercase, ref totalPool, ref result);
            if (dig) AddCharacterGroup(digits, ref totalPool, ref result);
            if (spec) AddCharacterGroup(specials, ref totalPool, ref result);

            while (result.Count < len)
            {
                int index = RandomNumberGenerator.GetInt32(0, totalPool.Length);
                result.Add(totalPool[index]);
            }

            for (int i = result.Count - 1; i > 0; i--)
            {
                int j = RandomNumberGenerator.GetInt32(0, i + 1);
                char temp = result[i];
                result[i] = result[j];
                result[j] = temp;
            }

            return new string(result.ToArray());
        }

        private void AddCharacterGroup(string poolCharacters, ref string totalPool, ref List<char> resultList)
        {
            totalPool += poolCharacters;

            int randomIndex = RandomNumberGenerator.GetInt32(0, poolCharacters.Length);
            char mandatoryChar = poolCharacters[randomIndex];

            resultList.Add(mandatoryChar);
        }
    }
}
