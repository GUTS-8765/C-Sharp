using System;
using System.Globalization;
using System.Text;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        Console.Write("Как вас зовут? ");
        string name = (Console.ReadLine() ?? "").Trim();
        if (name.Length == 0) name = "незнакомец";
        Console.WriteLine($"Здравствуйте, {name}!");

        bool running = true;
        while (running)
        {
            Console.WriteLine();
            Console.WriteLine("=== МЕНЮ ===");
            Console.WriteLine("1 — Шаг 1. Знакомство");
            Console.WriteLine("2 — Шаг 2. Байты в КБ/МБ/ГБ");
            Console.WriteLine("3 — Шаг 3. Флешка");
            Console.WriteLine("4 — Шаг 4. Время скачивания");
            Console.WriteLine("5 — Шаг 5. Размер текста");
            Console.WriteLine("6 — Шаг 6. Полный калькулятор (подменю)");
            Console.WriteLine("7 — Шаг 7. Свой расчёт (битрейт видео)");
            Console.WriteLine("0 — Выход");
            Console.Write("Ваш выбор: ");

            string cmd = (Console.ReadLine() ?? "").Trim();

            try
            {
                switch (cmd)
                {
                    case "1": Step1_Greeting.Run(); break;
                    case "2": Step2_Bytes.Run(); break;
                    case "3": Step3_Flash.Run(); break;
                    case "4": Step4_Download.Run(); break;
                    case "5": Step5_Text.Run(); break;
                    case "6": Step6_FullCalculator.Run(); break;
                    case "7": Step7_Custom.Run(); break;
                    case "0":
                        running = false;
                        Console.WriteLine($"До свидания, {name}!");
                        break;
                    default:
                        Console.WriteLine("Неизвестный пункт. Попробуйте снова.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}

// ===== Общие помощники ввода =====
static class Input
{
    public static double ReadDouble(string prompt)
    {
        Console.Write(prompt);
        string s = (Console.ReadLine() ?? "").Replace(',', '.');
        if (!double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out double v))
            throw new FormatException("Это не число.");
        return v;
    }

    public static long ReadLong(string prompt)
    {
        Console.Write(prompt);
        string s = (Console.ReadLine() ?? "").Replace(',', '.');
        if (!long.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out long v))
            throw new FormatException("Это не целое число.");
        return v;
    }

    public static string ReadString(string prompt)
    {
        Console.Write(prompt);
        return (Console.ReadLine() ?? "").Trim();
    }
}

// ===== Шаг 1. Знакомство =====
static class Step1_Greeting
{
    public static void Run()
    {
        Console.Write("Как вас зовут? ");
        string answer = (Console.ReadLine() ?? "").Trim();
        string name = answer.Length == 0 ? "незнакомец" : answer;

        Console.WriteLine($"Здравствуйте, {name}!");
        Console.WriteLine($"Сегодня: {DateTime.Now:dd.MM.yyyy}");
        Console.WriteLine($"В вашем имени {name.Length} символов.");
    }
}

// ===== Шаг 2. Байты → КБ/МБ/ГБ =====
static class Step2_Bytes
{
    public static void Run()
    {
        long bytes = Input.ReadLong("Введите размер в байтах: ");
        if (bytes < 0) { Console.WriteLine("Отрицательное число."); return; }

        Console.WriteLine();
        Console.WriteLine($"{"Единица",-10}{"По 1000",15}{"По 1024",15}");
        Console.WriteLine(new string('-', 40));
        Console.WriteLine($"{"КБ",-10}{bytes / 1000.0,15:F2}{bytes / 1024.0,15:F2}");
        Console.WriteLine($"{"МБ",-10}{bytes / 1_000_000.0,15:F2}{bytes / 1_048_576.0,15:F2}");
        Console.WriteLine($"{"ГБ",-10}{bytes / 1_000_000_000.0,15:F2}{bytes / 1_073_741_824.0,15:F2}");
    }
}

// ===== Шаг 3. Флешка =====
static class Step3_Flash
{
    public static void Run()
    {
        double gb = Input.ReadDouble("Объём накопителя в ГБ: ");
        if (gb <= 0) { Console.WriteLine("Нужно положительное число."); return; }

        long totalBytes = (long)(gb * 1_000_000_000);
        double winGb = totalBytes / 1_073_741_824.0;
        double lostGb = gb - winGb;

        Console.WriteLine();
        Console.WriteLine($"На коробке:      {gb:F2} ГБ");
        Console.WriteLine($"Windows покажет: {winGb:F2} ГБ");
        Console.WriteLine($"Разница:         {lostGb:F2} ГБ ({lostGb / gb * 100:F2} %)");
        Console.WriteLine("Ничего не пропало — просто разные определения гигабайта.");

        const long PhotoBytes = 4L * 1_048_576;
        long photos = totalBytes / PhotoBytes;
        long rem = totalBytes % PhotoBytes;

        Console.WriteLine($"Фото по 4 МБ:    {photos}");
        Console.WriteLine($"Останется:       {rem / 1_048_576.0:F2} МБ");
    }
}

// ===== Шаг 4. Время скачивания =====
static class Step4_Download
{
    public static void Run()
    {
        double fileGb = Input.ReadDouble("Размер файла в ГБ: ");
        double mbit = Input.ReadDouble("Скорость в Мбит/с: ");
        if (fileGb <= 0 || mbit <= 0) { Console.WriteLine("Нужны положительные числа."); return; }

        double seconds = fileGb * 1000.0 * 8.0 / mbit;
        long total = (long)Math.Ceiling(seconds);

        Console.WriteLine();
        Console.WriteLine($"Скорость: {mbit / 8:F2} МБ/с");
        Console.WriteLine($"Время:    {total / 3600} ч {total % 3600 / 60} мин {total % 60} с");
    }
}

// ===== Шаг 5. Размер текста =====
static class Step5_Text
{
    public static void Run()
    {
        Console.Write("Введите строку: ");
        string text = Console.ReadLine() ?? "";

        int bytes = Encoding.UTF8.GetByteCount(text);
        int russian = 0, latin = 0, digits = 0, spaces = 0, punct = 0, other = 0;

        foreach (char c in text)
        {
            bool isRussian = (c >= 'А' && c <= 'я') || c == 'ё' || c == 'Ё';
            bool isLatin = (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
            bool isDigit = char.IsDigit(c);
            bool isSpace = c == ' ';
            bool isPunct = ",.!?;:\"'()-—".IndexOf(c) >= 0;

            if (isRussian) russian++;
            else if (isLatin) latin++;
            else if (isDigit) digits++;
            else if (isSpace) spaces++;
            else if (isPunct) punct++;
            else other++;
        }

        Console.WriteLine();
        Console.WriteLine($"Символов (char):   {text.Length}");
        Console.WriteLine($"Байт в UTF-8:      {bytes}");
        Console.WriteLine($"Русских букв:      {russian}");
        Console.WriteLine($"Латинских букв:    {latin}");
        Console.WriteLine($"Цифр:              {digits}");
        Console.WriteLine($"Пробелов:          {spaces}");
        Console.WriteLine($"Знаков препинания: {punct}");
        Console.WriteLine($"Прочих:            {other}");

        int expected = russian * 2 + latin + digits + spaces + punct + other;
        Console.WriteLine();
        Console.WriteLine($"Ожидаемое по правилу: {expected} байт");
        Console.WriteLine($"Реальное:             {bytes} байт");
        Console.WriteLine(expected == bytes
            ? "Правило выполняется."
            : "Правило не выполняется — вероятно, есть эмодзи (4 байта, 2 char).");
    }
}

// ===== Шаг 6. Полный калькулятор (подменю) =====
static class Step6_FullCalculator
{
    public static void Run()
    {
        bool inside = true;
        while (inside)
        {
            Console.WriteLine();
            Console.WriteLine("--- Шаг 6. Полный калькулятор ---");
            Console.WriteLine("1 — Байты в КБ/МБ/ГБ");
            Console.WriteLine("2 — Флешка");
            Console.WriteLine("3 — Время скачивания");
            Console.WriteLine("4 — Размер текста");
            Console.WriteLine("0 — Назад в главное меню");
            Console.Write("Ваш выбор: ");

            string cmd = (Console.ReadLine() ?? "").Trim();
            try
            {
                switch (cmd)
                {
                    case "1": Step2_Bytes.Run(); break;
                    case "2": Step3_Flash.Run(); break;
                    case "3": Step4_Download.Run(); break;
                    case "4": Step5_Text.Run(); break;
                    case "0": inside = false; break;
                    default: Console.WriteLine("Неизвестный пункт."); break;
                }
            }
            catch (Exception ex) { Console.WriteLine($"Ошибка: {ex.Message}"); }
        }
    }
}

// ===== Шаг 7. Свой расчёт =====
static class Step7_Custom
{
    public static void Run()
    {
        double gb = Input.ReadDouble("Объём накопителя в ГБ: ");
        double mbitPerSec = Input.ReadDouble("Битрейт видео в Мбит/с: ");
        if (gb <= 0 || mbitPerSec <= 0) { Console.WriteLine("Нужны положительные числа."); return; }

        double totalMbits = gb * 1000.0 * 8.0;
        double seconds = totalMbits / mbitPerSec;
        long totalSec = (long)seconds;

        Console.WriteLine();
        Console.WriteLine($"Поместится видео: {totalSec / 3600} ч {totalSec % 3600 / 60} мин {totalSec % 60} с");
    }
}
