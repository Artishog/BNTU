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
    public partial class SingleResultForm : Form
    {

        private Form1 mainForm;

        public SingleResultForm(Form1 mainForm)
        {
            InitializeComponent();

            this.mainForm = mainForm;

            //car parameters
            try
            {
                //car settings
                label11.Text = mainForm.car.vehicleType.ToString();
                label12.Text = mainForm.car.ma.ToString();
                label13.Text = mainForm.car.m1.ToString();
                label14.Text = mainForm.car.m2.ToString();
                label15.Text = mainForm.car.G_fi.ToString();
                label16.Text = mainForm.car.Pemax.ToString();
                label17.Text = mainForm.car.np.ToString();
                label18.Text = mainForm.car.Memax.ToString();
                label19.Text = mainForm.car.nm.ToString();
                label20.Text = mainForm.car.K.ToString();

                label31.Text = mainForm.car.r0.ToString();
                label32.Text = mainForm.car.Vamax.ToString();
                label33.Text = mainForm.car.L0.ToString();
                label34.Text = mainForm.car.U0.ToString();
                label35.Text = mainForm.car.Ukp.ToString();
                label36.Text = mainForm.car.Urk_psi.ToString();
                label37.Text = mainForm.car.kpd_tr.ToString();
                label38.Text = mainForm.car.Urk_fi.ToString();
                label39.Text = mainForm.car.Urk.ToString();

                //gearwheel settings
                label50.Text = mainForm.gearwheel.x_kol.ToString();
                label51.Text = mainForm.gearwheel.x_shest.ToString();
                label52.Text = mainForm.gearwheel.beta.ToString();
                label53.Text = mainForm.gearwheel.mn.ToString();
                label54.Text = mainForm.gearwheel.coef_bw.ToString();
                label55.Text = mainForm.gearwheel.alpha.ToString();
                label56.Text = mainForm.gearwheel.ha_star.ToString();
                label57.Text = mainForm.gearwheel.hf_star.ToString();
                label58.Text = mainForm.gearwheel.c_star.ToString();

                //first pair settings
                label70.Text = mainForm.gearwheelPair1.beta_d.ToString();
                label71.Text = mainForm.gearwheelPair1.mt.ToString();
                label72.Text = mainForm.gearwheelPair1.z_sum.ToString();
                label73.Text = mainForm.gearwheelPair1.z_shest.ToString();
                label74.Text = mainForm.gearwheelPair1.z_kol.ToString();
                label75.Text = mainForm.gearwheelPair1.zmin.ToString();
                label76.Text = mainForm.gearwheelPair1.U.ToString();
                label77.Text = mainForm.gearwheelPair1.U_d.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка рассчета");
            }
        }

    }
}
