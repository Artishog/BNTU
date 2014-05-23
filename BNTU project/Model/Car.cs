using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNTU_project
{
    public class Car: ParentElement
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
        private int _K; //количество передач КПП
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
            _G_fi = 313920;
            _m1 = 6150;
            _m2 = 10000;
            _vehicleType = "Грузовой";

            _Pemax = 243000;
            _np = 2100;
            _nm = 1300;
            _K = 5;
            _Vamax = 100;
            _Ukp = 16.22;

            this._inputPropertyList = new List<string>();
            inputPropertyList.AddRange(new String[] {
                        "Полная масса автомобиля",
                        "Масса, приходящаяся на переднюю ось",
                        "Масса, приходящаяся на заднюю ось (тележку)",
                        "Сцепной вес автомобиля",
                        "Максимальная мощность",
                        "Частота при максимальной мощности",
                        "Максимальный крутящий момент",
                        "Частота при максимальном крутящем моменте",
                        "Количество передач КПП",
                        "Радиус качения колеса",
                        "Максимальная скорость автомобиля",
                        "Гарантированный пробег",
                        "Передаточное число главное передачи",
                        "Передаточное число i-й передачи КПП",
                        "Условие преодоления максимального сопротивления дороги",
                        "КПД трансмиссии",
                        "Условие отсутствия буксования",
                        "Передаточное число низшей ступени РК"
            });
            
        }

        public override object getByName(string name)
        {
            switch (name)
            {
                case "Полная масса автомобиля":
                    return _ma;
                    break;
                case "Масса, приходящаяся на переднюю ось":
                    return _m1;
                    break;
                case "Масса, приходящаяся на заднюю ось (тележку)":
                    return _m2;
                    break;
                case "Сцепной вес автомобиля":
                    return _G_fi;
                    break;
                case "Максимальная мощность":
                    return _Pemax;
                    break;
                case "Частота при максимальной мощности":
                    return _np;
                    break;
                case "Максимальный крутящий момент":
                    return _Memax;
                    break;
                case "Частота при максимальном крутящем моменте":
                    return _nm;
                    break;
                case "Количество передач КПП":
                    return _K;
                    break;
                case "Радиус качения колеса":
                    return _r0;
                    break;
                case "Максимальная скорость автомобиля":
                    return _Vamax;
                    break;
                case "Гарантированный пробег":
                    return _L0;
                    break;
                case "Передаточное число главное передачи":
                    return _U0;
                    break;
                case "Передаточное число i-й передачи КПП":
                    return _Ukp;
                    break;
                case "Условие преодоления максимального сопротивления дороги":
                    return _Urk_psi;
                    break;
                case "КПД трансмиссии":
                    return _kpd_tr;
                    break;
                case "Условие отсутствия буксования":
                    return _Urk_fi;
                    break;
                case "Передаточное число низшей ступени РК":
                    return _Urk;
                    break;

                default:
                    return null;
                    break;
            }
        }

        public override void setByName(string name, object value)
        {
            switch (name)
            {
                case "Полная масса автомобиля":
                    _ma = Convert.ToInt32(value);
                    break;
                case "Масса, приходящаяся на переднюю ось":
                    _m1 = Convert.ToDouble(value);
                    break;
                case "Масса, приходящаяся на заднюю ось (тележку)":
                    _m2 = Convert.ToDouble(value);
                    break;
                case "Сцепной вес автомобиля":
                    _G_fi = Convert.ToInt32(value);
                    break;
                case "Максимальная мощность":
                    _Pemax = Convert.ToInt32(value);
                    break;
                case "Частота при максимальной мощности":
                    _np = Convert.ToInt32(value);
                    break;
                case "Максимальный крутящий момент":
                    _Memax = Convert.ToInt32(value);
                    break;
                case "Частота при максимальном крутящем моменте":
                    _nm = Convert.ToInt32(value);
                    break;
                case "Количество передач КПП":
                    _K = Convert.ToInt32(value);
                    break;
                case "Радиус качения колеса":
                    _r0 = Convert.ToDouble(value);
                    break;
                case "Максимальная скорость автомобиля":
                    _Vamax = Convert.ToInt32(value);
                    break;
                case "Гарантированный пробег":
                    _L0 = Convert.ToInt32(value);
                    break;
                case "Передаточное число главное передачи":
                    _U0 = Convert.ToDouble(value);
                    break;
                case "Передаточное число i-й передачи КПП":
                    _Ukp = Convert.ToDouble(value);
                    break;
                case "Условие преодоления максимального сопротивления дороги":
                    _Urk_psi = Convert.ToDouble(value);
                    break;
                case "КПД трансмиссии":
                    _kpd_tr = Convert.ToDouble(value);
                    break;
                case "Условие отсутствия буксования":
                    _Urk_fi = Convert.ToDouble(value);
                    break;
                case "Передаточное число низшей ступени РК":
                    _Urk = Convert.ToDouble(value);
                    break;
            }
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
            _Urk_fi = (_G_fi * Constants.fi * _r0) / (_Memax * _U0 * _kpd_tr * _Ukp);
            _Urk = (_Urk_fi + _Urk_psi) / 2;
        }

        private void setDefaultGearsToUkpTable()
        {
            _gearsToUkp.add(1, _Ukp);
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
