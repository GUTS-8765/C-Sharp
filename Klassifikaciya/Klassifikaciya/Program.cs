using System;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        string[] dNames = {
            "вес 3, обычная", "вес 5, обычная", "вес 6, обычная",
            "вес 20, обычная", "вес 21, обычная", "вес 7, срочная",
            "вес 25, срочная", "вес 0, обычная", "вес -5, срочная"
        };
        int[] dWeights = { 3, 5, 6, 20, 21, 7, 25, 0, -5 };
        bool[] dExpress = { false, false, false, false, false, true, true, false, true };
        int[] dExpect = { 200, 200, 300, 300, 550, 600, 1100, -1, -1 };

        for (int i = 0; i < dNames.Length; i++)
            Check(dNames[i], CalcDelivery(dWeights[i], dExpress[i]), dExpect[i]);

        string[] pNames = {
            "Abcdef12", "abcdefG1", "abcdefg1", "Abcdefgh", "Abc1", "12345678"
        };
        string[] pPasswords = {
            "Abcdef12", "abcdefG1", "abcdefg1", "Abcdefgh", "Abc1", "12345678"
        };
        string[] pExpect = {
            "ОК", "ОК", "Нет заглавной буквы", "Нет цифры", "Короткий пароль", "Нет заглавной буквы"
        };

        for (int i = 0; i < pNames.Length; i++)
            Check(pNames[i], CheckPassword(pPasswords[i]), pExpect[i]);

        try
        {
            string got = CheckPassword(null);
            Console.WriteLine("пароль null: получено \"" + got + "\"");
        }
        catch (Exception ex)
        {
            Console.WriteLine("пароль null: АВАРИЙНОЕ ЗАВЕРШЕНИЕ (" + ex.GetType().Name + ")");
        }

        string[] gNames = {
            "100", "85", "84", "70", "69", "50", "49", "0", "-1", "101"
        };
        int[] gScores = { 100, 85, 84, 70, 69, 50, 49, 0, -1, 101 };
        string[] gExpect = {
            "5", "5", "4", "4", "3", "3", "2", "2", "Ошибка", "Ошибка"
        };

        for (int i = 0; i < gNames.Length; i++)
            Check("оценка " + gNames[i], Grade(gScores[i]), gExpect[i]);
    }

    static void Check(string name, int got, int expected)
    {
        if (got == expected)
            Console.WriteLine(name + ": OK");
        else
            Console.WriteLine(name + ": РАСХОЖДЕНИЕ, ожидалось " + expected + ", получено " + got);
    }

    static void Check(string name, string got, string expected)
    {
        if (got == expected)
            Console.WriteLine(name + ": OK");
        else
            Console.WriteLine(name + ": РАСХОЖДЕНИЕ, ожидалось \"" + expected + "\", получено \"" + got + "\"");
    }


    static int CalcDelivery(int weightKg, bool express)
    {
        int price = 200;
        if (weightKg > 5) price = price + 100;
        if (weightKg > 20) price = price + 250;
        if (express) price = price * 2;
        return price;
    }

    static string CheckPassword(string password)
    {
        if (password.Length < 8) return "Короткий пароль";

        bool hasDigit = false;
        for (int i = 0; i < password.Length; i++)
            if (char.IsDigit(password[i])) hasDigit = true;

        if (!hasDigit) return "Нет цифры";
        if (char.IsUpper(password[0])) return "ОК";
        return "Нет заглавной буквы";
    }

    static string Grade(int score)
    {
        if (score < 0 || score > 100) return "Ошибка";
        if (score > 85) return "5";
        if (score >= 70) return "4";
        if (score >= 50) return "3";
        return "2";
    }
}