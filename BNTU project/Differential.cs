using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNTU_project
{
    public class Differential
    {
        private double _d_korp; //диаметр корпуса
        private double _d_val; //средний диаметр выходных валов
        private double _d_kor; //делительный диаметр коронного колеса
        private double _d_sun; //делительный диаметр солнечного колеса
        private double _d_sat; //делительный диаметр сателлита
        private double _aw_sat; //межосевое расстояние от сателлитов до оси вых. валов
        private double _aw_dif; //межосевое расстояние от вых. валов до промежуточного вала
        private double _b_sun; //ширина солнечной шестерни
        private double _b_sat; //ширина сателлитов
        private double _l1; //длина корпуса l1
        private double _l2; //длина корпуса l2
        private double _M0_dif; //крутящий момент приведенный к дифференциалу
        private double _s; //толщина стенок корпуса дифференциала
        private double _gamma_p = 1.1; //коофициент неравномерности
        private int _n_sat = 3; //количество сателлитов
        private double _M1_d; //момент на 1 выходном валу
        private double _M2_d; //момент на 2 выходном валу
        private double _i_d; //внутреннее передаточное число дифференциала
        private double _m_dif; //масса дифференциала

        //объемы
        private double _V1;
        private double _V3;
        private double _V5;
        private double _V7;
        private double _V9;
        private double _V11;
        private double _V13;
        private double _V_dif;

        //Рассчет параметров
        public void calc_all(int Memax, double Ukp1, double Urk, double m1, double m2, double Ka)
        {
            calc_M0(Memax, Ukp1, Urk);
            calc_i_d(m1, m2);
            calc_M2_d();
            calc_M1_d();
            calc_aw_dif(Ka);
            calc_d_kor();
            calc_d_sun();
            calc_d_sat();
            calc_b_sat();
            calc_b_sun();
            calc_s();
            calc_d_korp();
            calc_d_val();
            calc_l1();
            calc_l2();
            calc_V1();
            calc_V3();
            calc_V5();
            calc_V7();
            calc_V9();
            calc_V11();
            calc_V13();
            calc_V_dif();
            calc_m_dif();
        }

        //потом будет параметров не урк а чтото другое
        public void calc_M0(int Memax, double Ukp1, double U1st)
        {
            _M0_dif = Memax * Ukp1 * U1st;
        }

        public void calc_aw_dif(double Ka)
        {
            _aw_dif = Ka * Math.Pow(((_M0_dif * _gamma_p) / _n_sat), 1.0 / 3.0);  
        }

        public void calc_i_d(double m1, double m2)
        {
            double G1;
            double G2;
            G1 = m1 * Constants.g * Constants.fi;
            G2 = m2 * Constants.g * Constants.fi;
            _i_d = G2 / G1;
        }

        public void calc_M2_d()
        {
            _M2_d = _M0_dif / (_i_d + 1);
        }

        public void calc_M1_d()
        {
            _M1_d = _M0_dif - _M2_d;
        }

        public void calc_d_sun()
        {
            _d_sun = _d_kor / _i_d;
        }

        public void calc_d_kor()
        {
            _d_kor = 0.86 * _aw_dif;
        }

        public void calc_d_sat()
        {
            _d_sat = (_d_kor - _d_sun) / 2;
        }

        public void calc_b_sat()
        {
            _b_sat = 0.19 * _aw_dif;
        }

        public void calc_b_sun()
        {
            _b_sun = 0.26 * _aw_dif;
        }

        public void calc_l1()
        {
            _l1 = 0.54 * _aw_dif;
        }

        public void calc_l2()
        {
            _l2 = 1.2 * _aw_dif;
        }

        public void calc_s()
        {
            _s = 0.06 * _aw_dif;
        }

        public void calc_d_val()
        {
            _d_val = 0.3 * _aw_dif;
        }

        public void calc_d_korp()
        {
            _d_korp = _d_kor + 4 * _s;
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
    }
}
