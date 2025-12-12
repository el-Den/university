using Lab6;
using Lab6WinForms.Core;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab6WinForms
{
    public class MainForm : Form
    {
        public MainForm()
        {
            Text = "Лабораторная №6 - Главное меню";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(420, 320);

            var btnFunc = new Button() { Text = "1. Отгадай функцию", Location = new Point(20, 20), Size = new Size(350, 40) };
            var btnArray = new Button() { Text = "2. Работа с массивами", Location = new Point(20, 70), Size = new Size(350, 40) };
            var btnGame = new Button() { Text = "3. Игра (Поиск сокровищ)", Location = new Point(20, 120), Size = new Size(350, 40) };
            var btnAbout = new Button() { Text = "4. Об авторе", Location = new Point(20, 170), Size = new Size(350, 40) };
            var btnExit = new Button() { Text = "Выход", Location = new Point(20, 220), Size = new Size(350, 40) };

            btnFunc.Click += (s, e) => new FunctionForm().ShowDialog();
            btnArray.Click += (s, e) => new ArrayForm().ShowDialog();
            btnGame.Click += (s, e) => new TreasureForm().ShowDialog();
            btnAbout.Click += (s, e) => new AboutForm().ShowDialog();
            btnExit.Click += (s, e) => Close();

            Controls.AddRange(new Control[] { btnFunc, btnArray, btnGame, btnAbout, btnExit });

            FormClosing += (s, e) =>
            {
                var res = MessageBox.Show("Вы действительно хотите выйти?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.No) e.Cancel = true;
            };
        }
    }
}
