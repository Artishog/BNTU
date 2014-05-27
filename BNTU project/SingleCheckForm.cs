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

                //Contact parameters
                label54.Text = mainForm.contact.Ph_shest.ToString();
                label55.Text = mainForm.contact.Ph_kol.ToString();
                label56.Text = mainForm.contact.Phpo.ToString();
                //label57.Text = mainForm.contact.Ph_limb.ToString();
                label58.Text = mainForm.contact.Ft.ToString();
                label59.Text = mainForm.contact.ZH.ToString();
                label60.Text = mainForm.contact.eps_beta.ToString();
                label61.Text = mainForm.contact.eps_alpha.ToString();
                label62.Text = mainForm.contact.z_eps.ToString();
                label63.Text = mainForm.contact.KH_alpha.ToString();
                label64.Text = mainForm.contact.KH_psi.ToString();
                label65.Text = mainForm.contact.KH_gamma.ToString();
                label66.Text = mainForm.contact.precision_plav.ToString();
                label67.Text = mainForm.contact.precision_sheroh.ToString();
                label68.Text = mainForm.contact.psi_bd.ToString();
                label69.Text = mainForm.contact.K0_beta.ToString();
                label70.Text = mainForm.contact.KH_omega.ToString();
                label71.Text = mainForm.contact.v.ToString();
                label72.Text = mainForm.contact.KH_beta.ToString();
                label73.Text = mainForm.contact.KH_v.ToString();
                label74.Text = mainForm.contact.Kj_delta.ToString();
                label75.Text = mainForm.contact.Kve.ToString();
                label76.Text = mainForm.contact.R1H_shest.ToString();
                label77.Text = mainForm.contact.R1H_kol.ToString();
                label78.Text = mainForm.contact.RH_lim.ToString();
                label79.Text = mainForm.contact.LH_shest.ToString();
                label80.Text = mainForm.contact.LH_kol.ToString();

                //Flexion parameters
                label109.Text = mainForm.flexion.KF_v.ToString();
                label107.Text = mainForm.flexion.YF0_shest.ToString();
                label110.Text = mainForm.flexion.YF0_kol.ToString();
                label111.Text = mainForm.flexion.ku_shest.ToString();
                label112.Text = mainForm.flexion.ku_kol.ToString();
                label113.Text = mainForm.flexion.zv_shest.ToString();
                label114.Text = mainForm.flexion.zv_kol.ToString();
                label115.Text = mainForm.flexion.YF0_shest.ToString();
                label116.Text = mainForm.flexion.YF0_kol.ToString();
                label117.Text = mainForm.flexion.Y_eps.ToString();
                label118.Text = mainForm.flexion.KF_alpha.ToString();
                label119.Text = mainForm.flexion.KF_beta.ToString();
                label120.Text = mainForm.flexion.KF_omega.ToString();
                label121.Text = mainForm.flexion.KFmu_shest.ToString();
                label122.Text = mainForm.flexion.KFmu_kol.ToString();
                label123.Text = mainForm.flexion.KF_x.ToString();
                label124.Text = mainForm.flexion.YR.ToString();
                label125.Text = mainForm.flexion.sigmaF_shest.ToString();
                label126.Text = mainForm.flexion.sigmaF_kol.ToString();
                label127.Text = mainForm.flexion.KF_c.ToString();
                label128.Text = mainForm.flexion.sigma_FPO.ToString();
                label129.Text = mainForm.flexion.R1F_shest.ToString();
                label130.Text = mainForm.flexion.R1F_kol.ToString();
                label131.Text = mainForm.flexion.RF_lim.ToString();
                label132.Text = mainForm.flexion.LF_shest.ToString();
                label133.Text = mainForm.flexion.LF_kol.ToString();

                //endurance parameters
                label139.Text = mainForm.endurance.Mj_max.ToString();
                label140.Text = mainForm.endurance.Kj_M.ToString();
                label141.Text = mainForm.endurance.Kj.ToString();
                label142.Text = mainForm.endurance.sigmaFmax_shest.ToString();
                label143.Text = mainForm.endurance.sigmaFmax_kol.ToString();
                label144.Text = mainForm.endurance.PHmax_shest.ToString();
                label145.Text = mainForm.endurance.PHmax_kol.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка рассчета");
            }
        }
    }
}
