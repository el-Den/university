using System;

namespace Lab6WinForms.Core
{
    /// <summary>Вычисление функции f = round(log_b( sqrt(b + sin(a)) / cos^3(a) ), 2).</summary>
    public static class FunctionGame
    {
        public static double Calculate(double a, double b)
        {
            double under = b + Math.Sin(a);
            if (b <= 0 || b == 1 || Math.Cos(a) == 0 || under <= 0) return double.NaN;
            double value = Math.Sqrt(under) / Math.Pow(Math.Cos(a), 3);
            if (value <= 0) return double.NaN;
            return Math.Round(Math.Log(value, b), 2);
        }
    }
}
