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
        private Flexion _flexion = new Flexion();
        private Endurance _endurance = new Endurance();

        private Steel _steel = new Steel();

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
        public Flexion flexion
        {
            get { return _flexion; }
            set { _flexion = value; }
        }
        public Endurance endurance
        {
            get { return _endurance; }
            set { _endurance = value; }
        }
        public Steel steel
        {
            get { return _steel; }
            set { _steel = value; }
        }
        
        public Form1()
        {
            InitializeComponent();

            _transferGearbox = new TransferGearbox3Case(_car, _gearwheelPair1, _gearwheelPair2, differential);

            х2Н4АToolStripMenuItem1.Checked = true;
            steel.setCurrentSteelByGrade(х2Н4АToolStripMenuItem1.Text);

            switch (transferGearbox.kinematicScheme)
            {
                case 1:
                    panel1.BackColor = Color.DarkGray;
                    panel2.BackColor = Color.White;
                    panel3.BackColor = Color.White;
                    panel4.BackColor = Color.White;
                    panel5.BackColor = Color.White;
                    panel6.BackColor = Color.White;

                    transferGearbox = new TransferGearbox1Case(car, gearwheelPair1, gearwheelPair2, differential);

                    break;
                case 2:
                    panel1.BackColor = Color.White;
                    panel2.BackColor = Color.DarkGray;
                    panel3.BackColor = Color.White;
                    panel4.BackColor = Color.White;
                    panel5.BackColor = Color.White;
                    panel6.BackColor = Color.White;

                    transferGearbox = new TransferGearbox2Case(car, gearwheelPair1, gearwheelPair2, differential);

                    break;
                case 3:
                    panel1.BackColor = Color.White;
                    panel2.BackColor = Color.White;
                    panel3.BackColor = Color.DarkGray;
                    panel4.BackColor = Color.White;
                    panel5.BackColor = Color.White;
                    panel6.BackColor = Color.White;

                    transferGearbox = new TransferGearbox3Case(car, gearwheelPair1, gearwheelPair2, differential);

                    break;
                case 4:
                    panel1.BackColor = Color.White;
                    panel2.BackColor = Color.White;
                    panel3.BackColor = Color.White;
                    panel4.BackColor = Color.DarkGray;
                    panel5.BackColor = Color.White;
                    panel6.BackColor = Color.White;

                    break;
                case 5:
                    panel1.BackColor = Color.White;
                    panel2.BackColor = Color.White;
                    panel3.BackColor = Color.White;
                    panel4.BackColor = Color.White;
                    panel5.BackColor = Color.DarkGray;
                    panel6.BackColor = Color.White;

                    break;
                case 6:
                    panel1.BackColor = Color.White;
                    panel2.BackColor = Color.White;
                    panel3.BackColor = Color.White;
                    panel4.BackColor = Color.White;
                    panel5.BackColor = Color.White;
                    panel6.BackColor = Color.DarkGray;
                    break;
            }

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

            calcCheckingClasses();

            if (!contact.isValid(car.L0))
                MessageBox.Show("Не выполняются условия по контактным напряжением");

            if (!flexion.isValid(car.L0))
                MessageBox.Show("Не выполняются условия по изгибным напряжениям");

            if (!endurance.isValid(steel.currentSteel.PHlimM, steel.currentSteel.sigmaFlimM))
                MessageBox.Show("Не выполняются условия по прочности");

            if (!gearwheelPair1.isValid())
                MessageBox.Show("Не выполняются условия по минимальному числу зубьев");

            Form singleCheckForm = new SingleCheckForm(this);
            singleCheckForm.Show();
        }

        public void calcCheckingClasses()
        {
            this.loadMode.calc_All(car.Memax, car.Ukp, gearwheelPair1.U_d, gearwheelPair2.U_d, car.ma, car.np, car.r0, car.U0, car.vehicleType, transferGearbox.kinematicScheme);

            this.contact.calc_All(loadMode.Mp, gearwheelPair1.U_d, gearwheelPair1.beta_d, gearwheelPair1.bw, gearwheel.mn,
                loadMode.np, gearwheelPair1.z_kol, gearwheelPair1.z_shest, gearwheelPair1.d_shest, gearwheelPair1.bf_shest,
                gearwheelPair1.da_kol, car.Ukp, loadMode.ksi, car.r0, steel.currentSteel.PHlimb_star, loadMode.KPH, steel.currentSteel.NHO);

            this.flexion.calc_All(gearwheelPair1.z_shest, gearwheelPair1.z_kol, gearwheelPair1.bf_shest, gearwheelPair1.bw, gearwheelPair1.beta_d,
                contact.z_eps, contact.precision_plav, contact.K0_beta, contact.Kj_delta, contact.Kve, steel.currentSteel.sigmaFlimb_c_star,
                contact.Ft, gearwheel.mn, car.Ukp, loadMode.ksi, loadMode.KPF, gearwheelPair1.U_d, gearwheelPair2.U_d, steel.currentSteel.NFO, contact.v, car.r0, car.U0, transferGearbox.kinematicScheme);

            this.endurance.calc_All(loadMode.Mc, loadMode.Mp, flexion.sigmaF_shest, flexion.sigmaF_kol, contact.Ph_shest, contact.Ph_kol);
        }

        private void нагрузочныйРежимToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadModeSettingsForm loadModeSettingsForm = new LoadModeSettingsForm(this);
            loadModeSettingsForm.Show();
        }

        private void х2Н4АToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in стальToolStripMenuItem.DropDownItems)
            {
                item.Checked = false;
            }

            х2Н4АToolStripMenuItem.Checked = true;
            steel.setCurrentSteelByGrade(х2Н4АToolStripMenuItem.Text);
        }

        private void хН3АToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in стальToolStripMenuItem.DropDownItems)
            {
                item.Checked = false;
            }

            хН3АToolStripMenuItem.Checked = true;
            steel.setCurrentSteelByGrade(хН3АToolStripMenuItem.Text);
        }

        private void xГНТАToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in стальToolStripMenuItem.DropDownItems)
            {
                item.Checked = false;
            }

            xГНТАToolStripMenuItem.Checked = true;
            steel.setCurrentSteelByGrade(xГНТАToolStripMenuItem.Text);
        }

        private void хГН2ТАToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in стальToolStripMenuItem.DropDownItems)
            {
                item.Checked = false;
            }

            хГН2ТАToolStripMenuItem.Checked = true;
            steel.setCurrentSteelByGrade(хГН2ТАToolStripMenuItem.Text);
        }

        private void хГТToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in стальToolStripMenuItem.DropDownItems)
            {
                item.Checked = false;
            }

            хГТToolStripMenuItem.Checked = true;
            steel.setCurrentSteelByGrade(хГТToolStripMenuItem.Text);
        }

        private void х2Н4ВАToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in стальToolStripMenuItem.DropDownItems)
            {
                item.Checked = false;
            }

            х2Н4ВАToolStripMenuItem.Checked = true;
            steel.setCurrentSteelByGrade(х2Н4ВАToolStripMenuItem.Text);
        }

        private void х2Н4АToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in стальToolStripMenuItem.DropDownItems)
            {
                item.Checked = false;
            }

            х2Н4АToolStripMenuItem1.Checked = true;
            steel.setCurrentSteelByGrade(х2Н4АToolStripMenuItem1.Text);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            panel1.BackColor = Color.DarkGray;
            panel2.BackColor = Color.White;
            panel3.BackColor = Color.White;
            panel4.BackColor = Color.White;
            panel5.BackColor = Color.White;
            panel6.BackColor = Color.White;

            transferGearbox = new TransferGearbox1Case(car, gearwheelPair1, gearwheelPair2, differential);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            panel1.BackColor = Color.White;
            panel2.BackColor = Color.DarkGray;
            panel3.BackColor = Color.White;
            panel4.BackColor = Color.White;
            panel5.BackColor = Color.White;
            panel6.BackColor = Color.White;

            transferGearbox = new TransferGearbox2Case(car, gearwheelPair1, gearwheelPair2, differential);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            panel1.BackColor = Color.White;
            panel2.BackColor = Color.White;
            panel3.BackColor = Color.DarkGray;
            panel4.BackColor = Color.White;
            panel5.BackColor = Color.White;
            panel6.BackColor = Color.White;

            transferGearbox = new TransferGearbox3Case(car, gearwheelPair1, gearwheelPair2, differential);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            panel1.BackColor = Color.White;
            panel2.BackColor = Color.White;
            panel3.BackColor = Color.White;
            panel4.BackColor = Color.DarkGray;
            panel5.BackColor = Color.White;
            panel6.BackColor = Color.White;

            transferGearbox = new TransferGearbox4Case(car, gearwheelPair1, gearwheelPair2, differential);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            panel1.BackColor = Color.White;
            panel2.BackColor = Color.White;
            panel3.BackColor = Color.White;
            panel4.BackColor = Color.White;
            panel5.BackColor = Color.DarkGray;
            panel6.BackColor = Color.White;

            transferGearbox = new TransferGearbox5Case(car, gearwheelPair1, gearwheelPair2, differential);
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            panel1.BackColor = Color.White;
            panel2.BackColor = Color.White;
            panel3.BackColor = Color.White;
            panel4.BackColor = Color.White;
            panel5.BackColor = Color.White;
            panel6.BackColor = Color.DarkGray;

            transferGearbox = new TransferGearbox6Case(car, gearwheelPair1, gearwheelPair2, differential);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OptimizationManager optimizationManager = new OptimizationManager(this);
            optimizationManager.startOptimizationCicle();

            Form singleResultForm = new SingleResultForm(this);
            singleResultForm.Show();
        }

    }
}
