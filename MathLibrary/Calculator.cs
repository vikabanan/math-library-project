// Оптимизируем существующие методы в MathLibrary/Calculator.cs

using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace MathLibrary
{
    public static class Calculator
    {
        #region Кэширование

        private static readonly ConcurrentDictionary<int, bool> PrimeCache =
            new ConcurrentDictionary<int, bool>();

        private static readonly double[] PowerOfTwoCache = new double[1024];

        // Добавлено: объявление FactorialCache
        private static readonly long[] FactorialCache = new long[21];

        static Calculator()
        {
            // Предвычисление факториалов
            FactorialCache[0] = 1;
            for (int i = 1; i <= 20; i++)
            {
                FactorialCache[i] = FactorialCache[i - 1] * i;
            }

            // Предвычисление степеней двойки
            for (int i = 0; i < 1024; i++)
            {
                PowerOfTwoCache[i] = Math.Pow(2, i);
            }
        }

        #endregion

        #region Базовые арифметические операции

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Add(double a, double b) => a + b;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Subtract(double a, double b) => a - b;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Multiply(double a, double b) => a * b;

        public static double Divide(double a, double b)
        {
            ValidateDivisor(b);
            return a / b;
        }

        #endregion

        #region Проверка чисел

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPrime(int number)
        {
            if (number < 2) return false;
            if (number <= 3) return true;
            if ((number & 1) == 0) return false;

            if (PrimeCache.TryGetValue(number, out bool cached))
                return cached;

            bool result = IsPrimeOptimized(number);
            PrimeCache[number] = result;
            return result;
        }

        private static bool IsPrimeOptimized(int number)
        {
            if (number % 3 == 0) return number == 3;

            int limit = (int)Math.Sqrt(number);
            for (int i = 5; i <= limit; i += 6)
            {
                if (number % i == 0 || number % (i + 2) == 0)
                    return false;
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEven(int number) => (number & 1) == 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOdd(int number) => (number & 1) == 1;

        #endregion

        #region Возведение в степень

        public static double Power(double number, int power)
        {
            if (Math.Abs(number - 2.0) < double.Epsilon && power >= 0 && power < 1024)
            {
                return PowerOfTwoCache[power];
            }

            return power == 0 ? 1 : FastPowerIterative(number, power);
        }

        public static double Power(double number, double power)
        {
            ValidatePowerArguments(number, power);
            return Math.Pow(number, power);
        }

        private static double FastPowerIterative(double number, int power)
        {
            double result = 1.0;
            long p = Math.Abs((long)power);
            double baseNum = number;

            while (p > 0)
            {
                if ((p & 1) == 1)
                    result *= baseNum;

                baseNum *= baseNum;
                p >>= 1;
            }

            return power > 0 ? result : 1.0 / result;
        }

        #endregion

        #region Факториал

        public static long Factorial(int n)
        {
            ValidateFactorialArgument(n);
            return FactorialCache[n];
        }

        #endregion

        #region Решение уравнений

        public static bool SolveQuadratic(double a, double b, double c, out double? x1, out double? x2)
        {
            x1 = null;
            x2 = null;

            const double epsilon = double.Epsilon * 100;

            if (Math.Abs(a) < epsilon)
            {
                return SolveLinearOptimized(b, c, out x1);
            }

            double discriminant = b * b - 4 * a * c;

            if (discriminant < -epsilon)
                return false;

            if (Math.Abs(discriminant) < epsilon)
            {
                x1 = -b / (2 * a);
                return true;
            }

            double sqrtD = Math.Sqrt(discriminant);
            double denominator = 2 * a;

            if (b >= 0)
            {
                x1 = (-b - sqrtD) / denominator;
                x2 = (2 * c) / (-b - sqrtD);
            }
            else
            {
                x1 = (-b + sqrtD) / denominator;
                x2 = (2 * c) / (-b + sqrtD);
            }

            if (x1 > x2)
            {
                var temp = x1;
                x1 = x2;
                x2 = temp;
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool SolveLinearOptimized(double b, double c, out double? x)
        {
            x = null;

            if (Math.Abs(b) < double.Epsilon)
                return Math.Abs(c) < double.Epsilon;

            x = -c / b;
            return true;
        }

        #endregion

        #region Расширенная функциональность

        public static double CircleArea(double radius)
        {
            if (radius < 0)
                throw new ArgumentException("Радиус не может быть отрицательным");

            return Math.PI * radius * radius;
        }

        public static double CircleAreaFromDiameter(double diameter)
        {
            if (diameter < 0)
                throw new ArgumentException("Диаметр не может быть отрицательным");

            return CircleArea(diameter / 2);
        }

        public static double CelsiusToFahrenheit(double celsius)
        {
            return (celsius * 9 / 5) + 32;
        }

        public static double FahrenheitToCelsius(double fahrenheit)
        {
            return (fahrenheit - 32) * 5 / 9;
        }

        public static double CelsiusToKelvin(double celsius)
        {
            if (celsius < -273.15)
                throw new ArgumentException("Температура ниже абсолютного нуля");

            return celsius + 273.15;
        }

        public static double KelvinToCelsius(double kelvin)
        {
            if (kelvin < 0)
                throw new ArgumentException("Температура в Кельвинах не может быть отрицательной");

            return kelvin - 273.15;
        }

        public static double Hypotenuse(double a, double b)
        {
            if (a < 0 || b < 0)
                throw new ArgumentException("Катеты не могут быть отрицательными");

            return Math.Sqrt(a * a + b * b);
        }

        public static double Leg(double hypotenuse, double otherLeg)
        {
            if (hypotenuse <= 0)
                throw new ArgumentException("Гипотенуза должна быть положительной");

            if (otherLeg < 0)
                throw new ArgumentException("Катет не может быть отрицательным");

            if (otherLeg >= hypotenuse)
                throw new ArgumentException("Катет не может быть больше или равен гипотенузе");

            return Math.Sqrt(hypotenuse * hypotenuse - otherLeg * otherLeg);
        }

        #endregion

        #region Валидация

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ValidateDivisor(double divisor)
        {
            if (Math.Abs(divisor) < double.Epsilon)
                throw new DivideByZeroException("Деление на ноль невозможно");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ValidateFactorialArgument(int n)
        {
            if (n < 0)
                throw new ArgumentException("Факториал отрицательного числа не определен");

            if (n > 20)
                throw new ArgumentOutOfRangeException(nameof(n), "Максимальное значение: 20");
        }

        private static void ValidatePowerArguments(double number, double power)
        {
            if (double.IsNaN(number) || double.IsNaN(power))
                throw new ArgumentException("Операции с NaN не поддерживаются");

            if (Math.Abs(number) < double.Epsilon && power < 0)
                throw new ArgumentException("Возведение нуля в отрицательную степень не определено");

            if (number < 0 && Math.Abs(power % 1) > double.Epsilon)
                throw new ArgumentException("Возведение отрицательного числа в нецелую степень не определено");
        }

        #endregion
    }
}