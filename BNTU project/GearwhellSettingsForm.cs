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
    public partial class GearwhellSettingsForm : Form
    {
        private Form1 mainForm;

        public GearwhellSettingsForm(Form1 mainForm)
        {
            InitializeComponent();

            this.mainForm = mainForm;

            trackBar1.Minimum = 7;
            trackBar1.Maximum = 22;
            trackBar1.TickFrequency = 1;
            trackBar1.SmallChange = 1;
            trackBar1.LargeChange = 1;
            if ((mainForm.gearwheel.beta < 7) || (mainForm.gearwheel.beta > 22))
                trackBar1.Value = 7;
            else
                trackBar1.Value = mainForm.gearwheel.beta;

            trackBar2.Minimum = 375;
            trackBar2.Maximum = 650;
            trackBar2.TickFrequency = 25;
            trackBar2.SmallChange = 25;
            trackBar2.LargeChange = 25;
            if ((mainForm.gearwheel.mn * 100 < 375) || (mainForm.gearwheel.mn * 100 > 650))
                trackBar2.Value = 375;
            else
                trackBar2.Value = (int)(mainForm.gearwheel.mn * 100);

            trackBar3.Minimum = 19;
            trackBar3.Maximum = 22;
            trackBar3.TickFrequency = 1;
            trackBar3.SmallChange = 1;
            trackBar3.LargeChange = 1;
            if ((mainForm.gearwheel.coef_bw * 100 < 19) || (mainForm.gearwheel.coef_bw * 100 > 22))
                trackBar3.Value = 19;
            else
                trackBar3.Value = (int)(mainForm.gearwheel.coef_bw * 100);

            label10.Text = trackBar1.Value.ToString();
            label11.Text = ((double)trackBar2.Value / 100).ToString();
            label12.Text = ((double)trackBar3.Value / 100).ToString();

            textBox1.Text = mainForm.gearwheel.x_kol.ToString();
            textBox2.Text = mainForm.gearwheel.x_shest.ToString();
            textBox3.Text = mainForm.gearwheel.alpha.ToString();
            textBox4.Text = mainForm.gearwheel.ha_star.ToString();
            textBox5.Text = mainForm.gearwheel.hf_star.ToString();
            textBox6.Text = mainForm.gearwheel.c_star.ToString();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label10.Text = trackBar1.Value.ToString();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            label11.Text = ((double)trackBar2.Value / 100).ToString();
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            label12.Text = ((double)trackBar3.Value / 100).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                mainForm.gearwheel.x_kol = double.Parse(textBox1.Text);
                mainForm.gearwheel.x_shest = double.Parse(textBox2.Text);
                mainForm.gearwheel.alpha = int.Parse(textBox3.Text);
                mainForm.gearwheel.ha_star = int.Parse(textBox4.Text);
                mainForm.gearwheel.hf_star = double.Parse(textBox5.Text);
                mainForm.gearwheel.c_star = double.Parse(textBox6.Text);

                mainForm.gearwheel.beta = trackBar1.Value;
                mainForm.gearwheel.mn = (double)trackBar2.Value / 100;
                mainForm.gearwheel.coef_bw = (double)trackBar3.Value / 100;

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
