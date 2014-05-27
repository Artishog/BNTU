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
        private double _ksi; //Относительный пробег

        public LoadMode()
        {
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
            calc_KPH();
            calc_KPF();
            calc_ksi();
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
        private void calc_KPH()
        {
            _KPH = 0.09;
        }

        private void calc_KPF()
        {
            _KPF = 0.016;
        }

        //по таблице, пока заглушка
        private void calc_ksi()
        {
            _ksi = 0.003;
        }

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
