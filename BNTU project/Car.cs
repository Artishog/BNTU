using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNTU_project
{
    public class Car
    {
        private int _ma; //полная масса автомобиля
        private double _m1; //масса приходящаяся на переднюю ось
        private double _m2; //масса приходящаяся на заднюю ось(тележку)
        private int _Pemax; //максимальная мощность
        private int _Memax; //максимальный крутящий момент
        private int _np; //частота при максимальной мощности
        private int _nm; //частота при максимальном крутящем моменте
        private int _Vamax; //максимальная скорость автомобиля
        private string _vehicleType; //тип автомобиля (легковой, грузовой, автобус городской, автобус междугородний, самосвал, многоприводный автомобиль)
        private int _L0 = 1000000; //гарантированный пробег
        private int _K; //количество передач
        private double _r0; //радиус качения колеса
        private double _U0; //передаточное число главной передачи
        private double _Ukp; //передаточное число i-ой передачи КПП
        private double _Urk_psi;
        private double _kpd_tr = 0.85; //КПД трансмиссии
        private double _Urk_fi;
        private double _Urk; //передаточное число низшей ступени 
        private int _G_fi = -1; //сцепной вес автомобиля (ma*g)
        private GearsAndUkpTable _gearsToUkp = new GearsAndUkpTable(); //таблица связи между gears и Uikp  

        public Car()
        {
            _ma = 32000;
            _r0 = 0.524;
            //_Memax = 1275;
            _Memax = 1300;
            _U0 = 4;
            _G_fi = 16150;
            _m1 = 6150;
            _m2 = 10000;
            _vehicleType = "Легковой";
            
        }

        //Вычисление параметров

        public void calc_All()
        {
            setDefaultGearsToUkpTable();
            calc_Urk_psi();
            calc_G_fi();
            calc_Urk();
        }

        private void calc_Urk_psi()
        {
            //var Ukp1 = 16.22; //Надо будет брать из таблицы
            try
            {
                var Ukp1 = gearsToUkp.getUkpByGear(1);
                _Urk_psi = (_ma * Constants.g * Constants.psi * _r0) / (_Memax * Ukp1 * _U0 * _kpd_tr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void calc_G_fi()
        {
            if (_G_fi == -1)
                _G_fi = (int)(_ma * Constants.g);
        }

        private void calc_Urk()
        {
            _Urk_fi = (_G_fi * Constants.fi * _r0) / (_Memax * _U0 * _kpd_tr);
            _Urk = (_Urk_fi + _Urk_psi) / 2;
        }

        private void setDefaultGearsToUkpTable()
        {
            _gearsToUkp.add(1, 16.22);
        }

        //Property
        public double Urk_psi
        {
            get { return _Urk_psi; }
            set { _Urk_psi = value; }
        }

        public double Urk_fi
        {
            get { return _Urk_fi; }
            set { _Urk_fi = value; }
        }

        public int G_fi
        {
            get { return _G_fi; }
            set { _G_fi = value; }
        }

        public double Urk
        {
            get { return _Urk; }
            set { _Urk = value; }
        }

        public int Memax
        {
            get { return _Memax; }
            set { _Memax = value; }
        }

        public GearsAndUkpTable gearsToUkp
        {
            get { return _gearsToUkp; }
        }

        public double m1
        {
            get { return _m1; }
            set { _m1 = value; }
        }

        public double m2
        {
            get { return _m2; }
            set { _m2 = value; }
        }

        public int Pemax
        {
            get { return _Pemax; }
            set { _Pemax = value; }
        }

        public int np
        {
            get { return _np; }
            set { _np = value; }
        }

        public int ma
        {
            get { return _ma; }
            set { _ma = value; }
        }

        public int nm
        {
            get { return _nm; }
            set { _nm = value; }
        }

        public int K
        {
            get { return _K; }
            set { _K = value; }
        }

        public double r0
        {
            get { return _r0; }
            set { _r0 = value; }
        }

        public int Vamax
        {
            get { return _Vamax; }
            set { _Vamax = value; }
        }

        public int L0
        {
            get { return _L0; }
            set { _L0 = value; }
        }

        public double U0
        {
            get { return _U0; }
            set { _U0 = value; }
        }

        public double Ukp
        {
            get { return _Ukp; }
            set { _Ukp = value; }
        }

        public double kpd_tr
        {
            get { return _kpd_tr; }
            set { _kpd_tr = value; }
        }

        public string vehicleType
        {
            get { return _vehicleType; }
            set { _vehicleType = value; }
        }
    }
}
