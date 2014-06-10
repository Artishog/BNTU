using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNTU_project
{
    public class TransferGearbox4Case : TransferGearbox
    {
        public TransferGearbox4Case(Car car, GearwheelPair gearwheelPair1, GearwheelPair gearwheelPair2, Differential differential)
            : base(car, gearwheelPair1, gearwheelPair2, differential)
        {
            //_aw1 = 263.4;
            _Ka = 9;
            _kinematicScheme = 4;
        }

        //Рассчет параметров
        public override void calc_allStep1()
        {
            calc_U1st(car.Urk);
            calc_U2st(car.Urk);
            calc_M0(car.Memax, car.gearsToUkp.getUkpByGear(1));
            calc_aw1();
        }

        public override void calc_allStep2()
        {
            this._U_d1 = gearwheelPair1.U_d;
            this._U_d2 = gearwheelPair2.U_d;

            calc_L1(gearwheelPair1.bf_shest, gearwheelPair1.bw);
            calc_L2();
        }

        public override void calc_allStep3()
        {
            calc_aw2withoutDifferential(gearwheelPair2.aw2_d);
            calc_H1(gearwheelPair1.d_shest, gearwheelPair1.d_kol, gearwheelPair2.d_kol);
            calc_H2();
            calc_B1(gearwheelPair1.d_kol);
            calc_B2();
            calc_m_korp();
            calc_d1();
            calc_d2();
            calc_d3();
            calc_mv1();
            calc_mv2();
            calc_mv3();
            calc_msh1(gearwheelPair1.d_shest, gearwheelPair1.bf_shest);
            calc_msh2(gearwheelPair1.d_kol, gearwheelPair1.bw);
            calc_msh3(gearwheelPair1.d_kol, gearwheelPair1.bw);
            calc_msh4(gearwheelPair1.d_kol, gearwheelPair1.bw);
            calc_msh5(gearwheelPair1.d_shest, gearwheelPair1.bf_shest);
            calc_mrk();
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
            _H1 = _aw1 + d_kol1 + _aw2 + 10;
        }

        public void calc_H2()
        {
            _H2 = _H1 + 2 * _delta;
        }

        public void calc_B1(double d_kol1)
        {
            _B1 = d_kol1 + 10;
        }

        public void calc_B2()
        {
            _B2 = _B1 + 2 * _delta;
        }

        public void calc_L1(double bf_shest, double bw1)
        {
            _L1 = bf_shest + 5 + bw1 / 2 + _Supr + bf_shest / 2 + 5;
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

        public void calc_msh1(double d_shest1, double bf_shest1)
        {
            _msh1 = Constants.pi * bf_shest1 * (d_shest1 * d_shest1 - d1 * d1) * Constants.rho_steel * Math.Pow(10, -9) / 4;
        }

        public void calc_msh2(double d_kol1, double bw1)
        {
            _msh2 = Constants.pi * bw1 * (d_kol1 * d_kol1 - d2 * d2) * Constants.rho_steel * Math.Pow(10, -9) / 4;
        }

        public void calc_msh3(double d_kol1, double bw1)
        {
            _msh3 = Constants.pi * bw1 * (d_kol1 * d_kol1 - d3 * d3) * Constants.rho_steel * Math.Pow(10, -9) / 4;
        }

        public void calc_msh4(double d_kol1, double bw1)
        {
            _msh4 = Constants.pi * bw1 * (d_kol1 * d_kol1 - d1 * d1) * Constants.rho_steel * Math.Pow(10, -9) / 4;
        }

        public void calc_msh5(double d_shest1, double bf_shest1)
        {
            _msh5 = Constants.pi * bf_shest1 * (d_shest1 * d_shest1 - d2 * d2) * Constants.rho_steel * Math.Pow(10, -9) / 4;
        }

        public void calc_mrk()
        {
            _mrk = _m_korp + _msh1 + _msh2 + _msh3 + _msh4 + _msh5 + _mv1 + _mv2 + _mv3 + _mupr;
        }
    }
}
