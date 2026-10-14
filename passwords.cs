using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

internal class Program
{
    private static byte[] randomBytes;
    private static object validChars;
    const string vaildChars = "a b c d e f g h i j l m n o p q r s t u v w x y z A B C D E F G H I J K L M N O P Q R S T U V W X Y Z 0 1 2 3 4 5 6 7 8 9";

    static void Main(string[] args)
    {
        int lenght = 12;
        string passwords = GeneratePassword(lenght);
        Console.WriteLine(passwords);
    }

    private static string GeneratePassword(int lenght)
    {
        throw new NotImplementedException();
    }

    private static string GeneratePasswords(int lenght)
    {
        byte[] rabdomBytes = new byte[LengthAttribute];
        using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(randomBytes);
        }
        StringBuilder passwords = new StringBuilder(lenght);
        foreach (byte b in rabdomBytes)
        {
            passwords.Append(validChars[b % vaildChars.Length]);
        }
        return passwords.ToString();
    }
}
