using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RegressionTesting
{
    public class DeliveryCalculator
    {
        private const decimal BaseRate = 250m;
        private const decimal UrgentSurcharge = 100m;

        public decimal CalcDelivery(decimal weight, bool isUrgent)
        {
            decimal totalPrice = weight * BaseRate;

            if (isUrgent)
            {
                totalPrice += UrgentSurcharge;
            }

            if (weight > 20)
            {
                totalPrice = totalPrice * 0.95m;
            }

            return totalPrice;
        }
    }

    public class TestResult
    {
        public string Name { get; set; }
        public decimal InputWeight { get; set; }
        public bool InputUrgent { get; set; }
        public decimal Expected { get; set; }
        public decimal Actual { get; set; }
        public bool IsPassed => Expected == Actual;
    }

    public class DeliveryTests
    {
        private readonly DeliveryCalculator _calc = new DeliveryCalculator();
        public List<TestResult> AllTests = new List<TestResult>();

        public void RunAllTests()
        {
            AllTests.Clear();

            RunTest("Обычная доставка 2кг", 2, false, 500m);
            RunTest("Срочная доставка 3кг", 3, true, 850m);
            RunTest("Скидка: Вес 25, без срочности", 25, false, 5937.5m);
            RunTest("Скидка: Вес 3, без срочности", 3, false, 750m);
            RunTest("Скидка: Вес 20 ровно, без срочности", 20, false, 5000m);
            RunTest("Скидка: Вес 25, со срочностью", 25, true, 6032.5m);
        }

        private void RunTest(string name, decimal weight, bool urgent, decimal expected)
        {
            var actual = _calc.CalcDelivery(weight, urgent);
            AllTests.Add(new TestResult
            {
                Name = name,
                InputWeight = weight,
                InputUrgent = urgent,
                Expected = expected,
                Actual = actual
            });
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var tests = new DeliveryTests();

            Console.WriteLine("--- ПУНКТ 2: ОТЧЕТ О ПАДЕНИЯХ ---");
            var oldCalc = new DeliveryCalculatorOld();
            Console.WriteLine($"CalcDelivery(7, false): ожидали 1400, пришло {oldCalc.CalcDelivery(7, false)} - применён новый базовый тариф.");
            Console.WriteLine($"CalcDelivery(7, true): ожидали 1500, пришло {oldCalc.CalcDelivery(7, true)} - изменен тариф.");
            Console.WriteLine();

            Console.WriteLine("--- ПУНКТ 5: ПРОГОН ОБНОВЛЕННОГО ПАКЕТА ---");
            Stopwatch sw = new Stopwatch();
            sw.Start();

            tests.RunAllTests();

            sw.Stop();

            int passedCount = 0;
            foreach (var test in tests.AllTests)
            {
                if (test.IsPassed)
                {
                    passedCount++;
                    Console.WriteLine($"[PASS] {test.Name}");
                }
                else
                {
                    Console.WriteLine($"[FAIL] {test.Name}. Ожидали: {test.Expected}, Пришло: {test.Actual}");
                }
            }

            int totalTests = tests.AllTests.Count;
            Console.WriteLine($"\nВсего проверок: {totalTests} (Из них старых: 2, Новых: 4)");
            Console.WriteLine($"Полный прогон занял: {sw.Elapsed.TotalSeconds:F4} секунд.");

            Console.WriteLine("\n--- ПУНКТ 6: ВЫГОДА ДИСЦИПЛИНЫ ---");
            int manualMinutesPerTest = 3;
            double manualTotalMinutes = totalTests * manualMinutesPerTest;

            double autoSeconds = sw.Elapsed.TotalSeconds;

            Console.WriteLine($"Ручной прогон ({totalTests} тестов * 3 мин) = {manualTotalMinutes} минут.");
            Console.WriteLine($"Автоматический прогон = {autoSeconds:F4} секунд.");
            Console.WriteLine($"\nИТОГ: Полный прогон пакета из {totalTests} проверок идёт {autoSeconds:F4} секунд против {manualTotalMinutes} минут ручного перебора — поэтому он и запускается после каждого изменения.");

            Console.ReadLine();
        }
    }

    public class DeliveryCalculatorOld
    {
        private const decimal OldBaseRate = 200m;
        private const decimal UrgentSurcharge = 100m;

        public decimal CalcDelivery(decimal weight, bool isUrgent)
        {
            decimal totalPrice = weight * OldBaseRate;
            if (isUrgent) totalPrice += UrgentSurcharge;
            return totalPrice;
        }
    }
}