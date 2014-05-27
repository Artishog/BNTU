using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNTU_project
{
    public class ModelState
    {
        private Form1 mainForm;

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
        
         public ModelState(Form1 mainForm)
        {
            this.mainForm = mainForm;
            this._car = (Car)mainForm.car.ShallowCopy();
            this._gearwheel = (Gearwheel)mainForm.gearwheel.ShallowCopy();
            this._gearwheelPair1 = (GearwheelPair)mainForm.gearwheelPair1.ShallowCopy();
            this._gearwheelPair2 = (GearwheelPair)mainForm.gearwheelPair2.ShallowCopy();
            this._differential = (Differential)mainForm.differential.ShallowCopy();
            this._transferGearbox = (TransferGearbox)mainForm.transferGearbox.ShallowCopy();

            this._transferGearbox.Car = this._car;
            this._transferGearbox.GearwheelPair1 = this._gearwheelPair1;
            this._transferGearbox.GearwheelPair2 = this._gearwheelPair2;
            this._transferGearbox.Differential = this._differential;
        }

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
         public Gearwheel gearwheel
         {
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

    }
}
