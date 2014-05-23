using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace BNTU_project
{
    public partial class DependenceGraphicForm : Form
    {
        private Form1 mainForm;

        private Car currentCar;
        private GearwheelPair currentGearwheelPair1;
        private GearwheelPair currentGearwheelPair2;
        private Gearwheel currentGearwheel;
        private TransferGearbox currentTransferGearbox;
        private Differential currentDifferential;

        private ParentElement elementX;
        private ParentElement elementY;
        private String nameX;
        private String nameY;
        private double minValue;
        private double maxValue;
        private double step;

        public DependenceGraphicForm(Form1 mainForm)
        {
            this.mainForm = mainForm;
            this.currentCar = (Car)mainForm.car.ShallowCopy();
            this.currentGearwheel = (Gearwheel)mainForm.gearwheel.ShallowCopy();
            this.currentGearwheelPair1 = (GearwheelPair)mainForm.gearwheelPair1.ShallowCopy();
            this.currentGearwheelPair2 = (GearwheelPair)mainForm.gearwheelPair2.ShallowCopy();
            this.currentDifferential = (Differential)mainForm.differential.ShallowCopy();
            this.currentTransferGearbox = (TransferGearbox)mainForm.transferGearbox.ShallowCopy();

            this.currentTransferGearbox.Car = this.currentCar;
            this.currentTransferGearbox.GearwheelPair1 = this.currentGearwheelPair1;
            this.currentTransferGearbox.GearwheelPair2 = this.currentGearwheelPair2;
            this.currentTransferGearbox.Differential = this.currentDifferential;

            InitializeComponent();

            comboBox1.Items.AddRange(new object[] {
                        "Раздаточная коробка",
                        "Первая пара",
                        "Вторая пара",
                        "Дифференциал"
            });

            comboBox3.Items.AddRange(new object[] {
                        "Автомобиль",
                        "Зубчатый венец",
                        "Дифференциал",
                        "Раздаточная коробка"
            });

            comboBox1.SelectedItem = comboBox1.Items[0];
            comboBox2.SelectedItem = comboBox2.Items[0];
            comboBox3.SelectedItem = comboBox3.Items[0];
            comboBox4.SelectedItem = comboBox4.Items[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<double> x = new List<double>();
            List<double> y = new List<double>();
            
            elementX.setByName(nameX, minValue);
            double valueX = minValue;

            while (valueX < maxValue)
            {
                valueX = Convert.ToDouble(elementX.getByName(nameX)) + step;
                elementX.setByName(nameX, valueX);
                calcAllParts();
                double valueY = Convert.ToDouble(elementY.getByName(nameY));

                x.Add(valueX);
                y.Add(valueY);
            }

            this.DependenceGraphic.Series.Clear();
            this.DependenceGraphic.Series.Add("График");
            this.DependenceGraphic.Series["График"].ChartType = SeriesChartType.Spline;
            this.DependenceGraphic.Series["График"].Points.DataBindXY(x, y);

           
        }

        private void calcAllParts()
        {
            //Car parameters
            this.currentCar.calc_All();
            //textBox1.Text = _currentCar.Memax.ToString();

            //Transfer gearbox parameters
            currentTransferGearbox.calc_allStep1();
            //currentTransferGearbox.aw1 = 263.4;

            //GearwhellPair1 parameters
            this.currentGearwheelPair1.calc_FirstPair(currentTransferGearbox.aw1, this.currentGearwheel.beta, this.currentGearwheel.mn, this.currentGearwheel.ha_star, this.currentGearwheel.hf_star, this.currentGearwheel.c_star, this.currentGearwheel.coef_bw, currentTransferGearbox.U1st);

            //GearwhellPair2 parameters
            this.currentGearwheelPair2.calc_SecondPair(this.currentGearwheelPair1, this.currentGearwheel.beta, this.currentGearwheel.mn, this.currentGearwheel.ha_star, this.currentGearwheel.hf_star, this.currentGearwheel.c_star, this.currentCar.Urk, currentTransferGearbox.U2st);

            //Differential parameters
            currentDifferential.calc_allStep1(this.currentCar, this.currentTransferGearbox);

            //Transfer gearbox parameters
            currentTransferGearbox.calc_allStep2();

            currentDifferential.calc_allStep2();

            currentTransferGearbox.calc_allStep3();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem.ToString())
            {
                case "Раздаточная коробка":
                    elementY = currentTransferGearbox;
                     comboBox2.Items.Clear();
                    foreach (var propertyName in currentTransferGearbox.outputPropertyList)
                        comboBox2.Items.Add(propertyName);
                    break;
                case "Первая пара":
                    elementY = currentGearwheelPair1;
                    comboBox2.Items.Clear();
                    foreach (var propertyName in currentGearwheelPair1.outputPropertyList)
                        comboBox2.Items.Add(propertyName);
                    break;
                case "Вторая пара":
                    elementY = currentGearwheelPair2;
                    comboBox2.Items.Clear();
                    foreach (var propertyName in currentGearwheelPair2.outputPropertyList)
                        comboBox2.Items.Add(propertyName);
                    break;
                case "Дифференциал":
                    elementY = currentDifferential;
                    comboBox2.Items.Clear();
                    foreach (var propertyName in currentDifferential.outputPropertyList)
                        comboBox2.Items.Add(propertyName);
                    break;
               
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox3.SelectedItem.ToString())
            {
                case "Автомобиль":
                    elementX = currentCar;
                    comboBox4.Items.Clear();
                    foreach (var propertyName in currentCar.inputPropertyList)
                        comboBox4.Items.Add(propertyName);
                    break;
                case "Раздаточная коробка":
                    elementX = currentTransferGearbox;
                    comboBox4.Items.Clear();
                    foreach (var propertyName in currentTransferGearbox.inputPropertyList)
                        comboBox4.Items.Add(propertyName);
                    break;
                case "Зубчатый венец":
                    elementX = currentGearwheel;
                    comboBox4.Items.Clear();
                    foreach (var propertyName in currentGearwheel.inputPropertyList)
                        comboBox4.Items.Add(propertyName);
                    break;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            nameY = comboBox2.SelectedItem.ToString();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            nameX = comboBox4.SelectedItem.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                minValue = Convert.ToDouble(textBox1.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Проверьте правильности вводимых данных");
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                maxValue = Convert.ToDouble(textBox2.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Проверьте правильности вводимых данных");
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                step = Convert.ToDouble(textBox3.Text);
 }
            catch (Exception ex)
            {
                MessageBox.Show("Проверьте правильности вводимых данных");
            }
        }

    }
}
