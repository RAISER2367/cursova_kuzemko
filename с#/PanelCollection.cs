using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace с_
{
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

        // Демонстрація зсуву (оператор +=)
        public void ShiftPanel(int idx, int offset)
        {
            if (idx >= 0 && idx < _panels.Count)
            {
                _panels[idx] += offset;
            }
        }

        // Демонстрація накладання (оператор +)
        public HeaderPanel CombinePanels(int idx1, int idx2)
        {
            if (idx1 >= 0 && idx1 < _panels.Count && idx2 >= 0 && idx2 < _panels.Count)
            {
                return _panels[idx1] + _panels[idx2];
            }
            return null;
        }

        // Робота з файлом через ООП підхід
        public void ExportToFile(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var p in _panels)
                    writer.WriteLine(p.ToString()); // Використання аналога <<
            }
        }

        public void ImportFromFile(string filePath)
        {
            if (!File.Exists(filePath)) return;
            _panels.Clear();
            string[] lines = File.ReadAllLines(filePath);

            // Йдемо з кінця, щоб при Insert(0, ...) зберігся правильний порядок
            for (int i = lines.Length - 1; i >= 0; i--)
            {
                if (!string.IsNullOrWhiteSpace(lines[i]))
                    InsertPanel(HeaderPanel.Parse(lines[i])); // Використання аналога >>
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