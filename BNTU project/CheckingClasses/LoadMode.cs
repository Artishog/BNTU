using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNTU_project
{
    public class LoadMode
    {
        private double _Mp; //Расчетный крутящий момент
        private double _np; //Расчетная частота вращения
        private double _Mc; //Момент сцепления
        private double _Md; //Максимальный момент в трансмиссии
        private double _Kpl = 1; //Коэффициент учитывающий возможную неравномерность деления крутящего момента
        private double _gamma_mid; //Среднее значение окружной силы на передаче
        private double _gamma_psi; //Удельное сопротивление дороги
        private double _gamma_p; //Удельная сила тяги
        private double _gamma_v = 0; //Среднее удельное сопротивление воздуха
        private double _gamma_j; //Среднее удельное сопротивление разгону
        private double _B; //Просто B
        private double _KPH; //Коэффициент пробега
        private double _KPF; //Коэффициент пробега
        private double _ksi; //Относительное использование передач по пробегу

        public LoadMode()
        {
            _ksi = 0.003;
        }

        //Методы
        public void calc_All(double Memax, double Ukp1, double U_d1, double U_d2, double ma, int np, double r0, double U0, string carType, int kinematicScheme)
        {
            calc_Md(Memax, Ukp1, U_d1, kinematicScheme);
            calc_Mc(ma, r0, Ukp1, U_d1, U_d2, U0);
            calc_Mp();
            calc_np(np, Ukp1);
            calc_gamma_p();
            calc_gamma_psi(carType);
            calc_B(carType);
            calc_gamma_j();
            calc_gamma_mid();
            calc_KPH(carType);
            calc_KPF();
        }

        private void calc_Md(double Memax, double Ukp1, double U_d1, int kinematicScheme)
        {
            if (kinematicScheme <= 3)
                _Md = 0.95 * Memax * Ukp1;
            else
                _Md = 0.95 * Memax * Ukp1 * U_d1;
        }

        private void calc_Mc(double ma, double r0, double Ukp1, double U_d1, double U_d2, double U0)
        {
            _Mc = (ma * Constants.g * Constants.fi * r0) / (Ukp1 * U_d1 * U_d2 * U0 * 0.9);
        }

        private void calc_Mp()
        {
            _Mp = _Md;
        }

        private void calc_np(int np, double Ukp1)
        {
            _np = 0.6 * np / Ukp1;
        }

        private void calc_gamma_p()
        {
            _gamma_p = Constants.fi;
        }

        private void calc_gamma_psi(string carType)
        {
            switch (carType)
            {
                case "Легковой":
                    _gamma_psi = 0.018;
                    break;
                case "Грузовой":
                    _gamma_psi = 0.03;
                    break;
                case "Автобус городской":
                    _gamma_psi = 0.03;
                    break;
                case "Автобус междугородний":
                    _gamma_psi = 0.03;
                    break;
                case "Самосвал":
                    _gamma_psi = 0.05;
                    break;
                case "Многоприводный автомобиль":
                    _gamma_psi = 0.05;
                    break;
                default:
                    _gamma_psi = 0.04;
                    break;
            }
        }

        private void calc_B(string carType)
        {
            switch (carType)
            {
                case "Легковой":
                    _B = 0.2;
                    break;
                case "Грузовой":
                    _B = 0.3;
                    break;
                case "Автобус городской":
                    _B = 0.3;
                    break;
                case "Автобус междугородний":
                    _B = 0.3;
                    break;
                case "Самосвал":
                    _B = 0.2;
                    break;
                case "Многоприводный автомобиль":
                    _B = 0.2;
                    break;
                default:
                    _B = 0.25;
                    break;
            }
        }

        private void calc_gamma_j()
        {
            _gamma_j = _B * (_gamma_p - _gamma_psi - _gamma_v);
        }

        private void calc_gamma_mid()
        {
            _gamma_mid = _gamma_psi + _gamma_v + _gamma_j;
        }

        //Вычисляется по графикам, нужно будет сделать, пока оставлены заглушки
        private void calc_KPH(string carType)
        {
            _KPH = 0.09;

            switch (carType)
            {
                case "Самосвал":
                    //график один
                    _KPH = getKPH_firstCase();
                    break;
                case "Грузовой":
                    //график два
                    _KPH = getKPH_firstCase();
                    break;
                case "Автобус городской":
                    //график два
                    _KPH = getKPH_firstCase();
                    break;
                case "Автобус междугородний":
                    //график два
                    _KPH = getKPH_firstCase();
                    break;
                default:
                    //график один
                    _KPH = getKPH_firstCase();
                    break;

            }
        }

        private double getKPH_firstCase()
        {
            double result = 0;
            double X = _gamma_p / _gamma_mid;

            if ((X >= 1) && ( X < 1.5))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 1, 1.5, 0.25, 0.34);
            }

            if ((X >= 1.5) && ( X < 1.8))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 1.5, 1.8, 0.2, 0.25);
            }
             
            if ((X >= 1.8) && ( X < 2))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 1.8, 2, 0.185, 0.2);
            }

            if ((X >= 2) && (X < 3))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 2, 3, 0.115, 0.185);
            }

            if ((X >= 3) && (X < 3.2))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 3, 3.2, 0.1, 0.115);
            }

            if ((X >= 3.2) && ( X < 3.7))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 3.2, 3.7, 0.08, 0.1);
            }

            if ((X >= 3.7) && ( X < 4.25))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 3.7, 4.25, 0.06, 0.08);
            }

            if ((X >= 4.25) && ( X < 5))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 4.25, 5, 0.04, 0.06);
            }

            if ((X >= 5) && ( X < 6))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 5, 6, 0.023, 0.04);
            }

            if ((X >= 6) && ( X < 6.35))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 6, 6.35, 0.02, 0.023);
            }

            if ((X >= 6.35) && (X < 7))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 6.35, 7, 0.017, 0.02);
            }

            if ((X >= 7) && (X < 8))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 7, 8, 0.014, 0.017);
            }

            if ((X >= 8) && (X < 9))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 8, 9, 0.01, 0.014);
            }    

            return result;
        }

        private void calc_KPF()
        {
            _KPF = 0.016;

            _KPF = getKPF();

        }

        private double getKPF()
        {
            double result = 0;
            double X = _gamma_p / _gamma_mid;

            if ((X >= 1) && (X < 1.1))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 1, 1.1, 0.1, 0.135);
            } 

            if ((X >= 1.1) && (X < 1.5))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 1.1, 1.5, 0.09, 0.1);
            } 

            if ((X >= 1.5) && (X < 1.7))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 1.5, 1.7, 0.08, 0.09);
            } 

            if ((X >= 1.7) && (X < 2))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 1.7, 2, 0.06, 0.08);
            } 

            if ((X >= 2) && (X < 2.6))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 2, 2.6, 0.04, 0.06);
            }

            if ((X >= 2.6) && (X < 3))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 2.6, 3, 0.028, 0.04);
            }

            if ((X >= 3) && (X < 3.35))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 3, 3.35, 0.02, 0.028);
            }

            if ((X >= 3.35) && (X < 4))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 3.35, 4, 0.014, 0.02);
            }

            if ((X >= 4) && (X < 4.4))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 4, 4.4, 0.01, 0.014);
            } 

            if ((X >= 4.4) && (X < 5))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 4.4, 5, 0.006, 0.01);
            }

            if ((X >= 5) && (X < 6))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 5, 6, 0.003, 0.006);
            }

            if ((X >= 6) && (X < 7))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 6, 7, 0.0017, 0.003);
            } 

            if ((X >= 7) && (X < 9))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 7, 9, 0.0006, 0.0017);
            } 

            return result;
        }

        //по таблице, пока заглушка
        /*private void calc_ksi()
        {
            _ksi = 0.003;
        }*/

        //Property
        public double Mp
        {
            get { return _Mp; }
            set { _Mp = value; }
        }

        public double np
        {
            get { return _np; }
            set { _np = value; }
        }

        public double Mc
        {
            get { return _Mc; }
            set { _Mc = value; }
        }

        public double Md 
        {
            get { return _Md; }
            set { _Md = value; }
        }

        public double Kpl
        {
            get { return _Kpl; }
            set { _Kpl = value; }
        }

        public double gamma_mid 
        {
            get { return _gamma_mid; }
            set { _gamma_mid = value; }
        }

        public double gamma_psi 
        {
            get { return _gamma_psi; }
            set { _gamma_psi = value; }
        }

        public double gamma_p
        {
            get { return _gamma_p; }
            set { _gamma_p = value; }
        }

        public double gamma_v 
        {
            get { return _gamma_v; }
            set { _gamma_v = value; }
        }

        public double gamma_j
        {
            get { return _gamma_j; }
            set { _gamma_j = value; }
        }

        public double B
        {
            get { return _B; }
            set { _B = value; }
        }

        public double KPH
        {
            get { return _KPH; }
            set { _KPH = value; }
        }

        public double KPF 
        {
            get { return _KPF; }
            set { _KPF = value; }
        }

        public double ksi 
        {
            get { return _ksi; }
            set { _ksi = value; }
        }
    }
}
