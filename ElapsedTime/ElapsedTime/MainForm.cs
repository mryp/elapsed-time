using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElapsedTime
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void calcButton_Click(object sender, EventArgs e)
        {
            if (inputTextBox.Text == "")
            {
                MessageBox.Show("入力データがありません");
                return;
            }

            using (var reader = new StringReader(inputTextBox.Text))
            {
                DateTime startTime = DateTime.MinValue;
                DateTime endTime = DateTime.MinValue;
                while (reader.Peek() > -1)
                {
                    var line = reader.ReadLine();
                    if (line == null || line.Trim() == "")
                    {
                        continue;
                    }

                    var commmaPos = line.IndexOf(',');

                    string timeText;
                    if (commmaPos == -1)
                    {
                        timeText = line;
                    }
                    else
                    {
                        timeText = line.Substring(0, commmaPos);
                    }
                    if (!DateTime.TryParse(timeText, out DateTime time))
                    {
                        continue;
                    }

                    if (startTime == DateTime.MinValue)
                    {
                        startTime = time;
                    }
                    else
                    {
                        endTime = time;
                    }
                }

                if (startTime == DateTime.MinValue || endTime == DateTime.MinValue)
                {
                    MessageBox.Show("開始時刻または終了時刻が取得できません");
                    return;
                }

                var span = endTime - startTime;
                outputTextBox.Text = span.ToString();
            }
        }
    }
}
