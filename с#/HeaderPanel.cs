using System;

namespace с_
{
    public class HeaderPanel : ScreenPanel
    {
        public string HeaderText { get; set; }
        public string FontColor { get; set; }

        public HeaderPanel() : base()
        {
            HeaderText = "NoName";
            FontColor = "Black";
        }

        public HeaderPanel(int l, int t, int r, int b, string bgCol, string header, string fCol)
            : base(l, t, r, b, bgCol)
        {
            HeaderText = header;
            FontColor = fCol;
        }

        // Конструктор копіювання
        public HeaderPanel(HeaderPanel other) : base(other)
        {
            HeaderText = string.Copy(other.HeaderText);
            FontColor = string.Copy(other.FontColor);
        }

        // Перевантаження + для зсуву
        public static HeaderPanel operator +(HeaderPanel p, int shift)
        {
            return new HeaderPanel(
                p.LeftX + shift, p.TopY + shift,
                p.RightX + shift, p.BottomY + shift,
                p.BackgroundColor, p.HeaderText, p.FontColor
            );
        }

        // Перевантаження + для накладання
        public static HeaderPanel operator +(HeaderPanel a, HeaderPanel b)
        {
            ScreenPanel combinedBase = (ScreenPanel)a + (ScreenPanel)b;
            return new HeaderPanel(combinedBase.LeftX, combinedBase.TopY, combinedBase.RightX, combinedBase.BottomY, combinedBase.BackgroundColor, "Об'єднано", "Red");
        }

        // ПЕРЕВИЗНАЧЕННЯ МЕТОДУ (Пізнє зв'язування)
        public override string ApplyTheme(string newColor)
        {
            this.FontColor = newColor; // Змінюємо колір тексту
            return $"Колір тексту шапки '{HeaderText}' оновлено на {newColor}";
        }

        public override string GetDescription()
        {
            return base.GetDescription() + $" | Шапка: <{HeaderText}> | Шрифт: {FontColor}";
        }

        // Аналог оператора << 
        public override string ToString()
        {
            return base.ToString() + $"|{HeaderText}|{FontColor}";
        }

        // Аналог оператора >> (читання з рядка файлу)
        public static HeaderPanel Parse(string data)
        {
            string[] parts = data.Split('|');
            if (parts.Length == 7)
            {
                return new HeaderPanel(
                    int.Parse(parts[0]), int.Parse(parts[1]),
                    int.Parse(parts[2]), int.Parse(parts[3]),
                    parts[4], parts[5], parts[6]
                );
            }
            throw new FormatException("Невірний формат рядка.");
        }
    }
}