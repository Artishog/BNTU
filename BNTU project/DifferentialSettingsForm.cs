using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BNTU_project
{
    public partial class DifferentialSettingsForm : Form
    {
        private Form1 mainForm;

        public DifferentialSettingsForm(Form1 mainForm)
        {
            InitializeComponent();

            this.mainForm = mainForm;

            trackBar1.Minimum = 3;
            trackBar1.Maximum = 5;
            trackBar1.TickFrequency = 1;
            trackBar1.SmallChange = 1;
            trackBar1.LargeChange = 1;
            if ((mainForm.differential.n_sat < 3) || (mainForm.differential.n_sat > 5))
                trackBar1.Value = 3;
            else
                trackBar1.Value = mainForm.differential.n_sat;

            label1.Text = trackBar1.Value.ToString();

            textBox1.Text = mainForm.differential.gamma_p.ToString();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = trackBar1.Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                mainForm.differential.gamma_p = double.Parse(textBox1.Text);
                mainForm.differential.n_sat = trackBar1.Value;

                mainForm.button1_Click(sender, e);
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Неверные входные данные");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
