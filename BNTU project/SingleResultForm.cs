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
                label75.Text = mainForm.gearwheelPair1.bf_shest.ToString();
                label76.Text = mainForm.gearwheelPair1.U.ToString();
                label77.Text = mainForm.gearwheelPair1.U_d.ToString();
                label78.Text = mainForm.gearwheelPair1.delta_U.ToString();
                label79.Text = mainForm.gearwheelPair1.d_kol.ToString();
                label90.Text = mainForm.gearwheelPair1.d_shest.ToString();
                label91.Text = mainForm.gearwheelPair1.da_kol.ToString();
                label92.Text = mainForm.gearwheelPair1.da_shest.ToString();
                label93.Text = mainForm.gearwheelPair1.df_kol.ToString();
                label94.Text = mainForm.gearwheelPair1.df_shest.ToString();
                label95.Text = mainForm.gearwheelPair1.bw.ToString();

                //second pair settings
                label110.Text = mainForm.gearwheelPair2.beta_d.ToString();
                label111.Text = mainForm.gearwheelPair2.mt.ToString();
                label112.Text = mainForm.gearwheelPair2.z_sum.ToString();
                label113.Text = mainForm.gearwheelPair2.z_shest.ToString();
                label114.Text = mainForm.gearwheelPair2.z_kol.ToString();
                label115.Text = mainForm.gearwheelPair2.bf_shest.ToString();
                label116.Text = mainForm.gearwheelPair2.U.ToString();
                label117.Text = mainForm.gearwheelPair2.U_d.ToString();
                label118.Text = mainForm.gearwheelPair2.delta_U.ToString();
                label119.Text = mainForm.gearwheelPair2.d_kol.ToString();
                label130.Text = mainForm.gearwheelPair2.d_shest.ToString();
                label131.Text = mainForm.gearwheelPair2.da_kol.ToString();
                label132.Text = mainForm.gearwheelPair2.da_shest.ToString();
                label133.Text = mainForm.gearwheelPair2.df_kol.ToString();
                label134.Text = mainForm.gearwheelPair2.df_shest.ToString();
                label135.Text = mainForm.gearwheelPair2.bw.ToString();

                //differential settings
                label150.Text = mainForm.differential.d_korp.ToString();
                label151.Text = mainForm.differential.d_val.ToString();
                label152.Text = mainForm.differential.d_kor.ToString();
                label153.Text = mainForm.differential.d_sun.ToString();
                label154.Text = mainForm.differential.d_sat.ToString();
                label155.Text = mainForm.differential.aw_sat.ToString();
                label156.Text = mainForm.differential.aw_dif.ToString();
                label157.Text = mainForm.differential.b_sun.ToString();
                label158.Text = mainForm.differential.b_sat.ToString();
                label159.Text = mainForm.differential.l1.ToString();
                label170.Text = mainForm.differential.l2.ToString();
                label171.Text = mainForm.differential.M0_dif.ToString();
                label172.Text = mainForm.differential.s.ToString();
                label173.Text = mainForm.differential.gamma_p.ToString();
                label174.Text = mainForm.differential.n_sat.ToString();
                label175.Text = mainForm.differential.M1_d.ToString();
                label176.Text = mainForm.differential.M2_d.ToString();
                label177.Text = mainForm.differential.i_d.ToString();
                label178.Text = mainForm.differential.m_dif.ToString();

                //transferGearbox settings
                label190.Text = mainForm.transferGearbox.aw1.ToString();
                label191.Text = mainForm.transferGearbox.aw2.ToString();
                label192.Text = mainForm.transferGearbox.Ka.ToString();
                label193.Text = mainForm.transferGearbox.U_d1.ToString();
                label194.Text = mainForm.transferGearbox.U_d2.ToString();
                label195.Text = mainForm.transferGearbox.d1.ToString();
                label196.Text = mainForm.transferGearbox.d2.ToString();
                label197.Text = mainForm.transferGearbox.d3.ToString();
                label198.Text = mainForm.transferGearbox.L2.ToString();
                label199.Text = mainForm.transferGearbox.H1.ToString();
                label210.Text = mainForm.transferGearbox.H2.ToString();
                label211.Text = mainForm.transferGearbox.delta.ToString();
                label212.Text = mainForm.transferGearbox.B1.ToString();
                label213.Text = mainForm.transferGearbox.B2.ToString();
                //label214.Text = mainForm.transferGearbox.dmax.ToString();
                label215.Text = mainForm.transferGearbox.mrk.ToString();

                label49.Text = mainForm.transferGearbox.m_korp.ToString();
                label59.Text = (mainForm.transferGearbox.mrk - mainForm.transferGearbox.m_korp).ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка рассчета");
            }
        }

    }
}
