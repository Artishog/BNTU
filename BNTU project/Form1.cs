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
        private Differential _differential = new Differential1Case();
        private TransferGearbox _transferGearbox;

        private LoadMode _loadMode = new LoadMode();
        private Contact _contact = new Contact();

        private Form carSettingsForm;
        private Form gearwheelSettingsForm;
        private Form differentialSettingsForm;
        private Form transferGearboxSettingsForm;

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
        public LoadMode loadMode
        {
            get { return _loadMode; }
            set { _loadMode = value; }
        }
        public Contact contact
        {
            get { return _contact; }
            set { _contact = value; }
        }
        
        public Form1()
        {
            InitializeComponent();

            _transferGearbox = new TransferGearbox3Case(_car, _gearwheelPair1, _gearwheelPair2, differential);

            //Gearwhell and GearwhellPair parameters
            this.gearwheel.mn = 6;
            this.gearwheelPair1.U = 1.1;
            this.gearwheel.coef_bw = 0.2;
        }

        public void button1_Click(object sender, EventArgs e)
        {
            calcAllParts();

            if (transferGearbox.needRecalculation)
            {
                transferGearbox.aw1 = transferGearbox.aw1 * (differential.d_korp + 3) / gearwheelPair2.df_kol;
                transferGearbox.aw2 = transferGearbox.aw2 * (differential.d_korp + 3) / gearwheelPair2.df_kol;

                calcAllParts();
            }

            Form singleResultForm = new SingleResultForm(this);
            singleResultForm.Show();
        }

        public void calcAllParts()
        {
            //Car parameters
            this.car.calc_All();
            //textBox1.Text = _car.Memax.ToString();

            //Transfer gearbox parameters
            transferGearbox.calc_allStep1();
            //transferGearbox.aw1 = 263.4;

            //GearwhellPair1 parameters
            this.gearwheelPair1.calc_FirstPair(transferGearbox.aw1, this.gearwheel.beta, this.gearwheel.mn, this.gearwheel.ha_star, this.gearwheel.hf_star, this.gearwheel.c_star, this.gearwheel.coef_bw, transferGearbox.U1st);

            //GearwhellPair2 parameters
            this.gearwheelPair2.calc_SecondPair(this.gearwheelPair1, this.gearwheel.beta, this.gearwheel.mn, this.gearwheel.ha_star, this.gearwheel.hf_star, this.gearwheel.c_star, this.car.Urk, transferGearbox.U2st);

            //Differential parameters
            differential.calc_allStep1(this.car, this.transferGearbox);

            //Transfer gearbox parameters
            transferGearbox.calc_allStep2();

            differential.calc_allStep2();

            transferGearbox.calc_allStep3();
        }

        private void параметрыМашиныToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (carSettingsForm == null || carSettingsForm.IsDisposed)
            {
                carSettingsForm = new CarSettingsForm(this);
                carSettingsForm.Show();
            }
        }

        private void параметрыШестерниToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gearwheelSettingsForm == null || gearwheelSettingsForm.IsDisposed)
            {
                gearwheelSettingsForm = new GearwhellSettingsForm(this);
                gearwheelSettingsForm.Show();
            }
        }

        private void параметрыДифференциалаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (differentialSettingsForm == null || differentialSettingsForm.IsDisposed)
            {
                differentialSettingsForm = new DifferentialSettingsForm(this);
                differentialSettingsForm.Show();
            }
        }

        private void параметрыРаздаточнойКоробкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (transferGearboxSettingsForm == null || transferGearboxSettingsForm.IsDisposed)
            {
                transferGearboxSettingsForm = new TransferGearboxSettingsForm(this);
                transferGearboxSettingsForm.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DependenceGraphicForm dependenceGraphicForm = new DependenceGraphicForm(this);
            dependenceGraphicForm.Show();
        }

        private void выборКинематическоСхемыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KinematicSchemeForm kinematicSchemeForm = new KinematicSchemeForm(this);
            kinematicSchemeForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            calcAllParts();

            if (transferGearbox.needRecalculation)
            {
                transferGearbox.aw1 = transferGearbox.aw1 * (differential.d_korp + 3) / gearwheelPair2.df_kol;
                transferGearbox.aw2 = transferGearbox.aw2 * (differential.d_korp + 3) / gearwheelPair2.df_kol;

                calcAllParts();
            }

            this.loadMode.calc_All(car.Memax, car.Ukp, gearwheelPair1.U_d, gearwheelPair2.U_d, car.ma, car.np, car.r0, car.U0, car.vehicleType, transferGearbox.kinematicScheme);
            this.contact.calc_All(loadMode.Mp, gearwheelPair1.beta_d, gearwheelPair1.bw, loadMode.np, gearwheel.mn, 
                loadMode.np, gearwheelPair2.z_kol, gearwheelPair2.z_shest, gearwheelPair1.d_shest);

            Form singleCheckForm = new SingleCheckForm(this);
            singleCheckForm.Show();
        }

        private void нагрузочныйРежимToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadModeSettingsForm loadModeSettingsForm = new LoadModeSettingsForm(this);
            loadModeSettingsForm.Show();
        }

    }
}
