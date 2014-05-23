using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNTU_project
{
    public abstract class Differential: ParentElement
    {
        protected double _d_korp; //диаметр корпуса
        protected double _d_val; //средний диаметр выходных валов
        protected double _d_kor; //делительный диаметр коронного колеса
        protected double _d_sun; //делительный диаметр солнечного колеса
        protected double _d_sat; //делительный диаметр сателлита
        protected double _aw_sat; //межосевое расстояние от сателлитов до оси вых. валов
        protected double _aw_dif; //межосевое расстояние от вых. валов до промежуточного вала
        protected double _b_sun; //ширина солнечной шестерни
        protected double _b_sat; //ширина сателлитов
        protected double _l1; //длина корпуса l1
        protected double _l2; //длина корпуса l2
        protected double _M0_dif; //крутящий момент приведенный к дифференциалу
        protected double _s; //толщина стенок корпуса дифференциала
        protected double _gamma_p = 1.1; //коофициент неравномерности
        protected int _n_sat = 3; //количество сателлитов
        protected double _M1_d; //момент на 1 выходном валу
        protected double _M2_d; //момент на 2 выходном валу
        protected double _i_d; //внутреннее передаточное число дифференциала
        protected double _m_dif; //масса дифференциала

        //объемы
        protected double _V1;
        protected double _V3;
        protected double _V5;
        protected double _V7;
        protected double _V9;
        protected double _V11;
        protected double _V13;
        protected double _V_dif;

        public abstract void calc_allStep1(Car car, TransferGearbox transferGearbox);
        public abstract void calc_allStep2();

        public Differential()
        {
            this._outputPropertyList = new List<string>();
            this._inputPropertyList = new List<string>();

            inputPropertyList.AddRange(new String[] {
                        "Коэффициент неравномерности",
                        "Количество сателлитов"
            });

            outputPropertyList.AddRange(new String[] {
                        "Диаметр корпуса",
                        "Средний диаметр выходных валов",
                        "Делительный диаметр коронного колеса",
                        "Делительный диаметр солнечного колеса",
                        "Делительный диаметр сателлита",
                        "Межосевое расстояние от сателлитов до оси вых. валов",
                        "Межосевое расстояние от вых. валов до промежуточного вала",
                        "Ширина солнечной шестерни",
                        "Ширина сателлитов",
                        "Длина 1 корпуса дифференциала",
                        "Длина 2 корпуса дифференциала",
                        "Крутящий момент, приведенный к дифференциалу",
                        "Толщина стенок корпуса дифференциала",
                        "Момент на 1 выходном валу",
                        "Момент на 2 выходном валу",
                        "Внутреннее передаточное число дифференциала",
                        "Масса дифференциала"
            });
        }

        public override void setByName(string name, object value)
        {
            switch (name)
            {
                case "Коэффициент неравномерности":
                    _gamma_p = Convert.ToDouble(value);
                    break;
                case "Количество саттелитов":
                    _n_sat = Convert.ToInt32(value);
                    break;
            }
        }

        public override object getByName(string name)
        {
            switch (name)
            {
                case "Диаметр корпуса":
                    return _d_korp;
                case "Средний диаметр выходных валов":
                    return _d_val;
                case "Делительный диаметр коронного колеса":
                    return _d_kor;
                case "Делительный диаметр солнечного колеса":
                    return _d_sun;
                case "Делительный диаметр сателлита":
                    return _d_sat;
                case "Межосевое расстояние от сателлитов до оси вых. валов":
                    return _aw_sat;
                case "Межосевое расстояние от вых. валов до промежуточного вала":
                    return _aw_dif;
                case "Ширина солнечной шестерни":
                    return _b_sun;
                case "Ширина сателлитов":
                    return _b_sat;
                case "Длина 1 корпуса дифференциала":
                    return _l1;
                case "Длина 2 корпуса дифференциала":
                    return _l2;
                case "Крутящий момент, приведенный к дифференциалу":
                    return _M0_dif;
                case "Толщина стенок корпуса дифференциала":
                    return _s;
                case "Момент на 1 выходном валу":
                    return _M1_d;
                case "Момент на 2 выходном валу":
                    return _M2_d;
                case "Внутреннее передаточное число дифференциала":
                    return _i_d;
                case "Масса дифференциала":
                    return _m_dif;

                default:
                    return null;
            }
        }


        //расчет объемов
        public void calc_V1()
        {
            _V1 = Constants.pi / 8 * (_l2 - _l1) * (Math.Pow(( _d_val + 2 * _s), 2) - Math.Pow(_d_val, 2));
        }

        public void calc_V3()
        {
            _V3 = Constants.pi / 4 * _s * (Math.Pow(_d_korp, 2) - Math.Pow(_d_val, 2));
        }

        public void calc_V5()
        {
            _V5 = Constants.pi / 4 * _b_sun * (Math.Pow(_d_sun, 2) - Math.Pow(_d_val, 2));
        }

        public void calc_V7()
        {
            _V7 = Constants.pi / 4 * (_l1 - 3 * _s) * (Math.Pow(_d_korp, 2) - Math.Pow((_d_korp - 4 * _s), 2));
        }

        public void calc_V9()
        {
            _V9 = Constants.pi / 2 * _s * (Math.Pow(_d_korp, 2) - Math.Pow(_d_val, 2)); 
        }

        public void calc_V11()
        {
            _V11 = Constants.pi / 8 * (_l2 - _l1) * (Math.Pow((_d_val + 4 * _s), 2) - Math.Pow(_d_val, 2));
        }

        public void calc_V13()
        {
            _V13 = Constants.pi / 4 * Math.Pow(_d_sat, 2) * _b_sat;
        }

        public void calc_V_dif()
        {
            _V_dif = _V1 + _V3 + _V5 + _V7 + _V9 + _V11 + _n_sat * _V13;
        }

        public void calc_m_dif()
        {
            _m_dif = _V_dif * Constants.rho_steel * Math.Pow(10, -9);
        }

        //Property

        public double V_dif
        {
            get { return _V_dif; }
        }

        public double m_dif
        {
            get { return _m_dif; }
        }

        public double l1
        {
            get { return _l1; }
        }

        public double l2
        {
            get { return _l2; }
        }

        public double gamma_p
        {
            get { return _gamma_p; }
            set { _gamma_p = value; }
        }

        public int n_sat
        {
            get { return _n_sat; }
            set { _n_sat = value; }
        }

        public double d_korp
        {
            get { return _d_korp; }
        }

        public double d_val
        {
            get { return _d_val; }
        }

        public double d_kor
        {
            get { return _d_kor; }
        }

        public double d_sun
        {
            get { return _d_sun; }
        }

        public double d_sat
        {
            get { return _d_sat; }
        }

        public double aw_sat
        {
            get { return _aw_sat; }
        }

        public double aw_dif
        {
            get { return _aw_dif; }
        }

        public double b_sun
        {
            get { return _b_sun; }
        }

        public double b_sat
        {
            get { return _b_sat; }
        }

        public double M0_dif
        {
            get { return _M0_dif; }
        }

        public double s
        {
            get { return _s; }
        }

        public double M1_d
        {
            get { return _M1_d; }
        }

        public double M2_d
        {
            get { return _M2_d; }
        }

        public double i_d
        {
            get { return _i_d; }
        }

    }
    
}
