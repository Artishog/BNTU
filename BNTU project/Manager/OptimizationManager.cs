using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BNTU_project
{
    public class OptimizationManager
    {
        private Form1 mainForm;
        private ModelState currentState;

        public OptimizationManager(Form1 mainForm)
        {
            this.mainForm = mainForm;
            calcModel();
        }

        public void startOptimizationCicle()
        {
            currentState = new ModelState(mainForm);

            mainForm.gearwheel.coef_bw = 0.19;
            while (mainForm.gearwheel.coef_bw <= 0.22)
            {
                mainForm.transferGearbox.Ka = 8.5;
                while (mainForm.transferGearbox.Ka <= 9.6)
                {
                    mainForm.gearwheel.mn = 3.75;
                    while (mainForm.gearwheel.mn <= 6.5)
                    {
                        var isValid = optimizationIteration();

                        mainForm.gearwheel.mn = mainForm.gearwheel.mn + 0.25;
                    }

                    mainForm.transferGearbox.Ka = mainForm.transferGearbox.Ka + 0.1;
                }

                mainForm.gearwheel.coef_bw = mainForm.gearwheel.coef_bw + 0.01;
            }

            rewriteCurrentModel();
        }

        private bool optimizationIteration()
        {
            var isValidModel = true;

            calcModel();

            if (!mainForm.contact.isValid(mainForm.car.L0))
                isValidModel = false;

            if (!mainForm.flexion.isValid(mainForm.car.L0))
                isValidModel = false;

            if (!mainForm.endurance.isValid(mainForm.steel.currentSteel.PHlimM, mainForm.steel.currentSteel.sigmaFlimM))
                isValidModel = false;

            if (!mainForm.gearwheelPair1.isValid())
                isValidModel = false;

            if (isValidModel)
            {
               // MessageBox.Show("yay?");
            }

            if (isValidModel && (mainForm.transferGearbox.mrk < currentState.transferGearbox.mrk))
            {
                currentState = new ModelState(mainForm);
                return true;
            }

            return false;
        }

        private void calcModel()
        {
            mainForm.calcAllParts();

            if (mainForm.transferGearbox.needRecalculation)
            {
                mainForm.transferGearbox.aw1 = mainForm.transferGearbox.aw1 * (mainForm.differential.d_korp + 3) / mainForm.gearwheelPair2.df_kol;
                mainForm.transferGearbox.aw2 = mainForm.transferGearbox.aw2 * (mainForm.differential.d_korp + 3) / mainForm.gearwheelPair2.df_kol;

                mainForm.calcAllParts();
            }

            mainForm.calcCheckingClasses();
        }

        private void rewriteCurrentModel()
        {
            mainForm.car = (Car)currentState.car.ShallowCopy();
            mainForm.gearwheel = (Gearwheel)currentState.gearwheel.ShallowCopy();
            mainForm.gearwheelPair1 = (GearwheelPair)currentState.gearwheelPair1.ShallowCopy();
            mainForm.gearwheelPair2 = (GearwheelPair)currentState.gearwheelPair2.ShallowCopy();
            mainForm.differential = (Differential)currentState.differential.ShallowCopy();
            mainForm.transferGearbox = (TransferGearbox)currentState.transferGearbox.ShallowCopy();

            mainForm.transferGearbox.Car = mainForm.car;
            mainForm.transferGearbox.GearwheelPair1 = mainForm.gearwheelPair1;
            mainForm.transferGearbox.GearwheelPair2 = mainForm.gearwheelPair2;
            mainForm.transferGearbox.Differential = mainForm.differential;

            calcModel();
        }
    }
}
