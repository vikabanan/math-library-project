using System;

namespace MathLibrary
{
    public class Calculator
    {
        /// <summary>
        /// Складывает два числа.5665
        /// </summary>
        public static double Add(double a, double b)
        {
            return a + b;
        }

        /// <summary>
        /// Вычитает второе число из первого.
        /// </summary>
        public static double Subtract(double a, double b)
        {
            return a - b;
        }

        /// <summary>
        /// Умножает два числа.
        /// </summary>
        public static double Multiply(double a, double b)
        {
            return a * b;
        }

        /// <summary>
        /// Делит первое число на второе.
        /// </summary>
        public static double Divide(double a, double b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException("Делитель не может быть равен нулю.");
            }
            return a / b;
        }

        /// <summary>
        /// Проверяет, является ли число простым.
        /// </summary>
        public static bool IsPrime(int number)
        {
            if (number <= 1)
                return false;

            if (number == 2)
                return true;

            if (number % 2 == 0)
                return false;

            for (int i = 3; i <= Math.Sqrt(number); i += 2)
            {
                if (number % i == 0)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Возводит число в степень.
        /// </summary>
        public static double Power(double number, double power)
        {
            return Math.Pow(number, power);
        }

        /// <summary>
        /// Вычисляет факториал числа.
        /// </summary>
        public static long Factorial(int n)
        {
            if (n < 0)
            {
                throw new ArgumentException("Факториал отрицательного числа не определен.");
            }

            if (n > 20) // Ограничение для типа long
            {
                throw new ArgumentException("Слишком большое число для вычисления факториала. Максимум 20.");
            }

            long result = 1;
            for (int i = 2; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }

        /// <summary>
        /// Решает квадратное уравнение ax² + bx + c = 0.
        /// </summary>
        /// <returns>True, если есть действительные корни</returns>
        public static bool SolveQuadratic(double a, double b, double c, out double? x1, out double? x2)
        {
            x1 = null;
            x2 = null;

            // Проверка на линейное уравнение
            if (a == 0)
            {
                if (b == 0)
                {
                    return false; // Нет решений или бесконечно много
                }
                // Линейное уравнение bx + c = 0
                x1 = -c / b;
                return true;
            }

            double discriminant = b * b - 4 * a * c;

            if (discriminant < 0)
            {
                return false; // Нет действительных корней
            }

            if (Math.Abs(discriminant) < 1e-10) // Дискриминант близок к нулю
            {
                x1 = -b / (2 * a);
                return true;
            }

            x1 = (-b + Math.Sqrt(discriminant)) / (2 * a);
            x2 = (-b - Math.Sqrt(discriminant)) / (2 * a);

            // Сортируем корни для удобства
            if (x1 > x2)
            {
                var temp = x1;
                x1 = x2;
                x2 = temp;
            }

            return true;
        }
    }
}