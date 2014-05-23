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
    public partial class KinematicSchemeForm : Form
    {
        private TransferGearbox transferGearbox;
        private Form1 mainForm;

        public KinematicSchemeForm(Form1 mainForm)
        {
            InitializeComponent();

            this.mainForm = mainForm;

            switch (mainForm.transferGearbox.kinematicScheme)
            {
                case 1:
                    panel1.BackColor = Color.DarkGray;
                    panel2.BackColor = Color.White;
                    panel3.BackColor = Color.White;
                    panel4.BackColor = Color.White;
                    panel5.BackColor = Color.White;
                    panel6.BackColor = Color.White;

                    transferGearbox = new TransferGearbox1Case(mainForm.car, mainForm.gearwheelPair1, mainForm.gearwheelPair2, mainForm.differential);

                    break;
                case 2:
                    panel1.BackColor = Color.White;
                    panel2.BackColor = Color.DarkGray;
                    panel3.BackColor = Color.White;
                    panel4.BackColor = Color.White;
                    panel5.BackColor = Color.White;
                    panel6.BackColor = Color.White;

                    transferGearbox = new TransferGearbox2Case(mainForm.car, mainForm.gearwheelPair1, mainForm.gearwheelPair2, mainForm.differential);

                    break;
                case 3:
                    panel1.BackColor = Color.White;
                    panel2.BackColor = Color.White;
                    panel3.BackColor = Color.DarkGray;
                    panel4.BackColor = Color.White;
                    panel5.BackColor = Color.White;
                    panel6.BackColor = Color.White;

                    transferGearbox = new TransferGearbox3Case(mainForm.car, mainForm.gearwheelPair1, mainForm.gearwheelPair2, mainForm.differential);

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
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            panel1.BackColor = Color.DarkGray;
            panel2.BackColor = Color.White;
            panel3.BackColor = Color.White;
            panel4.BackColor = Color.White;
            panel5.BackColor = Color.White;
            panel6.BackColor = Color.White;

            transferGearbox = new TransferGearbox1Case(mainForm.car, mainForm.gearwheelPair1, mainForm.gearwheelPair2, mainForm.differential);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            panel1.BackColor = Color.White;
            panel2.BackColor = Color.DarkGray;
            panel3.BackColor = Color.White;
            panel4.BackColor = Color.White;
            panel5.BackColor = Color.White;
            panel6.BackColor = Color.White;

            transferGearbox = new TransferGearbox2Case(mainForm.car, mainForm.gearwheelPair1, mainForm.gearwheelPair2, mainForm.differential);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            panel1.BackColor = Color.White;
            panel2.BackColor = Color.White;
            panel3.BackColor = Color.DarkGray;
            panel4.BackColor = Color.White;
            panel5.BackColor = Color.White;
            panel6.BackColor = Color.White;

            transferGearbox = new TransferGearbox3Case(mainForm.car, mainForm.gearwheelPair1, mainForm.gearwheelPair2, mainForm.differential);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            panel1.BackColor = Color.White;
            panel2.BackColor = Color.White;
            panel3.BackColor = Color.White;
            panel4.BackColor = Color.DarkGray;
            panel5.BackColor = Color.White;
            panel6.BackColor = Color.White;

            transferGearbox = new TransferGearbox4Case(mainForm.car, mainForm.gearwheelPair1, mainForm.gearwheelPair2, mainForm.differential);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            panel1.BackColor = Color.White;
            panel2.BackColor = Color.White;
            panel3.BackColor = Color.White;
            panel4.BackColor = Color.White;
            panel5.BackColor = Color.DarkGray;
            panel6.BackColor = Color.White;

            transferGearbox = new TransferGearbox5Case(mainForm.car, mainForm.gearwheelPair1, mainForm.gearwheelPair2, mainForm.differential);
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            panel1.BackColor = Color.White;
            panel2.BackColor = Color.White;
            panel3.BackColor = Color.White;
            panel4.BackColor = Color.White;
            panel5.BackColor = Color.White;
            panel6.BackColor = Color.DarkGray;

            transferGearbox = new TransferGearbox6Case(mainForm.car, mainForm.gearwheelPair1, mainForm.gearwheelPair2, mainForm.differential);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainForm.transferGearbox = transferGearbox;
            mainForm.calcAllParts();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
