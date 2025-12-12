using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Lab6WinForms.Core;

namespace Lab6WinForms
{
    public class ArrayForm : Form
    {
        private NumericUpDown nudLength;
        private Button btnCreate, btnGenerate, btnShell, btnSelect, btnMin, btnMax, btnMean, btnHighlight;
        private DataGridView dgv;
        private ArrayWorker worker;
        private DataTable table;

        public ArrayForm()
        {
            Text = "Работа с одномерными массивами";
            Size = new Size(1000, 700);
            StartPosition = FormStartPosition.CenterParent;

            var lblLen = new Label() { Text = "Длина массива:", Location = new Point(10, 12) };
            nudLength = new NumericUpDown() { Location = new Point(110, 10), Minimum = 1, Maximum = 1000, Value = 10 };
            btnCreate = new Button() { Text = "Создать (по умолчанию)", Location = new Point(230, 8), Width = 180 };
            btnGenerate = new Button() { Text = "Сгенерировать случайный", Location = new Point(430, 8), Width = 180 };

            dgv = new DataGridView() { Location = new Point(10, 48), Width = 960, Height = 520, AllowUserToAddRows = false };
            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            btnShell = new Button() { Text = "Сортировка Шелла", Location = new Point(10, 580) };
            btnSelect = new Button() { Text = "Сортировка Выбором", Location = new Point(150, 580) };
            btnMin = new Button() { Text = "Найти min", Location = new Point(320, 580) };
            btnMax = new Button() { Text = "Найти max", Location = new Point(420, 580) };
            btnMean = new Button() { Text = "Среднее", Location = new Point(520, 580) };
            btnHighlight = new Button() { Text = "Выделить min/max", Location = new Point(640, 580) };

            Controls.AddRange(new Control[] { lblLen, nudLength, btnCreate, btnGenerate, dgv, btnShell, btnSelect, btnMin, btnMax, btnMean, btnHighlight });

            btnCreate.Click += BtnCreate_Click;
            btnGenerate.Click += BtnGenerate_Click;
            btnShell.Click += BtnShell_Click;
            btnSelect.Click += BtnSelect_Click;
            btnMin.Click += BtnMin_Click;
            btnMax.Click += BtnMax_Click;
            btnMean.Click += BtnMean_Click;
            btnHighlight.Click += BtnHighlight_Click;
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            int len = (int)nudLength.Value;
            worker = new ArrayWorker(len);
            BindToGrid(worker.ArrayCopy());
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            int len = (int)nudLength.Value;
            worker = new ArrayWorker(len);
            worker.FillRandom();
            BindToGrid(worker.ArrayCopy());
        }

        private void BindToGrid(int[] arr)
        {
            table = new DataTable();
            table.Columns.Add("Value", typeof(int));
            foreach (var v in arr) table.Rows.Add(v);
            dgv.DataSource = table;
        }

        private int[] ReadFromGrid()
        {
            if (dgv.DataSource == null) return new int[0];
            var dt = (DataTable)dgv.DataSource;
            int n = dt.Rows.Count;
            int[] res = new int[n];
            for (int i = 0; i < n; i++) res[i] = Convert.ToInt32(dt.Rows[i]["Value"]);
            return res;
        }

        private void BtnShell_Click(object sender, EventArgs e)
        {
            if (dgv.DataSource == null) { MessageBox.Show("Создайте массив."); return; }
            var arr = ReadFromGrid();
            worker.SetArray(arr);
            worker.ShellSort();
            BindToGrid(worker.ArrayCopy());
        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            if (dgv.DataSource == null) { MessageBox.Show("Создайте массив."); return; }
            var arr = ReadFromGrid();
            worker.SetArray(arr);
            worker.SelectionSort();
            BindToGrid(worker.ArrayCopy());
        }

        private void BtnMin_Click(object sender, EventArgs e)
        {
            if (dgv.DataSource == null) return;
            var arr = ReadFromGrid();
            MessageBox.Show("min = " + arr.Min());
        }

        private void BtnMax_Click(object sender, EventArgs e)
        {
            if (dgv.DataSource == null) return;
            var arr = ReadFromGrid();
            MessageBox.Show("max = " + arr.Max());
        }

        private void BtnMean_Click(object sender, EventArgs e)
        {
            if (dgv.DataSource == null) return;
            var arr = ReadFromGrid();
            MessageBox.Show("mean = " + arr.Average().ToString("F2"));
        }

        private void BtnHighlight_Click(object sender, EventArgs e)
        {
            if (dgv.DataSource == null) return;
            var arr = ReadFromGrid();
            int min = arr.Min(), max = arr.Max();
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                int val = Convert.ToInt32(dgv.Rows[i].Cells[0].Value);
                if (val == min) dgv.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                else if (val == max) dgv.Rows[i].DefaultCellStyle.BackColor = Color.LightCoral;
                else dgv.Rows[i].DefaultCellStyle.BackColor = Color.White;
            }
        }
    }
}
