using System;

class Program
{
    
    static double F(double x) => x * x * x + x - 3.0;

    
    static double Phi(double x) => Math.Pow(3.0 - x, 1.0 / 3.0);

    static double Log2(double x) => Math.Log(x) / Math.Log(2.0);

    static string Sign(double y) => y > 0 ? "+" : y < 0 ? "-" : "0";

    static void Main()
    {
        double a0 = 1.0;
        double b0 = 1.5;
        double eps = 1e-6;

        Console.WriteLine("1. Локализация f(x) = x^3 + x - 3 на [0; 2] с шагом 0.5");
        Console.WriteLine(" x        f(x)        знак");
        for (int i = 0; i <= 4; i++)
        {
            double x = i * 0.5;
            double y = F(x);
            Console.WriteLine($"{x,4:F1}  {y,10:F4}  {Sign(y),4}");
        }

        Console.WriteLine();
        Console.WriteLine("Смена знака: между x = 1.0 и x = 1.5.");
        Console.WriteLine($"f(1) * f(1.5) = {F(1.0) * F(1.5):F4} < 0");
        Console.WriteLine("Отрезок локализации: [1; 1.5]");
        Console.WriteLine();

        Console.WriteLine("2. Три шага бисекции на [1; 1.5]");
        double a = a0;
        double b = b0;

        Console.WriteLine("Шаг\t a\t m\t b\t f(m)\t новая половина");

        for (int step = 1; step <= 3; step++)
        {
            double aOld = a;
            double bOld = b;

            double m = (a + b) / 2.0;
            double fm = F(m);

            string half;
            if (F(aOld) * fm <= 0)
            {
                b = m;
                half = "левая: b = m";
            }
            else
            {
                a = m;
                half = "правая: a = m";
            }

            Console.WriteLine(
                $"{step}\t{aOld:F4}\t{m:F4}\t{bOld:F4}\t{fm:F4}\t{half}");
        }

        double midAfter3 = (a + b) / 2.0;

        Console.WriteLine($"Итоговый отрезок после 3 шагов: [{a:F4}; {b:F4}]");
        Console.WriteLine($"Середина: {midAfter3:F5}, f(середина) = {F(midAfter3):F6}");
        Console.WriteLine();

        Console.WriteLine("3. Оценка числа шагов бисекции для [1; 1.5]");
        double length = b0 - a0;

        double n01Exact = Log2(length / 0.01);
        int n01 = (int)Math.Ceiling(n01Exact);

        double n1e6Exact = Log2(length / 1e-6);
        int n1e6 = (int)Math.Ceiling(n1e6Exact);

        Console.WriteLine($"eps = 0.01:   n > log2({length}/0.01) = {n01Exact:F3} => {n01} шагов");
        Console.WriteLine($"eps = 1e-6:   n > log2({length}/1e-6) = {n1e6Exact:F3} => {n1e6} шагов");
        Console.WriteLine();

        Console.WriteLine("4. Проверка середины итогового отрезка после 3 шагов");
        Console.WriteLine($"f({midAfter3:F5}) = {F(midAfter3):F6}");
        Console.WriteLine($"f(1.0) = {F(1.0):F4}");
        Console.WriteLine($"f(1.5) = {F(1.5):F4}");
        Console.WriteLine();

        var bisectionResult = Bisection(a0, b0, eps);
        Console.WriteLine("Метод половинного деления до eps = 1e-6");
        Console.WriteLine($"x = {bisectionResult.x:F7}, f(x) = {F(bisectionResult.x):E3}, шагов = {bisectionResult.steps}");
        Console.WriteLine();

        var iterationResult = SimpleIteration(1.0, eps);
        Console.WriteLine("Метод простых итераций: phi(x) = (3 - x)^(1/3), eps = 1e-6");
        Console.WriteLine($"x = {iterationResult.x:F7}, f(x) = {F(iterationResult.x):E3}, итераций = {iterationResult.steps}");

        double phiDerivative = -1.0 / (3.0 * Math.Pow(3.0 - iterationResult.x, 2.0 / 3.0));
        Console.WriteLine($"phi'(x) около корня = {phiDerivative:F4}, |phi'| < 1 — сходимость есть");
    }

    static (double x, int steps) Bisection(double a, double b, double eps)
    {
        if (F(a) * F(b) > 0)
            throw new ArgumentException("На концах отрезка одинаковые знаки.");

        int steps = 0;

        while (b - a > eps)
        {
            double m = (a + b) / 2.0;

            if (F(a) * F(m) <= 0)
                b = m;
            else
                a = m;

            steps++;

            if (steps > 100000)
                throw new InvalidOperationException("Слишком много шагов бисекции.");
        }

        return ((a + b) / 2.0, steps);
    }

    static (double x, int steps) SimpleIteration(double x0, double eps)
    {
        double x = x0;
        double prev;
        int steps = 0;

        do
        {
            prev = x;
            x = Phi(x);
            steps++;

            if (steps > 100000)
                throw new InvalidOperationException("Итерации не сходятся.");
        }
        while (Math.Abs(x - prev) > eps);

        return (x, steps);
    }
}