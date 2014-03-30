using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNTU_project
{
    public class TransferGearbox
    {
        private double _Ka; //кооэфициент межосевого расстояния [8.5 ... 9.6]
        private double _aw1; //межосевое расстояние входного и промежуточного валов
        private double _aw2; //межосевое расстояние промежуточного и выходных валов
        private double _U1st; //передаточное число первой ступени
        private double _U2st; //передаточное число второй ступени
        private double _L1; //длина внутреннего объема раздаточной коробки
        private double _L2; //длина раздаточной коробки
        private double _ld; //длина дифференциала
        private double _H1; //высота внутреннего периметра поперечного сечения
        private double _H2; //высота внешнего периметра поперечного сечения
        private double _delta = 0.7; //средняя толщина стенок картера
        private double _B1; //ширина внутреннего периметра поперечного сечения
        private double _B2; //ширина внешнего периметра поперечного сечения
        private double _dmax; //диаметр наибольшего колеса или корпуса дифференциала
        private double _M0; //крутящий момент на выходном валу коробки передач
        private double _V_korp; //объем корпуса раздаточной коробки
        private double _m_korp; //масса корпуса раздаточной коробки
        private double _d1; //диаметр входного вала
        private double _d2; //диаметр промежуточного вала
        private double _d3; //диаметр выходных валов
        private double _Vv1; //объем входного вала
        private double _Vv2; //объем промежуточного вала
        private double _Vv3; //объем выходных валов
        private double _mv1; //масса входного вала
        private double _mv2; //масса промежуточного вала
        private double _mv3; //масса выходных валов
        private double _Vsh1; //объем шестерни входного вала
        private double _Vsh2; //объем шестерни промежуточного вала
        private double _Vsh3; //объем шестерни выходного вала
        private double _msh1; //масса шестерни входного вала
        private double _msh2; //масса шестерни промежуточного вала
        private double _msh3; //масса шестерни выходного вала
        private double _mrk; //масса раздаточной коробки

        public TransferGearbox()
        {
            //_aw1 = 263.4;
            _Ka = 9;
        }

        //Рассчет параметров
        public void calc_allStep1(double Urk, double Ukp1, int Memax)
        {
            calc_U1st(Urk);
            calc_U2st(Urk);
            calc_M0(Memax, Ukp1);
            calc_aw1();
        }

        public void calc_allStep2(double d_shest1, double d_kol1, double d_kol2, double l2, double bw1, double bw2, double bf_shest1, double aw2_d, double m_dif)
        {
            calc_aw2withoutDifferential(aw2_d); 
            calc_H1(d_shest1, d_kol1, d_kol2);
            calc_H2();
            calc_B1(d_kol2);
            calc_B2();
            calc_ld(l2, bw2);
            calc_L2();
            calc_L1();
            calc_V_korp();
            calc_m_korp();
            calc_d1();
            calc_d2();
            calc_d3();
            calc_Vv1();
            calc_Vv2();
            calc_Vv3();
            calc_mv1();
            calc_mv2();
            calc_mv3();
            calc_Vsh1(bf_shest1, d_shest1);
            calc_Vsh2(bw1, d_kol1);
            calc_Vsh3(bw2, d_kol2);
            calc_msh1();
            calc_msh2();
            calc_msh3();
            calc_mrk(m_dif);
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
            _H1 = _aw1 + d_shest1 / 2 + d_kol1 + _aw2 + d_kol2 / _aw2 + 10;
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

        public void calc_L2() //требует доработки
        {
            _L2= _ld;
        }

        public void calc_ld(double l2, double bw2) //требует доработки
        {
            _ld = l2 + bw2;
        }

        public void calc_L1()
        {
            _L1 = _L2 - 2 * _delta;
        }

        public void calc_V_korp()
        {
            _V_korp = _H2 * _B2 * _L2 - _H1 * _B1 * _L1;
        }

        public void calc_m_korp()
        {
            _m_korp = _V_korp * Constants.rho_chug * Math.Pow(10, -9);
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

        public void calc_Vv1()
        {
            _Vv1 = Constants.pi * Math.Pow(_d1, 2) * _L2 / 4;
        }

        public void calc_Vv2()
        {
            _Vv2 = Constants.pi * Math.Pow(_d2, 2) * _L2 / 4;
        }

        public void calc_Vv3()
        {
            _Vv3 = Constants.pi * Math.Pow(_d3, 2) * _L2 / 4;
        }

        public void calc_mv1()
        {
            _mv1 = _Vv1 * Constants.rho_steel * Math.Pow(10, -9);
        }

        public void calc_mv2()
        {
            _mv2 = _Vv2 * Constants.rho_steel * Math.Pow(10, -9);
        }

        public void calc_mv3()
        {
            _mv3 = _Vv3 * Constants.rho_steel * Math.Pow(10, -9);
        }

        public void calc_Vsh1(double bf_shest1, double d_shest1)
        {
            _Vsh1 = Constants.pi / 4 * bf_shest1 * (Math.Pow(d_shest1, 2) - Math.Pow(_d1, 2));
        }

        public void calc_Vsh2(double bw1, double d_kol1)
        {
            _Vsh2 = Constants.pi / 4 * bw1 * (Math.Pow(d_kol1, 2) - Math.Pow(_d2, 2));
        }

        public void calc_Vsh3(double bw2, double d_kol2)
        {
            _Vsh3 = Constants.pi / 4 * bw2 * (Math.Pow(d_kol2, 2) - Math.Pow(_d3, 2));
        }

        public void calc_msh1()
        {
            _msh1 = _Vsh1 * Constants.rho_steel * Math.Pow(10, -9);
        }

        public void calc_msh2()
        {
            _msh2 = _Vsh2 * Constants.rho_steel * Math.Pow(10, -9);
        }

        public void calc_msh3()
        {
            _msh3 = _Vsh3 * Constants.rho_steel * Math.Pow(10, -9);
        }

        public void calc_mrk(double m_dif)
        {
            _mrk = _m_korp + _msh1 + _msh2 + _msh3 + _mv1 + _mv2 + _mv3 + m_dif;
        }

        //Property
        public double aw1
        {
            get { return _aw1; }
            set { _aw1 = value; }
        }

        public double aw2
        {
            get { return _aw2; }
            set { _aw2 = value; }
        }

        public double Ka
        {
            get { return _Ka; }
            set { _Ka = value; }
        }

        public double U1st
        {
            get { return _U1st; }
            set { _U1st = value; }
        }

        public double U2st
        {
            get { return _U2st; }
            set { _U2st = value; }
        }

        public double M0
        {
            get { return _M0; }
            set { _M0 = value; }
        }

        public double H1
        {
            get { return _H1; }
        }

        public double H2
        {
            get { return _H2; }
        }

        public double B1
        {
            get { return _B1; }
        }

        public double B2
        {
            get { return _B2; }
        }

        public double L1
        {
            get { return _L1; }
        }

        public double L2
        {
            get { return _L2; }
        }

        public double V_korp
        {
            get { return _V_korp; }
        }

        public double m_korp
        {
            get { return _m_korp; }
        }

        public double mrk
        {
            get { return _mrk; }
        }

        public double msh1
        {
            get { return _msh1; }
        }

        public double msh2
        {
            get { return _msh2; }
        }

        public double msh3
        {
            get { return _msh3; }
        }

        public double mv1
        {
            get { return _mv1; }
        }

        public double mv2
        {
            get { return _mv2; }
        }

        public double mv3
        {
            get { return _mv3; }
        }

        public double delta
        {
            get { return _delta; }
            set { _delta = value; }
        }
    
    }
}
