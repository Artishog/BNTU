using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNTU_project
{
    public class Differential1Case: Differential
    {
        //Рассчет параметров
        public override void calc_allStep1(Car car, TransferGearbox transferGearbox)
        {
            calc_M0(car.Memax, car.gearsToUkp.getUkpByGear(1), transferGearbox.U1st);
            calc_i_d(car.m1, car.m2);
            calc_M2_d();
            calc_M1_d();
            calc_aw_dif(transferGearbox.Ka);
            calc_d_kor();
            calc_d_sun();
            calc_d_sat();
            calc_b_sat();
            calc_b_sun();
            calc_aw_sat();
            calc_s();
            calc_d_korp();
            calc_d_val();
            calc_l1();
        }

        public override void calc_allStep2()
        {
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

        public void calc_aw_sat()
        {
            _aw_sat = (_d_sat + _d_sun) / 2;
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
            _d_val = 0.35 * _aw_dif;
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

    }
}
