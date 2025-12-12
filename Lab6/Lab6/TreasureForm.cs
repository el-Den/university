using System;
using System.Drawing;
using System.Windows.Forms;
using Lab6WinForms.Core;

namespace Lab6WinForms
{
    public class TreasureForm : Form
    {
        private DataGridView dgv;
        private TreasureGame game;
        private NumericUpDown nudSize;
        private Button btnNewGame;

        public TreasureForm()
        {
            Text = "Игра: Поиск сокровищ";
            Size = new Size(900, 700);
            StartPosition = FormStartPosition.CenterParent;

            var lbl = new Label() { Text = "Клик по ячейке, чтобы открыть её", Location = new Point(10, 10) };
            nudSize = new NumericUpDown() { Location = new Point(10, 36), Minimum = 5, Maximum = 20, Value = 10 };
            btnNewGame = new Button() { Text = "Новая игра", Location = new Point(120, 32) };

            dgv = new DataGridView() { Location = new Point(10, 72), Width = 860, Height = 560 };
            dgv.RowHeadersVisible = true;
            dgv.AllowUserToAddRows = false;
            dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;

            Controls.AddRange(new Control[] { lbl, nudSize, btnNewGame, dgv });

            btnNewGame.Click += (s, e) => NewGame();
            dgv.CellClick += Dgv_CellClick;

            NewGame();
        }

        private void NewGame()
        {
            int size = (int)nudSize.Value;
            int treasures = Math.Max(3, size); // пример правила
            int traps = Math.Max(2, size / 2);
            game = new TreasureGame(size, size, treasures, traps);

            dgv.Columns.Clear();
            dgv.Rows.Clear();
            for (int c = 0; c < size; c++)
            {
                var col = new DataGridViewTextBoxColumn();
                col.Width = 40;
                col.HeaderText = c.ToString();
                dgv.Columns.Add(col);
            }
            dgv.Rows.Add(size);

            for (int r = 0; r < size; r++)
            {
                dgv.Rows[r].HeaderCell.Value = r.ToString();
                for (int c = 0; c < size; c++)
                {
                    dgv[c, r].Value = ""; // скрыто
                    dgv[c, r].Style.BackColor = Color.LightGray;
                }
            }
        }

        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex, c = e.ColumnIndex;
            if (r < 0 || c < 0) return;
            if (game.IsRevealed(r, c)) return;

            string val = game.RevealCell(r, c);
            dgv[c, r].Value = val == "0" ? "" : val;
            if (val == "C") dgv[c, r].Style.BackColor = Color.Gold;
            else if (val == "T") dgv[c, r].Style.BackColor = Color.IndianRed;
            else dgv[c, r].Style.BackColor = Color.White;

            int found = game.CountRevealedTreasures();
            int traps = game.CountRevealedTraps();
            if (found >= game.TreasureCount)
            {
                MessageBox.Show("Поздравляем — все сокровища найдены!");
                RevealAll();
            }
            else if (traps > 0)
            {
                MessageBox.Show("Увы — вы попались в ловушку!");
                RevealAll();
            }
        }

        private void RevealAll()
        {
            for (int r = 0; r < game.Rows; r++)
                for (int c = 0; c < game.Cols; c++)
                {
                    var v = game.GetCellView(r, c) ?? game.RevealCell(r, c);
                    dgv[c, r].Value = v == "0" ? "" : v;
                }
        }
    }
}
