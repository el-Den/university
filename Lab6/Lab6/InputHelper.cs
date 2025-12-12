using System;
using System.Windows.Forms;

namespace Lab6WinForms.Core
{
    public static class InputHelper
    {
        public static bool TryParseInt(string s, out int value)
        {
            return int.TryParse(s?.Trim(), out value);
        }

        public static bool TryParseDouble(string s, out double value)
        {
            return double.TryParse(s?.Trim(), out value);
        }

        public static int ReadIntFromTextBox(TextBox tb, int min = int.MinValue, int max = int.MaxValue)
        {
            if (!TryParseInt(tb.Text, out int v)) throw new ArgumentException("Неверный ввод целого числа");
            if (v < min || v > max) throw new ArgumentOutOfRangeException($"Число должно быть в пределах {min}..{max}");
            return v;
        }
    }
}
