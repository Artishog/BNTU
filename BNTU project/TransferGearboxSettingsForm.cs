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
    public partial class TransferGearboxSettingsForm : Form
    {
        private Form1 mainForm;

        public TransferGearboxSettingsForm(Form1 mainForm)
        {
            InitializeComponent();

            this.mainForm = mainForm;

            trackBar1.Minimum = 85;
            trackBar1.Maximum = 96;
            trackBar1.TickFrequency = 1;
            trackBar1.SmallChange = 1;
            trackBar1.LargeChange = 1;
            if ((mainForm.transferGearbox.Ka * 10 < 85) || (mainForm.transferGearbox.Ka * 10 > 96))
                trackBar1.Value = 85;
            else
                trackBar1.Value = (int)(mainForm.transferGearbox.Ka * 10);

            label1.Text = ((double)trackBar1.Value / 10).ToString();

            textBox1.Text = mainForm.transferGearbox.delta.ToString();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = ((double)trackBar1.Value / 10).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                mainForm.transferGearbox.delta = double.Parse(textBox1.Text);
                mainForm.transferGearbox.Ka = (double)trackBar1.Value / 10;

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
