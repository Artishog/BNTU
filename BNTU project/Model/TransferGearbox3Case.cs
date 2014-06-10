using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNTU_project
{
    public class TransferGearbox3Case : TransferGearbox
    {
        public TransferGearbox3Case(Car car, GearwheelPair gearwheelPair1, GearwheelPair gearwheelPair2, Differential differential)
            : base(car, gearwheelPair1, gearwheelPair2, differential)
        {
            //_aw1 = 263.4;
            _Ka = 9;
            _kinematicScheme = 3;
        }

        //Рассчет параметров
        public void checkCondition()
        {
            if (gearwheelPair2.df_kol < differential.d_korp + 3)
            {
                _needRecalculation = true;
            }
            else
            {
                _needRecalculation = false;
            }
        }

        public override void calc_allStep1()
        {
            calc_U1st(car.Urk);
            calc_U2st(car.Urk);
            calc_M0(car.Memax, car.gearsToUkp.getUkpByGear(1));
            if (!_needRecalculation)
                calc_aw1();
        }

        public override void calc_allStep2()
        {
            this._U_d1 = gearwheelPair1.U_d;
            this._U_d2 = gearwheelPair2.U_d;

            calc_L1(gearwheelPair1.bf_shest, differential.l1, gearwheelPair1.bw);
            calc_L2();
        }

        public override void calc_allStep3()
        {
            if (!_needRecalculation)
                calc_aw2withoutDifferential(gearwheelPair2.aw2_d);
            calc_H1(gearwheelPair1.d_shest, gearwheelPair1.d_kol, gearwheelPair2.d_kol);
            calc_H2();
            calc_B1(gearwheelPair2.d_kol);
            calc_B2();
            calc_ld(differential.l2, gearwheelPair2.bw);
            calc_m_korp();
            calc_d1();
            calc_d2();
            calc_d3();
            calc_mv1();
            calc_mv2();
            calc_mv3();
            calc_msh1(gearwheelPair1.d_shest, gearwheelPair1.bw);
            calc_msh2(gearwheelPair1.d_kol, gearwheelPair1.bf_shest);
            calc_msh3(gearwheelPair2.d_kol, gearwheelPair1.bw, differential.d_korp);
            calc_mrk(differential.m_dif);
            checkCondition();
        }

        public void calc_U1st(double Urk)
        {
            _U1st = Math.Sqrt(Urk);
        }

        public void calc_U2st(double Urk)
        {
            _U2st = Math.Sqrt(Urk);
        }

        public void calc_M0(int Memax, double Ukp1)
        {
            _M0 = Memax * Ukp1;
        }

        public void calc_aw1()
        {
            _aw1 = _Ka * Math.Pow(_M0, 1.0 / 3.0);
        }

        public void calc_aw2withoutDifferential(double aw2_d)
        {
            //_aw2 = _Ka * Math.Pow((_M0 * _U1st), 1.0 / 3.0);
            _aw2 = aw2_d; //для однорядной раздаточной коробки (с выносом)
        }

        public void calc_aw2withDifferential(double aw_dif)
        {
            _aw2 = aw_dif;
        }

        public void calc_H1(double d_shest1, double d_kol1, double d_kol2)
        {
            _H1 = _aw1 + d_shest1 / 2 + _aw2 +  d_kol2 / 2 + 10;
        }

        public void calc_H2()
        {
            _H2 = _H1 + 2 * _delta;
        }

        public void calc_B1(double d_kol2)
        {
            _B1 = d_kol2 + 10;
        }

        public void calc_B2()
        {
            _B2 = _B1 + 2 * _delta;
        }

        public void calc_ld(double l2, double bw2) //требует доработки
        {
            _ld = l2 + bw2;
        }

        public void calc_L1(double bf_shest1, double l1, double bw1)
        {
            _L1 = l1 + 10;
        }

        public void calc_L2() //требует доработки
        {
            _L2 = _L1 + 2 * delta;
        }

        public void calc_m_korp()
        {
            _m_korp = (_L2 * _B2 * _H2 - _L1 * _B1 * _H1) * Constants.rho_chug * Math.Pow(10, -9);
        }

        public void calc_d1()
        {
            _d1 = 0.41 * _aw1;
        }

        public void calc_d2()
        {
            _d2 = 0.37 * _aw1;
        }

        public void calc_d3()
        {
            _d3 = 0.35 * _aw2;
        }

        public void calc_mv1()
        {
            _mv1 = (Constants.pi * _d1 * _d1 * L2 / 4) * Constants.rho_steel * Math.Pow(10, -9);
        }

        public void calc_mv2()
        {
            _mv2 = (Constants.pi * _d2 * _d2 * L2 / 4) * Constants.rho_steel * Math.Pow(10, -9);
        }

        public void calc_mv3()
        {
            _mv3 = (Constants.pi * _d3 * _d3 * L2 / 4) * Constants.rho_steel * Math.Pow(10, -9);
        }

        public void calc_msh1(double d_shest1, double bw1)
        {
            _msh1 = Constants.pi * bw1 * (d_shest1 * d_shest1 - d1 * d1) * Constants.rho_steel * Math.Pow(10, -9) / 4;
        }

        public void calc_msh2(double d_kol1, double bf_shest1)
        {
            _msh2 = Constants.pi * bf_shest1 * (d_kol1 * d_kol1 - d2 * d2) * Constants.rho_steel * Math.Pow(10, -9) / 4;
        }

        public void calc_msh3(double d_kol2, double bw1, double d_korp)
        {
            _msh3 = Constants.pi * bw1 * (d_kol2 * d_kol2 - d_korp * d_korp) * Constants.rho_steel * Math.Pow(10, -9) / 4;
        }

        public void calc_mrk(double m_dif)
        {
            _mrk = _m_korp + _msh1 + _msh2 + _msh3 + _mv1 + _mv2 + _mv3 + m_dif + _mupr;
        }

    }
}

