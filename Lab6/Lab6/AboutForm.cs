using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab6WinForms
{
    public class AboutForm : Form
    {
        public AboutForm()
        {
            Text = "Об авторе";
            Size = new Size(480, 280);
            StartPosition = FormStartPosition.CenterParent;

            var lbl = new Label()
            {
                Text = "Автор: Полевода Денис Алексеевич\nГруппа: 6105-090301D\n\nЛабораторная работа №6: Windows Forms\nРеализованы формы: функция, массивы, игра, автор.",
                Location = new Point(20, 20),
                AutoSize = true
            };
            Controls.Add(lbl);
        }
    }
}
