using System;
using System.Collections.Generic;

namespace MathLibrary
{
    /// <summary>
    /// Предоставляет набор математических операций и алгоритмов
    /// </summary>
    public static class Calculator
    {
        #region Базовые арифметические операции

        /// <summary>
        /// Складывает два числа
        /// </summary>
        public static double Add(double a, double b) => a + b;

        /// <summary>
        /// Вычитает второе число из первого
        /// </summary>
        public static double Subtract(double a, double b) => a - b;

        /// <summary>
        /// Умножает два числа
        /// </summary>
        public static double Multiply(double a, double b) => a * b;

        /// <summary>
        /// Делит первое число на второе с проверкой на ноль
        /// </summary>
        public static double Divide(double a, double b)
        {
            ValidateDivisor(b);
            return a / b;
        }

        #endregion

        #region Проверка чисел

        /// <summary>
        /// Оптимизированная проверка числа на простоту
        /// </summary>
        private static Dictionary<int, bool> _primeCache = new Dictionary<int, bool>();

        public static bool IsPrime(int number)
        {
            if (_primeCache.TryGetValue(number, out bool cachedResult))
                return cachedResult;

            bool result = IsPrimeInternal(number);
            _primeCache[number] = result;
            return result;
        }

        private static bool IsPrimeInternal(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            int limit = (int)Math.Sqrt(number);

            for (int i = 3; i <= limit; i += 2)
            {
                if ((number % i) == 0)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Быстрая проверка четности числа
        /// </summary>
        public static bool IsEven(int number) => (number & 1) == 0;

        /// <summary>
        /// Быстрая проверка нечетности числа
        /// </summary>
        public static bool IsOdd(int number) => (number & 1) == 1;

        #endregion

        #region Возведение в степень

        /// <summary>
        /// Быстрое возведение в степень (бинарный алгоритм)
        /// </summary>
        public static double Power(double number, int power)
        {
            if (power == 0) return 1;
            if (power == 1) return number;

            double result = FastPower(number, Math.Abs(power));

            return power > 0 ? result : 1.0 / result;
        }

        private static double FastPower(double number, int power)
        {
            if (power == 0) return 1;
            if (power == 1) return number;

            double half = FastPower(number, power / 2);

            if ((power & 1) == 0)
                return half * half;
            else
                return half * half * number;
        }

        /// <summary>
        /// Возведение в степень (общий случай)
        /// </summary>
        public static double Power(double number, double power)
        {
            ValidatePowerArguments(number, power);
            return Math.Pow(number, power);
        }

        #endregion

        #region Факториал

        /// <summary>
        /// Кэш для предвычисленных факториалов
        /// </summary>
        private static readonly long[] FactorialCache = new long[21];

        static Calculator()
        {
            FactorialCache[0] = 1;
            for (int i = 1; i <= 20; i++)
            {
                FactorialCache[i] = FactorialCache[i - 1] * i;
            }
        }

        /// <summary>
        /// Оптимизированное вычисление факториала с использованием кэша
        /// </summary>
        public static long Factorial(int n)
        {
            ValidateFactorialArgument(n);
            return FactorialCache[n];
        }

        #endregion

        #region Решение уравнений

        /// <summary>
        /// Решение квадратного уравнения с оптимизированным вычислением
        /// </summary>
        public static bool SolveQuadratic(double a, double b, double c, out double? x1, out double? x2)
        {
            x1 = null;
            x2 = null;

            ValidateQuadraticArguments(a, b, c);

            const double epsilon = 1e-12;

            if (Math.Abs(a) < epsilon)
            {
                return SolveLinear(b, c, out x1);
            }

            double discriminant = CalculateDiscriminant(a, b, c);

            if (discriminant < -epsilon)
                return false;

            if (Math.Abs(discriminant) < epsilon)
            {
                x1 = -b / (2 * a);
                return true;
            }

            // ИСПРАВЛЕНО: передаем c в метод
            return SolveWithStableFormula(a, b, c, discriminant, out x1, out x2);
        }

        // ИСПРАВЛЕНО: добавлен параметр c
        private static bool SolveWithStableFormula(double a, double b, double c, double discriminant, out double? x1, out double? x2)
        {
            double sqrtD = Math.Sqrt(discriminant);

            // Стабильная формула для уменьшения погрешности
            if (b >= 0)
            {
                x1 = (-b - sqrtD) / (2 * a);
                x2 = (2 * c) / (-b - sqrtD);
            }
            else
            {
                x1 = (-b + sqrtD) / (2 * a);
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

        private static bool SolveLinear(double b, double c, out double? x)
        {
            x = null;

            if (Math.Abs(b) < double.Epsilon)
            {
                return Math.Abs(c) < double.Epsilon;
            }

            x = -c / b;
            return true;
        }

        private static double CalculateDiscriminant(double a, double b, double c)
        {
            double bSquared = b * b;
            double fourAC = 4 * a * c;

            return bSquared - fourAC;
        }

        #endregion

        #region Валидация

        private static void ValidateDivisor(double divisor)
        {
            if (Math.Abs(divisor) < double.Epsilon)
            {
                throw new DivideByZeroException("Деление на ноль невозможно");
            }
        }

        private static void ValidateFactorialArgument(int n)
        {
            if (n < 0)
            {
                throw new ArgumentException(
                    $"Факториал отрицательного числа ({n}) не определен. " +
                    "Факториал определен только для неотрицательных целых чисел.");
            }

            if (n > 20)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(n),
                    $"Слишком большое число для вычисления факториала. " +
                    $"Максимально допустимое значение: 20. Получено: {n}.");
            }
        }

        private static void ValidatePowerArguments(double number, double power)
        {
            if (double.IsNaN(number) || double.IsNaN(power))
            {
                throw new ArgumentException("Операции с NaN не поддерживаются.");
            }

            if (Math.Abs(number) < double.Epsilon && power < 0)
            {
                throw new ArgumentException("Возведение нуля в отрицательную степень не определено.");
            }

            if (number < 0 && Math.Abs(power % 1) > double.Epsilon)
            {
                throw new ArgumentException(
                    "Возведение отрицательного числа в нецелую степень " +
                    "не определено в действительных числах.");
            }
        }

        private static void ValidateQuadraticArguments(double a, double b, double c)
        {
            if (double.IsNaN(a) || double.IsNaN(b) || double.IsNaN(c))
            {
                throw new ArgumentException("Коэффициенты уравнения не могут быть NaN.");
            }

            if (double.IsInfinity(a) || double.IsInfinity(b) || double.IsInfinity(c))
            {
                throw new ArgumentException("Коэффициенты уравнения не могут быть бесконечностью.");
            }
        }

        #endregion
    }
}