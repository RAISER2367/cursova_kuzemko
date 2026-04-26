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
                currentScreen.InsertPanel(hp);
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
                lstWindows.SelectedIndex = 0; // Залишаємо фокус на верхньому елементі
            }
        }

        private void btnChangeStyle_Click(object sender, EventArgs e)
        {
            if (lstWindows.SelectedIndex == -1)
            {
                MessageBox.Show("Оберіть елемент зі списку для застосування стилю.");
                return;
            }

            int iteratorIndex = 0;
            ScreenPanel activePanel = null;

            foreach (var p in currentScreen)
            {
                if (iteratorIndex == lstWindows.SelectedIndex)
                {
                    activePanel = p;
                    break;
                }
                iteratorIndex++;
            }

            if (activePanel != null)
            {
                string resultMsg = activePanel.ApplyTheme(txtNewColor.Text);
                MessageBox.Show(resultMsg, "Демонстрація поліморфізму");
                UpdateUI();
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

        private void label3_Click(object sender, EventArgs e) { }
        private void label7_Click(object sender, EventArgs e) { }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
