using System;

namespace с_
{
    public class ScreenPanel
    {
        public int LeftX { get; set; }
        public int TopY { get; set; }
        public int RightX { get; set; }
        public int BottomY { get; set; }
        public string BackgroundColor { get; set; }

        // Конструктор за замовчуванням
        public ScreenPanel() { BackgroundColor = "None"; }

        public ScreenPanel(int left, int top, int right, int bottom, string color)
        {
            LeftX = left; TopY = top; RightX = right; BottomY = bottom;
            BackgroundColor = color;
        }

        // Конструктор копіювання
        public ScreenPanel(ScreenPanel other)
        {
            LeftX = other.LeftX; TopY = other.TopY;
            RightX = other.RightX; BottomY = other.BottomY;
            BackgroundColor = string.Copy(other.BackgroundColor);
        }

        // Перевантаження операції + (накладання двох вікон)
        public static ScreenPanel operator +(ScreenPanel a, ScreenPanel b)
        {
            return new ScreenPanel(
                Math.Min(a.LeftX, b.LeftX), Math.Min(a.TopY, b.TopY),
                Math.Max(a.RightX, b.RightX), Math.Max(a.BottomY, b.BottomY),
                a.BackgroundColor
            );
        }

        // Перевантаження операції + для числа (у C# це автоматично дозволяє використовувати +=)
        public static ScreenPanel operator +(ScreenPanel p, int shift)
        {
            return new ScreenPanel(p.LeftX + shift, p.TopY + shift, p.RightX + shift, p.BottomY + shift, p.BackgroundColor);
        }

        // ВІРТУАЛЬНИЙ МЕТОД (для Поліморфізму)
        public virtual string ApplyTheme(string newColor)
        {
            this.BackgroundColor = newColor; // Змінюємо фон панелі
            return "Колір фону панелі оновлено на " + newColor;
        }

        public virtual string GetDescription()
        {
            return $"Панель: [{LeftX},{TopY}] - [{RightX},{BottomY}] | Заливка: {BackgroundColor}";
        }

        // Аналог оператора << для збереження у файл
        public override string ToString()
        {
            return $"{LeftX}|{TopY}|{RightX}|{BottomY}|{BackgroundColor}";
        }
    }
}