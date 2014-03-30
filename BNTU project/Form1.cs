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

    public partial class Form1 : Form
    {
        private Car _car = new Car();
        private GearwheelPair _gearwheelPair1 = new GearwheelPair();
        private GearwheelPair _gearwheelPair2 = new GearwheelPair();
        private Gearwheel _gearwheel = new Gearwheel();
        private TransferGearbox _transferGearbox = new TransferGearbox();
        private Differential _differential = new Differential();

        public Car car 
        { 
            get { return _car; } 
            set { _car = value; } 
        }
        public GearwheelPair gearwheelPair1 
        {
            get { return _gearwheelPair1; }
            set { _gearwheelPair1 = value; } 
        }
        public GearwheelPair gearwheelPair2 
        {
            get { return _gearwheelPair2; }
            set { _gearwheelPair2 = value; }
        }
        public Gearwheel gearwheel {
            get { return _gearwheel; }
            set { _gearwheel = value; } 
        }
        public TransferGearbox transferGearbox 
        {
            get { return _transferGearbox; }
            set { _transferGearbox = value; } 
        }
        public Differential differential 
        {
            get { return _differential; }
            set { _differential = value; } 
        }
        
        public Form1()
        {
            InitializeComponent();

            //Gearwhell and GearwhellPair parameters
            this.gearwheel.mn = 6;
            this.gearwheelPair1.U = 1.1;
            this.gearwheel.coef_bw = 0.2;
        }

        public void button1_Click(object sender, EventArgs e)
        {
            //DataBase db = new DataBase();

            //Car parameters
            this.car.calc_All();
            //textBox1.Text = _car.Memax.ToString();
            if (textBox1.Text != "")
            {
                this.car.Memax = Convert.ToInt32(textBox1.Text);
            }
            else
            {
                textBox1.Text = this.car.Memax.ToString();
            }
            textBox2.Text = this.car.Urk.ToString();

            //Transfer gearbox parameters
            transferGearbox.calc_allStep1(this.car.Urk, this.car.gearsToUkp.getUkpByGear(1), this.car.Memax);
            //transferGearbox.aw1 = 263.4;

            //GearwhellPair1 parameters
            this.gearwheelPair1.calc_FirstPair(transferGearbox.aw1, this.gearwheel.beta, this.gearwheel.mn, this.gearwheel.ha_star, this.gearwheel.hf_star, this.gearwheel.c_star, this.gearwheel.coef_bw, transferGearbox.U1st);

            //GearwhellPair2 parameters
            this.gearwheelPair2.calc_SecondPair(this.gearwheelPair1, this.gearwheel.beta, this.gearwheel.mn, this.gearwheel.ha_star, this.gearwheel.hf_star, this.gearwheel.c_star, this.car.Urk, transferGearbox.U2st);

            //Differential parameters
            differential.calc_all(this.car.Memax, this.car.gearsToUkp.getUkpByGear(1), this.car.Urk, this.car.m1, this.car.m2, transferGearbox.Ka);

            //Transfer gearbox parameters
            transferGearbox.calc_allStep2(this.gearwheelPair1.d_shest, this.gearwheelPair1.d_kol, this.gearwheelPair2.d_kol, differential.l2, this.gearwheelPair1.bw, this.gearwheelPair2.bw, this.gearwheelPair1.bf_shest, this.gearwheelPair2.aw2_d, differential.m_dif);

            

            //GearwheelPair1 output
            textBox3.Text = this.gearwheelPair1.z_sum.ToString();
            textBox4.Text = this.gearwheelPair1.beta_d.ToString();
            textBox5.Text = this.gearwheelPair1.mt.ToString();
            textBox6.Text = this.gearwheelPair1.z_shest.ToString();
            textBox7.Text = this.gearwheelPair1.z_kol.ToString();
            textBox8.Text = this.gearwheelPair1.U_d.ToString();
            textBox9.Text = this.gearwheelPair1.delta_U.ToString();
            textBox10.Text = this.gearwheelPair1.d_shest.ToString();
            textBox11.Text = this.gearwheelPair1.d_kol.ToString();
            textBox12.Text = this.gearwheelPair1.da_shest.ToString();
            textBox13.Text = this.gearwheelPair1.da_kol.ToString();
            textBox14.Text = this.gearwheelPair1.df_shest.ToString();
            textBox15.Text = this.gearwheelPair1.df_kol.ToString();
            textBox16.Text = this.gearwheelPair1.bw.ToString();
            textBox17.Text = this.gearwheelPair1.bf_shest.ToString();

            //GearwheelPair2 output
            textBox26.Text = this.gearwheelPair2.z_sum.ToString();
            textBox27.Text = this.gearwheelPair2.beta_d.ToString();
            textBox28.Text = this.gearwheelPair2.mt.ToString();
            textBox29.Text = this.gearwheelPair2.z_shest.ToString();
            textBox30.Text = this.gearwheelPair2.z_kol.ToString();
            textBox31.Text = this.gearwheelPair2.U_d.ToString();
            textBox32.Text = this.gearwheelPair2.delta_U.ToString();
            textBox33.Text = this.gearwheelPair2.d_shest.ToString();
            textBox34.Text = this.gearwheelPair2.d_kol.ToString();
            textBox35.Text = this.gearwheelPair2.da_shest.ToString();
            textBox36.Text = this.gearwheelPair2.da_kol.ToString();
            textBox37.Text = this.gearwheelPair2.df_shest.ToString();
            textBox38.Text = this.gearwheelPair2.df_kol.ToString();
            textBox39.Text = this.gearwheelPair2.bw.ToString();
            textBox40.Text = this.gearwheelPair2.bf_shest.ToString();

            //тест базы данных
            //textBox18.Text = db.Connect();

            //объем и масса дифференциала
            textBox18.Text = differential.V_dif.ToString();
            textBox19.Text = differential.m_dif.ToString();

            //TransferGearBox output
            textBox20.Text = transferGearbox.Ka.ToString();
            textBox21.Text = transferGearbox.aw1.ToString();
            textBox22.Text = transferGearbox.aw2.ToString();
            textBox23.Text = transferGearbox.U1st.ToString();
            textBox24.Text = transferGearbox.U2st.ToString();
            textBox25.Text = transferGearbox.M0.ToString();

            textBox41.Text = transferGearbox.H1.ToString();
            textBox42.Text = transferGearbox.H2.ToString();
            textBox43.Text = transferGearbox.B1.ToString();
            textBox44.Text = transferGearbox.B2.ToString();
            textBox45.Text = transferGearbox.L1.ToString();
            textBox46.Text = transferGearbox.L2.ToString();
            textBox47.Text = transferGearbox.V_korp.ToString();
            textBox48.Text = transferGearbox.m_korp.ToString();
            textBox49.Text = transferGearbox.msh1.ToString();
            textBox50.Text = transferGearbox.msh2.ToString();
            textBox51.Text = transferGearbox.msh3.ToString();
            textBox52.Text = transferGearbox.mv1.ToString();
            textBox53.Text = transferGearbox.mv2.ToString();
            textBox54.Text = transferGearbox.mv3.ToString();
            textBox55.Text = transferGearbox.mrk.ToString();

            Form singleResultForm = new SingleResultForm(this);
            singleResultForm.Show();

          
        }

        private void параметрыМашиныToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form carSettingsForm = new CarSettingsForm(this);
            carSettingsForm.Show();
        }

        private void параметрыШестерниToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form gearwhellSettingsForm = new GearwhellSettingsForm(this);
            gearwhellSettingsForm.Show();
        }

        private void параметрыДифференциалаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form differentialSettingsForm = new DifferentialSettingsForm(this);
            differentialSettingsForm.Show();
        }

        private void параметрыРаздаточнойКоробкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form transferGearboxSettingsForm = new TransferGearboxSettingsForm(this);
            transferGearboxSettingsForm.Show();
        }

    }
}
