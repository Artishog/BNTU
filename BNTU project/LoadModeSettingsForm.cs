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
    public partial class LoadModeSettingsForm : Form
    {
        private Form1 mainForm;

        public LoadModeSettingsForm(Form1 mainForm)
        {
            InitializeComponent();

            this.mainForm = mainForm;

            textBox1.Text = mainForm.loadMode.Kpl.ToString();
            textBox2.Text = mainForm.loadMode.gamma_v.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                mainForm.loadMode.Kpl = double.Parse(textBox1.Text);
                mainForm.loadMode.gamma_v = double.Parse(textBox2.Text);
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
