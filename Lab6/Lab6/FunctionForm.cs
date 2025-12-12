using System;
using System.Drawing;
using System.Windows.Forms;
using Lab6WinForms.Core;

namespace Lab6WinForms
{
    public class FunctionForm : Form
    {
        private Label lblFormula, lblA, lblB, lblAttempts, lblResult, lblTime;
        private TextBox tbA, tbB, tbGuess;
        private NumericUpDown nudAttempts;
        private Button btnStart, btnCheck;
        private System.Windows.Forms.Timer _timer;
        private ProgressBar _progress;
        private int _attemptsLeft;
        private double _correctValue;
        private int _timeTotal = 30;
        private int _timeLeft;

        public FunctionForm()
        {
            Text = "Отгадай функцию";
            Size = new Size(620, 420);
            StartPosition = FormStartPosition.CenterParent;

            lblFormula = new Label() { Text = "f = log_b ( sqrt(b + sin(a)) / cos^3(a) )", Location = new Point(20, 16), AutoSize = true, Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold) };
            lblA = new Label() { Text = "a (в радианах):", Location = new Point(20, 50) };
            tbA = new TextBox() { Location = new Point(150, 46), Width = 140 };
            lblB = new Label() { Text = "b (основание):", Location = new Point(20, 86) };
            tbB = new TextBox() { Location = new Point(150, 82), Width = 140 };
            lblAttempts = new Label() { Text = "Попыток:", Location = new Point(20, 122) };
            nudAttempts = new NumericUpDown() { Location = new Point(150, 118), Minimum = 1, Maximum = 10, Value = 3 };

            btnStart = new Button() { Text = "Старт", Location = new Point(320, 46), Size = new Size(100, 30) };
            btnCheck = new Button() { Text = "Проверить", Location = new Point(320, 82), Size = new Size(100, 30), Enabled = false };

            var lblGuess = new Label() { Text = "Ваш ответ:", Location = new Point(20, 160) };
            tbGuess = new TextBox() { Location = new Point(150, 156), Width = 140, Enabled = false };
            lblResult = new Label() { Text = "", Location = new Point(20, 200), AutoSize = true, ForeColor = Color.Blue };

            _progress = new ProgressBar() { Location = new Point(20, 240), Width = 420, Minimum = 0, Maximum = _timeTotal, Value = 0 };
            lblTime = new Label() { Text = $"Осталось: {_timeTotal}s", Location = new Point(460, 240), AutoSize = true };

            Controls.AddRange(new Control[] { lblFormula, lblA, tbA, lblB, tbB, lblAttempts, nudAttempts, btnStart, btnCheck, lblGuess, tbGuess, lblResult, _progress, lblTime });

            btnStart.Click += BtnStart_Click;
            btnCheck.Click += BtnCheck_Click;

            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 1000;
            _timer.Tick += Timer_Tick;
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (!InputHelper.TryParseDouble(tbA.Text, out double a))
            { MessageBox.Show("Введите корректное число a"); return; }
            if (!InputHelper.TryParseDouble(tbB.Text, out double b))
            { MessageBox.Show("Введите корректное число b"); return; }

            _correctValue = FunctionGame.Calculate(a, b);
            if (double.IsNaN(_correctValue))
            {
                MessageBox.Show("Функция не определена при введённых значениях a и b. Попробуйте другие.");
                return;
            }

            _attemptsLeft = (int)nudAttempts.Value;
            tbGuess.Enabled = true;
            btnCheck.Enabled = true;
            btnStart.Enabled = false;
            nudAttempts.Enabled = false;

            _timeTotal = 30;
            _progress.Maximum = _timeTotal;
            _progress.Value = 0;
            _timeLeft = _timeTotal;
            lblTime.Text = $"Осталось: {_timeLeft}s";
            _timer.Start();

            lblResult.Text = $"Игра началась. Попыток: {_attemptsLeft}";
        }

        private void BtnCheck_Click(object sender, EventArgs e)
        {
            if (!InputHelper.TryParseDouble(tbGuess.Text, out double guess))
            {
                MessageBox.Show("Введите корректный ответ (число).");
                return;
            }

            double rounded = Math.Round(guess, 2);
            if (rounded == _correctValue)
            {
                _timer.Stop();
                lblResult.Text = $"Успех! Правильный ответ: {_correctValue}";
                EndGame();
            }
            else
            {
                _attemptsLeft--;
                if (_attemptsLeft <= 0)
                {
                    _timer.Stop();
                    lblResult.Text = $"Поражение. Правильный ответ: {_correctValue}";
                    EndGame();
                }
                else
                    lblResult.Text = $"Неверно. Осталось попыток: {_attemptsLeft}";
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _timeLeft--;
            _progress.Value = Math.Min(_progress.Maximum, _progress.Value + 1);
            lblTime.Text = $"Осталось: {_timeLeft}s";
            if (_timeLeft <= 0)
            {
                _timer.Stop();
                lblResult.Text = $"Время вышло. Правильный ответ: {_correctValue}";
                EndGame();
            }
        }

        private void EndGame()
        {
            tbGuess.Enabled = false;
            btnCheck.Enabled = false;
            btnStart.Enabled = true;
            nudAttempts.Enabled = true;
        }
    }
}
