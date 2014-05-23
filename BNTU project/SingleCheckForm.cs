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
    public partial class SingleCheckForm : Form
    {
        Form1 mainForm;

        public SingleCheckForm(Form1 mainForm)
        {
            InitializeComponent();

            this.mainForm = mainForm;

            try
            {
                //LoadMode parameters
                label11.Text = mainForm.loadMode.Mp.ToString();
                label12.Text = mainForm.loadMode.np.ToString();
                label13.Text = mainForm.loadMode.Mc.ToString();
                label14.Text = mainForm.loadMode.Md.ToString();
                label15.Text = mainForm.loadMode.Kpl.ToString();
                label16.Text = mainForm.loadMode.gamma_mid.ToString();
                label17.Text = mainForm.loadMode.gamma_psi.ToString();
                label18.Text = mainForm.loadMode.gamma_p.ToString();
                label19.Text = mainForm.loadMode.gamma_v.ToString();
                label20.Text = mainForm.loadMode.gamma_j.ToString();

                label31.Text = mainForm.loadMode.KPH.ToString();
                label32.Text = mainForm.loadMode.KPF.ToString();
                label33.Text = mainForm.loadMode.ksi.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка рассчета");
            }
        }
    }
}
