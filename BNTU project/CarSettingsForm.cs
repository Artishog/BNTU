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
    public partial class CarSettingsForm : Form
    {
        private Form1 mainForm;

        public CarSettingsForm(Form1 mainForm)
        {
            InitializeComponent();

            this.mainForm = mainForm;

            this.comboBox1.Items.AddRange(new object[] {
                        "Легковой",
                        "Грузовой",
                        "Автобус городской",
                        "Автобус междугородний",
                        "Самосвал",
                        "Многоприводный автомобиль"});
            if (mainForm.car.vehicleType != null)
                this.comboBox1.SelectedItem = mainForm.car.vehicleType;
            else 
                this.comboBox1.SelectedItem = "Легковой";

            textBox1.Text = mainForm.car.ma.ToString();
            textBox2.Text = mainForm.car.m1.ToString();
            textBox3.Text = mainForm.car.m2.ToString();
            textBox4.Text = mainForm.car.G_fi.ToString();
            textBox5.Text = mainForm.car.Pemax.ToString();
            textBox6.Text = mainForm.car.np.ToString();
            textBox7.Text = mainForm.car.Memax.ToString();
            textBox8.Text = mainForm.car.nm.ToString();
            textBox9.Text = mainForm.car.K.ToString();
            textBox10.Text = mainForm.car.r0.ToString();
            textBox11.Text = mainForm.car.Vamax.ToString();
            textBox12.Text = mainForm.car.L0.ToString();
            textBox13.Text = mainForm.car.U0.ToString();
            textBox14.Text = mainForm.car.Ukp.ToString();
            textBox15.Text = mainForm.car.Urk_psi.ToString();
            textBox16.Text = mainForm.car.kpd_tr.ToString();
            textBox17.Text = mainForm.car.Urk_fi.ToString();
            textBox18.Text = mainForm.car.Urk.ToString();

            this.textBox1.Select();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                mainForm.car.vehicleType = comboBox1.SelectedItem.ToString();
                mainForm.car.ma = int.Parse(textBox1.Text);
                mainForm.car.m1 = double.Parse(textBox2.Text);
                mainForm.car.m2 = double.Parse(textBox3.Text);
                mainForm.car.G_fi = int.Parse(textBox4.Text);
                mainForm.car.Pemax = int.Parse(textBox5.Text);
                mainForm.car.np = int.Parse(textBox6.Text);
                mainForm.car.Memax = int.Parse(textBox7.Text);
                mainForm.car.nm = int.Parse(textBox8.Text);
                mainForm.car.K = int.Parse(textBox9.Text);
                mainForm.car.r0 = double.Parse(textBox10.Text);
                mainForm.car.Vamax = int.Parse(textBox11.Text);
                mainForm.car.L0 = int.Parse(textBox12.Text);
                mainForm.car.U0 = double.Parse(textBox13.Text);
                mainForm.car.Ukp = double.Parse(textBox14.Text);
                mainForm.car.Urk_psi = double.Parse(textBox15.Text);
                mainForm.car.kpd_tr = double.Parse(textBox16.Text);
                mainForm.car.Urk_fi = double.Parse(textBox17.Text);
                mainForm.car.Urk = double.Parse(textBox18.Text);

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
