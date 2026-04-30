using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace с_
{
    public partial class Form1 : Form
    {
        PanelCollection currentScreen = new PanelCollection();

        public Form1()
        {
            InitializeComponent();
            btnAdd.Enabled = false;

            txtX1.TextChanged += CheckInputFields;
            txtY1.TextChanged += CheckInputFields;
            txtX2.TextChanged += CheckInputFields;
            txtY2.TextChanged += CheckInputFields;
            txtBg.TextChanged += CheckInputFields;
            txtTitle.TextChanged += CheckInputFields;
            txtTextCol.TextChanged += CheckInputFields;

            // Автозавантаження при запуску
            currentScreen.ImportFromFile("panels_config.txt");
            UpdateUI();
        }

        private void CheckInputFields(object sender, EventArgs e)
        {
            bool isReady = !string.IsNullOrWhiteSpace(txtX1.Text) &&
                             !string.IsNullOrWhiteSpace(txtY1.Text) &&
                             !string.IsNullOrWhiteSpace(txtX2.Text) &&
                             !string.IsNullOrWhiteSpace(txtY2.Text) &&
                             !string.IsNullOrWhiteSpace(txtBg.Text) &&
                             !string.IsNullOrWhiteSpace(txtTitle.Text) &&
                             !string.IsNullOrWhiteSpace(txtTextCol.Text);

            btnAdd.Enabled = isReady;
        }

        private void UpdateUI()
        {
            lstWindows.Items.Clear();
            // Цикл працює за рахунок кастомного ітератора (Вимога 3)
            foreach (var panel in currentScreen)
            {
                lstWindows.Items.Add(panel.GetDescription());
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int left = int.Parse(txtX1.Text);
                int top = int.Parse(txtY1.Text);
                int right = int.Parse(txtX2.Text);
                int bottom = int.Parse(txtY2.Text);

                if (right <= left || bottom <= top)
                {
                    MessageBox.Show("Увага: Праві та нижні координати повинні бути більшими за ліві та верхні!", "Помилка вводу");
                    return;
                }

                var hp = new HeaderPanel(left, top, right, bottom, txtBg.Text, txtTitle.Text, txtTextCol.Text);
                currentScreen.InsertPanel(hp); // Додавання на 0-ву позицію
                UpdateUI();

                ResetForm();
            }
            catch { MessageBox.Show("Будь ласка, перевірте правильність вводу числових координат."); }
        }

        private void ResetForm()
        {
            txtX1.Clear(); txtY1.Clear(); txtX2.Clear(); txtY2.Clear();
            txtBg.Clear(); txtTitle.Clear(); txtTextCol.Clear();
        }

        private void btnFocus_Click(object sender, EventArgs e)
        {
            if (lstWindows.SelectedIndex != -1)
            {
                currentScreen.BringToFront(lstWindows.SelectedIndex);
                UpdateUI();
                lstWindows.SelectedIndex = 0;
            }
        }

        private void btnChangeStyle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNewColor.Text))
            {
                MessageBox.Show("Введіть колір для масового оновлення!");
                return;
            }

            // Масове оновлення стилю для демонстрації поліморфізму
            foreach (var p in currentScreen)
            {
                p.ApplyTheme(txtNewColor.Text);
            }

            MessageBox.Show("Стиль успішно оновлено! Завдяки поліморфізму змінився колір шрифту заголовка.", "Демонстрація поліморфізму");
            UpdateUI();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstWindows.SelectedIndex != -1)
            {
                currentScreen.DeletePanel(lstWindows.SelectedIndex);
                UpdateUI();
            }
            else
            {
                MessageBox.Show("Оберіть елемент, який потрібно видалити.");
            }
        }

        // ======================================================
        // КНОПКИ ДЛЯ ТЕСТУВАННЯ ПЕРЕВАНТАЖЕНИХ ОПЕРАТОРІВ
        // ======================================================

        private void btnShift_Click(object sender, EventArgs e)
        {
            if (lstWindows.SelectedIndex != -1)
            {
                // Зсув вибраної панелі на 20 пікселів вправо-вниз
                currentScreen.ShiftPanel(lstWindows.SelectedIndex, 20);
                MessageBox.Show("Координати панелі зсунуто на +20 (Демонстрація оператора +=)");
                UpdateUI();
            }
            else
            {
                MessageBox.Show("Оберіть панель для зсуву!");
            }
        }

        private void btnCombine_Click(object sender, EventArgs e)
        {
            if (lstWindows.Items.Count >= 2)
            {
                HeaderPanel combined = currentScreen.CombinePanels(0, 1);
                MessageBox.Show("Результат накладання перших двох панелей:\n\n" + combined.GetDescription(), "Демонстрація оператора +");
            }
            else
            {
                MessageBox.Show("Для накладання потрібно щонайменше 2 панелі у списку!");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            currentScreen.ExportToFile("panels_config.txt");
            MessageBox.Show("Конфігурацію збережено у файл panels_config.txt");
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            currentScreen.ImportFromFile("panels_config.txt");
            UpdateUI();
        }

        // Збереження перед закриттям форми
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            currentScreen.ExportToFile("panels_config.txt");
        }

        private void label3_Click(object sender, EventArgs e) { }
        private void label7_Click(object sender, EventArgs e) { }
        private void Form1_Load(object sender, EventArgs e) { }
    }
}
