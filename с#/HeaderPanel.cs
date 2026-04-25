using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace с_
{
    public class HeaderPanel : ScreenPanel
    {
        public string HeaderText { get; set; }
        public string FontColor { get; set; }

        public HeaderPanel(int l, int t, int r, int b, string bgCol, string header, string fCol)
            : base(l, t, r, b, bgCol)
        {
            HeaderText = header;
            FontColor = fCol;
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
    }
}
