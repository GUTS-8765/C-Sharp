using System;

namespace ConsoleApp4
{
    internal class SearchTime
    {
        AlphabetSize alphabetSize = new AlphabetSize();

        private const double UniverseAgeInSeconds = 13.8 * 1_000_000_000 * 365.25 * 24 * 3600;
        private const double YearInSeconds = 365.25 * 24 * 3600;

        public const double FastSpeed = 10_000_000_000;
        public const double SlowSpeed = FastSpeed / 1_000_000;

        public double GetSearchTime(string? p, double speed)
        {
            if(string.IsNullOrEmpty(p))
            {
                return 0;
            }

            int size = alphabetSize.GetAlphabetSize(p);
            double totalCombinations = Math.Pow(size, p.Length);

            return (totalCombinations / 2) / speed;
        }

        public string FormatSeconds(double seconds)
        {
            if (seconds < 1) return "Меньше секунды";

            if (seconds >= UniverseAgeInSeconds) return "Дольше возраста Вселенной (более 13.8 миллиардов лет)";
            if (seconds >= YearInSeconds * 1_000_000_000) return $"{seconds / (YearInSeconds * 1_000_000_000):0.##} миллиарда(ов) лет";
            if (seconds >= YearInSeconds * 1_000_000) return $"{seconds / (YearInSeconds * 1_000_000):0.##} миллиона(ов) лет";
            if (seconds >= YearInSeconds * 1000) return $"{seconds / (YearInSeconds * 1000):0.##} тысяч(и) лет";
            if (seconds >= YearInSeconds) return $"{seconds / YearInSeconds:0.##} лет (года)";
            if (seconds >= 24 * 3600) return $"{seconds / (24 * 3600):0.##} дней";
            if (seconds >= 3600) return $"{seconds / 3600:0.##} часов";
            if (seconds >= 60) return $"{seconds / 60:0.##} минут";

            return $"{seconds:0.##} секунд";
        }


        public double CalculateTime(double combinations, double speed) => (combinations / 2) / speed;
    }
}
