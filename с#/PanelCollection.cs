using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace с_
{
    // Реалізуємо IEnumerable для підтримки foreach
    public class PanelCollection : IEnumerable<HeaderPanel>
    {
        private List<HeaderPanel> _panels = new List<HeaderPanel>();

        public void InsertPanel(HeaderPanel panel)
        {
            _panels.Insert(0, panel);
        }

        public void BringToFront(int idx)
        {
            if (idx > 0 && idx < _panels.Count)
            {
                HeaderPanel target = _panels[idx];
                _panels.RemoveAt(idx);
                _panels.Insert(0, target);
            }
        }

        public void DeletePanel(int idx)
        {
            if (idx >= 0 && idx < _panels.Count) _panels.RemoveAt(idx);
        }

        // Робота з файлом (тепер використовуємо розділювач '|')
        public void ExportToFile(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var p in _panels)
                    writer.WriteLine($"{p.LeftX}|{p.TopY}|{p.RightX}|{p.BottomY}|{p.BackgroundColor}|{p.HeaderText}|{p.FontColor}");
            }
        }

        public void ImportFromFile(string filePath)
        {
            if (!File.Exists(filePath)) return;
            _panels.Clear();
            string[] lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split('|');
                if (parts.Length == 7)
                    _panels.Add(new HeaderPanel(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]), parts[4], parts[5], parts[6]));
            }
        }

        // --- РЕАЛІЗАЦІЯ ІТЕРАТОРА ---

        public IEnumerator<HeaderPanel> GetEnumerator()
        {
            return new PanelIterator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // Внутрішній КЛАС-ІТЕРАТОР
        private class PanelIterator : IEnumerator<HeaderPanel>
        {
            private readonly PanelCollection _source;
            private int _pos;

            public PanelIterator(PanelCollection source)
            {
                _source = source;
                _pos = -1;
            }

            public HeaderPanel Current => _source._panels[_pos];
            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                _pos++;
                return _pos < _source._panels.Count;
            }

            public void Reset()
            {
                _pos = -1;
            }

            public void Dispose() { }
        }
    }
}
