using System;
using System.Collections.Generic;
using MathLibrary;

namespace MathLibraryClient
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Демонстрация работы MathLibrary.dll\n");
            Console.WriteLine("Программа демонстрирует все методы математической библиотеки\n");

            DemonstrateBasicOperations();

            DemonstratePrimeNumbers();

            DemonstratePower();

            DemonstrateFactorial();

            DemonstrateQuadraticEquations();

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }

        static void DemonstrateBasicOperations()
        {
            Console.WriteLine("Базовые арифметические операции");

            double x = 10.5, y = 3.5;

            Console.WriteLine($"Числа: x = {x}, y = {y}");
            Console.WriteLine($"Сложение: {x} + {y} = {Calculator.Add(x, y)}");
            Console.WriteLine($"Вычитание: {x} - {y} = {Calculator.Subtract(x, y)}");
            Console.WriteLine($"Умножение: {x} * {y} = {Calculator.Multiply(x, y)}");
            Console.WriteLine($"Деление: {x} / {y} = {Calculator.Divide(x, y):F2}");

            try
            {
                Console.WriteLine("\nПопытрка деления на ноль:");
                Calculator.Divide(x, 0);
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            Console.WriteLine();
        }

        static void DemonstratePrimeNumbers()
        {
            Console.WriteLine("Проверка чисел на простоту");

            int[] numbers = { 2, 3, 4, 17, 18, 19, 97, 100 };

            foreach (int num in numbers)
            {
                bool isPrime = Calculator.IsPrime(num);
                Console.WriteLine($"Число {num}: {(isPrime ? "простое" : "не простое")}");
            }

            Console.WriteLine();
        }

        static void DemonstratePower()
        {
            Console.WriteLine("Возведение в степень");

            double number = 2;
            double power = 5;

            Console.WriteLine($"{number}^{power} = {Calculator.Power(number, power)}");

            Console.WriteLine($"2^0 = {Calculator.Power(2, 0)}");
            Console.WriteLine($"2^(-1) = {Calculator.Power(2, -1):F3}");
            Console.WriteLine($"9^(0.5) = {Calculator.Power(9, 0.5)}");

            Console.WriteLine();
        }

        static void DemonstrateFactorial()
        {
            Console.WriteLine("Вычисление факториала");

            int[] numbers = { 0, 1, 5, 10 };

            foreach (int n in numbers)
            {
                try
                {
                    Console.WriteLine($"{n}! = {Calculator.Factorial(n)}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Ошибка для числа {n}: {ex.Message}");
                }
            }

            try
            {
                Console.WriteLine("\nПопытка вычисления факториала отрицательного числа:");
                Calculator.Factorial(-5);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            Console.WriteLine();
        }

        static void DemonstrateQuadraticEquations()
        {
            Console.WriteLine("Решение квадратных уравнений ax² + bx + c = 0");

            TestQuadratic(1, -3, 2, "x² - 3x + 2 = 0");

            TestQuadratic(1, -4, 4, "x² - 4x + 4 = 0");

            TestQuadratic(1, 1, 1, "x² + x + 1 = 0");

            TestQuadratic(0, 2, -4, "2x - 4 = 0");

            TestQuadratic(0, 0, 5, "5 = 0");
        }

        static void TestQuadratic(double a, double b, double c, string equation)
        {
            Console.Write($"Уравнение: {equation}");

            bool hasRoots = Calculator.SolveQuadratic(a, b, c, out double? x1, out double? x2);

            if (hasRoots)
            {
                if (x2 == null)
                    Console.WriteLine($" => Один корень: x = {x1:F2}");
                else
                    Console.WriteLine($" => Два корня: x₁ = {x1:F2}, x₂ = {x2:F2}");
            }
            else
            {
                Console.WriteLine(" => Нет действительных корней");
            }
        }

    }
}