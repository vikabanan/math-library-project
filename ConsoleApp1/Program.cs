using MathLibrary;  
using System;
namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ПОЛНАЯ ДЕМОНСТРАЦИЯ ВОЗМОЖНОСТЕЙ MathLibrary");
            Console.WriteLine();

            TestBasicOperations();
            TestPrimeNumbers();
            TestPower();
            TestFactorial();
            TestQuadraticEquations();
            TestEdgeCases();
            TestErrorHandling();

            Console.WriteLine("\nДЕМОНСТРАЦИЯ ЗАВЕРШЕНА");
            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }

        static void TestBasicOperations()
        {
            Console.WriteLine("1. БАЗОВЫЕ АРИФМЕТИЧЕСКИЕ ОПЕРАЦИИ");
            Console.WriteLine();

            double a = 15.0, b = 4.0;
            Console.WriteLine($"   Числа: a = {a}, b = {b}");
            Console.WriteLine($"   Сложение: {a} + {b} = {Calculator.Add(a, b)}");
            Console.WriteLine($"   Вычитание: {a} - {b} = {Calculator.Subtract(a, b)}");
            Console.WriteLine($"   Умножение: {a} * {b} = {Calculator.Multiply(a, b)}");
            Console.WriteLine($"   Деление: {a} / {b} = {Calculator.Divide(a, b):F2}");

            a = -10.5; b = 2.5;
            Console.WriteLine($"\n   Числа: a = {a}, b = {b}");
            Console.WriteLine($"   Сложение: {a} + {b} = {Calculator.Add(a, b)}");
            Console.WriteLine($"   Вычитание: {a} - {b} = {Calculator.Subtract(a, b)}");
            Console.WriteLine($"   Умножение: {a} * {b} = {Calculator.Multiply(a, b)}");
            Console.WriteLine($"   Деление: {a} / {b} = {Calculator.Divide(a, b):F2}");

            Console.WriteLine();
        }

        static void TestPrimeNumbers()
        {
            Console.WriteLine("2. ПРОВЕРКА ЧИСЕЛ НА ПРОСТОТУ");
            Console.WriteLine();

            int[] testNumbers = {
                1, 2, 3, 4, 5, 11, 13, 17, 19, 23,
                29, 31, 37, 41, 43, 47, 97, 100, 121, 997
            };

            Console.WriteLine("   Проверка чисел:");
            for (int i = 0; i < testNumbers.Length; i += 5)
            {
                Console.Write("   ");
                for (int j = i; j < Math.Min(i + 5, testNumbers.Length); j++)
                {
                    bool isPrime = Calculator.IsPrime(testNumbers[j]);
                    Console.Write($"{testNumbers[j],3}: {(isPrime ? "да " : "нет ")}  ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static void TestPower()
        {
            Console.WriteLine("3. ТЕСТИРОВАНИЕ МЕТОДА POWER");
            Console.WriteLine();

            Console.WriteLine("   --- Положительные степени ---");
            TestPowerCase(2, 3, "2³");
            TestPowerCase(5, 2, "5²");
            TestPowerCase(10, 4, "10⁴");

            Console.WriteLine("\n   --- Нулевая степень ---");
            TestPowerCase(2, 0, "2⁰");
            TestPowerCase(100, 0, "100⁰");
            TestPowerCase(-5, 0, "(-5)⁰");

            Console.WriteLine("\n   --- Отрицательные степени ---");
            TestPowerCase(2, -1, "2⁻¹");
            TestPowerCase(4, -2, "4⁻²");
            TestPowerCase(10, -3, "10⁻³");

            Console.WriteLine("\n   --- Дробные степени (корни) ---");
            TestPowerCase(9, 0.5, "√9");
            TestPowerCase(16, 0.25, "⁴√16");
            TestPowerCase(27, 1.0 / 3.0, "∛27");

            Console.WriteLine("\n   --- Степени с отрицательным основанием ---");
            TestPowerCase(-2, 2, "(-2)²");
            TestPowerCase(-2, 3, "(-2)³");
            TestPowerCase(-2, 4, "(-2)⁴");

            Console.WriteLine("\n   --- Степени числа 10 ---");
            for (int i = -3; i <= 3; i++)
            {
                TestPowerCase(10, i, $"10^{i}");
            }

            Console.WriteLine("\n   --- Граничные значения ---");
            TestPowerCase(0, 5, "0⁵");
            TestPowerCase(1, 1000, "1¹⁰⁰⁰");
            TestPowerCase(0.5, 2, "(0.5)²");

            Console.WriteLine();
        }

        static void TestPowerCase(double number, double power, string description)
        {
            try
            {
                double result = Calculator.Power(number, power);
                Console.WriteLine($"   {description,-8} = {result,-12:F6}  ({number}^{power} = {result})");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"   {description,-8} = ОШИБКА: {ex.Message}");
            }
        }

        static void TestFactorial()
        {
            Console.WriteLine("4. ТЕСТИРОВАНИЕ МЕТОДА FACTORIAL");
            Console.WriteLine();

            Console.WriteLine("   --- Факториалы малых чисел ---");
            int[] smallNumbers = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            foreach (int n in smallNumbers)
            {
                TestFactorialCase(n);
            }

            Console.WriteLine("\n   --- Факториалы средних чисел ---");
            int[] mediumNumbers = { 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
            foreach (int n in mediumNumbers)
            {
                TestFactorialCase(n);
            }

            Console.WriteLine("\n   --- Проверка граничных значений ---");
            TestFactorialCase(21);
            TestFactorialCase(-1);
            TestFactorialCase(-5);

            Console.WriteLine();
        }

        static void TestFactorialCase(int n)
        {
            try
            {
                long result = Calculator.Factorial(n);
                Console.WriteLine($"   {n,2}! = {result,20:N0}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"   {n,2}! = ОШИБКА: {ex.Message}");
            }
        }

        static void TestQuadraticEquations()
        {
            Console.WriteLine("5. ТЕСТИРОВАНИЕ МЕТОДА SOLVEQUADRATIC");
            Console.WriteLine();

            Console.WriteLine("   --- Два действительных корня ---");
            TestQuadraticCase(1, -3, 2, "x² - 3x + 2 = 0");
            TestQuadraticCase(1, -5, 6, "x² - 5x + 6 = 0");
            TestQuadraticCase(2, -7, 3, "2x² - 7x + 3 = 0");
            TestQuadraticCase(1, 2, -3, "x² + 2x - 3 = 0");

            Console.WriteLine("\n   --- Один корень (дискриминант = 0) ---");
            TestQuadraticCase(1, -4, 4, "x² - 4x + 4 = 0");
            TestQuadraticCase(4, -4, 1, "4x² - 4x + 1 = 0");
            TestQuadraticCase(1, -6, 9, "x² - 6x + 9 = 0");
            TestQuadraticCase(9, -6, 1, "9x² - 6x + 1 = 0");

            Console.WriteLine("\n   --- Нет действительных корней ---");
            TestQuadraticCase(1, 1, 1, "x² + x + 1 = 0");
            TestQuadraticCase(2, 1, 2, "2x² + x + 2 = 0");
            TestQuadraticCase(1, 0, 1, "x² + 1 = 0");
            TestQuadraticCase(3, 0, 5, "3x² + 5 = 0");

            Console.WriteLine("\n   --- Линейные уравнения ---");
            TestQuadraticCase(0, 2, -4, "2x - 4 = 0");
            TestQuadraticCase(0, -3, 6, "-3x + 6 = 0");
            TestQuadraticCase(0, 5, 0, "5x = 0");
            TestQuadraticCase(0, 1, -1, "x - 1 = 0");

            Console.WriteLine("\n   --- Особые случаи ---");
            TestQuadraticCase(0, 0, 5, "5 = 0");
            TestQuadraticCase(0, 0, 0, "0 = 0");
            TestQuadraticCase(1, 0, 0, "x² = 0");
            TestQuadraticCase(1, 0, -4, "x² - 4 = 0");

            Console.WriteLine("\n   --- Неполные квадратные уравнения ---");
            TestQuadraticCase(1, -2, 0, "x² - 2x = 0");
            TestQuadraticCase(2, 0, -8, "2x² - 8 = 0");
            TestQuadraticCase(3, 0, 0, "3x² = 0");
            TestQuadraticCase(1, 5, 0, "x² + 5x = 0");

            Console.WriteLine();
        }

        static void TestQuadraticCase(double a, double b, double c, string equation)
        {
            try
            {
                Console.Write($"   {equation,-20} => ");

                bool hasRoots = Calculator.SolveQuadratic(a, b, c, out double? x1, out double? x2);

                if (hasRoots)
                {
                    if (x2 == null)
                        Console.WriteLine($"один корень: x = {x1:F3}");
                    else
                        Console.WriteLine($"два корня: x₁ = {x1:F3}, x₂ = {x2:F3}");
                }
                else
                {
                    Console.WriteLine("нет действительных корней");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ОШИБКА: {ex.Message}");
            }
        }

        static void TestEdgeCases()
        {
            Console.WriteLine("6. ТЕСТИРОВАНИЕ ГРАНИЧНЫХ СЛУЧАЕВ");
            Console.WriteLine();

            Console.WriteLine("   --- Максимальные значения ---");
            try { Console.WriteLine($"   MaxInt + MaxInt = {Calculator.Add(int.MaxValue, int.MaxValue)}"); }
            catch (Exception ex) { Console.WriteLine($"   Ошибка: {ex.Message}"); }

            try { Console.WriteLine($"   MinInt - MaxInt = {Calculator.Subtract(int.MinValue, int.MaxValue)}"); }
            catch (Exception ex) { Console.WriteLine($"   Ошибка: {ex.Message}"); }

            Console.WriteLine("\n   --- Проверка специальных значений в Divide ---");
            try { Console.WriteLine($"   1.0 / 0.0 = {Calculator.Divide(1.0, 0.0)}"); }
            catch (Exception ex) { Console.WriteLine($"   1.0 / 0.0 => Ошибка: {ex.Message}"); }

            try { Console.WriteLine($"   0.0 / 0.0 = {Calculator.Divide(0.0, 0.0)}"); }
            catch (Exception ex) { Console.WriteLine($"   0.0 / 0.0 => Ошибка: {ex.Message}"); }

            Console.WriteLine("\n   --- Проверка IsPrime с большими числами ---");
            int[] largeNumbers = { 10007, 10009, 100003, 1000003 };
            foreach (int num in largeNumbers)
            {
                bool isPrime = Calculator.IsPrime(num);
                Console.WriteLine($"   Число {num}: {(isPrime ? "простое" : "не простое")}");
            }

            Console.WriteLine();
        }

        static void TestErrorHandling()
        {
            Console.WriteLine("7. ТЕСТИРОВАНИЕ ОБРАБОТКИ ОШИБОК");
            Console.WriteLine();

            Console.WriteLine("   --- Проверка деления на ноль ---");
            try
            {
                Console.Write("   Calculator.Divide(10, 0) => ");
                Calculator.Divide(10, 0);
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"ПЕРЕХВАЧЕНО: {ex.Message}");
            }

            Console.WriteLine("\n   --- Проверка отрицательного факториала ---");
            try
            {
                Console.Write("   Calculator.Factorial(-5) => ");
                Calculator.Factorial(-5);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"ПЕРЕХВАЧЕНО: {ex.Message}");
            }

            Console.WriteLine("\n   --- Проверка слишком большого факториала ---");
            try
            {
                Console.Write("   Calculator.Factorial(21) => ");
                Calculator.Factorial(21);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"ПЕРЕХВАЧЕНО: {ex.Message}");
            }

            Console.WriteLine("\n   --- Проверка возведения 0 в отрицательную степень ---");
            try
            {
                Console.Write("   Calculator.Power(0, -1) => ");
                Calculator.Power(0, -1);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"ПЕРЕХВАЧЕНО: {ex.Message}");
            }

            Console.WriteLine("\n   --- Проверка возведения отрицательного числа в дробную степень ---");
            try
            {
                Console.Write("   Calculator.Power(-2, 0.5) => ");
                Calculator.Power(-2, 0.5);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"ПЕРЕХВАЧЕНО: {ex.Message}");
            }

            Console.WriteLine();
        }
    }
}

