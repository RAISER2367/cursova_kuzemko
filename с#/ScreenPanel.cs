using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace с_
{
    public class ScreenPanel
    {
        // Координати та колір фону перейменовано
        public int LeftX { get; set; }
        public int TopY { get; set; }
        public int RightX { get; set; }
        public int BottomY { get; set; }
        public string BackgroundColor { get; set; }

        public ScreenPanel(int left, int top, int right, int bottom, string color)
        {
            LeftX = left; TopY = top; RightX = right; BottomY = bottom;
            BackgroundColor = color;
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
    }
}
