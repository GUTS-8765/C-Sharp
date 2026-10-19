using ConsoleApp4;

internal class Program()
{
    public static void Main()
    {
        AlphabetSize alphabetSize = new AlphabetSize();
        PasswordStrength passwordStrength = new PasswordStrength();
        SearchTime searchTime = new SearchTime();
        PasswordGenerator passwordGenerator = new PasswordGenerator();
        Passphrase passphrase = new Passphrase();

        while(true)
        {
            Console.Clear();

            WriteColored("ConsoleApp4", ConsoleColor.Blue);

            Console.WriteLine("\n1 - Получить размер алфавита" +
                "\n2 - Стойкость в битах и оценка пароля" +
                "\n3 - Время перебора пароля" +
                "\n4 - Генератор паролей" +
                "\n5 - Генератор парольной фразы" +
                "\n\n0 - Выход\n");

            string? choice = ReadColoredLine(ConsoleColor.Green);

            switch (choice)
            {
                case "1":
                    SetTitle("Получить размер алфавита", ConsoleColor.Blue);
                    Console.WriteLine("\nВведи пароль:");

                    string? alphabetPassword = ReadColoredLine(ConsoleColor.Green);

                    if (string.IsNullOrEmpty(alphabetPassword))
                    {
                        WriteColored("Пароль не может быть пустым!", ConsoleColor.DarkRed);
                        Continue();
                        break;
                    }

                    int passwordAlphabetSize = alphabetSize.GetAlphabetSize(alphabetPassword);

                    WriteColored($"\nРазмер алфавита: {passwordAlphabetSize}", ConsoleColor.Green);
                    Continue();

                    break;

                case "2":
                    SetTitle("Стойкость в битах и оценка пароля", ConsoleColor.Blue);
                    Console.WriteLine("\nВведи пароль:");

                    string? strengthPassword = ReadColoredLine(ConsoleColor.Green);

                    if (string.IsNullOrEmpty(strengthPassword))
                    {
                        WriteColored("Пароль не может быть пустым!", ConsoleColor.DarkRed);
                        Continue();
                        break;
                    }

                    double strength = passwordStrength.GetPasswordStrength(strengthPassword);

                    if (!(strength <= 0))
                    {
                        Console.Write("\nСила пароля: ");
                        Console.Write($"{strength:0.##}");
                    }

                    Console.WriteLine("\n"+passwordStrength.GetPasswordStrengthString(strength));
                    Console.ResetColor();

                    Continue();

                    break;

                case "3":
                    SetTitle("Время перебора пароля", ConsoleColor.Blue);
                    Console.WriteLine("\nВведи пароль:");

                    string? searchTimePassword = ReadColoredLine(ConsoleColor.Green);

                    if (string.IsNullOrEmpty(searchTimePassword))
                    {
                        WriteColored("Пароль не может быть пустым!", ConsoleColor.DarkRed);
                        Continue();
                        break;
                    }

                    double fastTime = searchTime.GetSearchTime(searchTimePassword, SearchTime.FastSpeed);
                    double slowTime = searchTime.GetSearchTime(searchTimePassword, SearchTime.SlowSpeed);

                    Console.WriteLine($"\nДлина пароля: {searchTimePassword.Length} символов.");
                    Console.WriteLine($"Используемый алфавит: {alphabetSize.GetAlphabetSize(searchTimePassword)}");
                    Console.WriteLine($"-----------------------------------------");
                    Console.WriteLine($"[Быстрый перебор]  Время подбора: {searchTime.FormatSeconds(fastTime)}");
                    Console.WriteLine($"[Медленный хэш]   Время подбора: {searchTime.FormatSeconds(slowTime)}");

                    Continue();

                    break;

                case "4":
                    int len = 0;

                    SetTitle("Генератор паролей", ConsoleColor.Blue);

                    while(true)
                    {
                        Console.WriteLine("\nВведи длину пароля (от 8 до 64):");

                        string? passwordLength = ReadColoredLine(ConsoleColor.Green);

                        if (!int.TryParse(passwordLength, out len))
                        {
                            WriteColored("Введи корректное число!\n", ConsoleColor.DarkRed);
                            continue;
                        }

                        if (len < 8 || len > 64)
                        {
                            WriteColored("Введи число в диапазоне от 8 до 64!\n", ConsoleColor.DarkRed);
                            continue;
                        }

                        break;
                    }

                    bool useLow = false;
                    bool useUp = false;
                    bool useDig = false;
                    bool useSpec = false;

                    Question("\nИспользовать строчные буквы (a-z)? [Y/N]", ref useLow);
                    Question("\nИспользовать заглавные буквы (A-Z)? [Y/N]", ref useUp);
                    Question("\nИспользовать цифры (0-9)? [Y/N]", ref useDig);
                    Question("\nИспользовать спецсимволы (!@#$)? [Y/N]", ref useSpec);

                    if(!useLow && !useUp && !useDig && !useSpec)
                    {
                        WriteColored("\nНужно выбрать хотя бы одну группу символов!", ConsoleColor.DarkRed);
                    }
                    else
                    {
                        string pass = passwordGenerator.GeneratePassword(len, useLow, useUp, useDig, useSpec);

                        WriteColored("\n" + pass, ConsoleColor.Green);
                    }

                    Continue();

                    break;

                case "5":
                    int passphraseLength = 0;

                    SetTitle("Генератор парольной фразы", ConsoleColor.Blue);

                    while (true)
                    {
                        Console.WriteLine("\nВведи количество слов в парольной фразе:");

                        string? passphraseWordsCount = ReadColoredLine(ConsoleColor.Green);

                        if (!int.TryParse(passphraseWordsCount, out passphraseLength))
                        {
                            WriteColored("\nВведи корректное число!", ConsoleColor.DarkRed);
                            continue;
                        }

                        if (passphraseLength < 1 || passphraseLength > 100)
                        {
                            WriteColored("\nВведи число в диапазоне от 1 до 100!", ConsoleColor.DarkRed);
                            continue;
                        }

                        break;
                    }

                    WriteColored($"\nПарольная фраза: {passphrase.GetRandomPhrase(passphraseLength)}", ConsoleColor.Green);
                    Console.WriteLine($"\nСтойкость: {passphrase.GetPassphraseStrength(passphraseLength):0.##}");

                    Continue();

                    break;

                case "0":
                    return;
            }
        }
    }

    static void WriteColored(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ResetColor();
    }

    static void Continue()
    {
        Console.WriteLine("\nENTER - Продолжить");
        Console.ReadLine();
    }

    static void SetTitle(string title, ConsoleColor color)
    {
        Console.Clear();
        WriteColored(title, color);
    }

    static string? ReadColoredLine(ConsoleColor color)
    {
        Console.ForegroundColor = color;
        string? input = Console.ReadLine();
        Console.ResetColor();

        return input;
    }

    static void Question(string text, ref bool tumbler)
    {
        Console.WriteLine(text);
        tumbler = Console.ReadKey(true).Key == ConsoleKey.Y;

        WriteColored(tumbler ? "\nПринято" : "\nОтклонено", tumbler ? ConsoleColor.Green : ConsoleColor.DarkRed);
    }
}

