using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNTU_project
{
    public class Flexion
    {
        private double _KF_v; //Коэффициент учитывающий влияние динамический нагрузок на усталость зубчатых колес
        private double _YF0_shest; //Номинальное значение коэффициента напряжения изгиба зуба ведущего колеса
        private double _YF0_kol; //Номинально значение коэффициента напряжения изгиба зуба ведомого колеса
        private double _ku_shest; //Коэффициент учитывающий параметры парного ведущего зубчатого колеса
        private double _ku_kol; //Коэффициент учитывающий параметры парного ведомого зубчатого колеса
        private double _zv_shest; //Эквивалентное число зубьев ведущего колеса
        private double _zv_kol; //Эквивалентное число зубьев ведомого колеса
        private double _YF_shest; //Единичное напряжение изгиба ведущего колеса
        private double _YF_kol; //Единичное напряжение изгиба ведомого колеса
        private double _Y_eps; //Коэффициент учитывающий перекрытие зубьев
        private double _KF_alpha; //Коэффициент учитывающий вид зуба и точность его изготовления
        private double _KF_beta; //Коэффициент учитывающий неравномерность распределения нагрузки по ширине венцов
        private double _KF_omega = 1; //Коэффициент учитывающий влияние приработки зубьев в процессе эксплуатации
        private double _KFmu_shest = 1.05; //коэффициент учитывающий влияние сил трения для ведущего колеса
        private double _KFmu_kol = 0.95; //Коэффициент учитывающий влияние сил трения для ведомого колеса
        private int _KF_x = 1; //Коэффициент учитывающий влияние размеров зубчатых колес
        private int _YR = 1; //Коэффициент учитывающий особенности технологии переходной кривой у зуба
        private double _sigmaF_shest; //Расчетное напряжение изгиба ведущего колеса
        private double _sigmaF_kol; //Расчетное напряжение изгиба ведомого колеса
        private double _KF_c = 1.3; //Коэффициент учитывающий характер нагружения
        private double _sigma_FPO; //Предельно напряжение изгиба
        private double _R1F_shest; //Ресурс ведущего зубчатого колеса по усталости при изгибе
        private double _R1F_kol; //Ресурс ведомого зубчатого колеса по усталости при изгибе
        private double _RF_lim; //Циклостойкость по изгибным напряжениям
        private double _LF_shest; //Пробег автомобиля до усталостной поломки зуба ведущего колеса
        private double _LF_kol; //Пробег автомобиля до усталостной поломки зуба ведомого колеса

        public Flexion()
        {
        }

        //Методы
        public void calc_All(double z_shest1, double z_kol1, double bf_shest1, double bf_kol1, double beta_d, double z_eps, int precision_plav,
            double K0_beta, double Kj_delta, double Kve, int sigmaFlimb_c_star, double Ft, double mn, double Ukp1,
            double ksi, double KPF, double U_d1, double U_d2, int NFO, double v, double r0, double U0, int kinematicScheme)
        {
            calc_zv_shest(z_shest1, beta_d);
            calc_zv_kol(z_kol1, beta_d);
            calc_YF0_shest();
            calc_YF0_kol();
            calc_ku_shest(z_shest1);
            calc_ku_kol(z_kol1);
            calc_YF_shest();
            calc_YF_kol();
            calc_Y_eps(z_eps);
            calc_KF_alpha(precision_plav);
            calc_KF_beta(K0_beta);
            calc_KF_v(Kj_delta, Kve, v);
            calc_sigma_FPO(sigmaFlimb_c_star);
            calc_sigmaF_shest(Ft, bf_shest1, mn);
            calc_sigmaF_kol(Ft, bf_kol1, mn);
            calc_R1F_shest(U_d1, U_d2, U0, ksi, KPF, r0, kinematicScheme);
            calc_R1F_kol(U0, ksi, KPF, U_d2, r0, kinematicScheme);
            calc_RF_lim(NFO);
            calc_LF_shest();
            calc_LF_kol();
        }

        public bool isValid(int L0)
        {
            var result = true;

            if (_LF_shest < L0)
                result = false;

            if (_LF_kol < L0)
                result = false;

            return result;
        }

        //Заглушка, считается по графику
        private void calc_YF0_shest()
        {
            //_YF0_shest = 2.17;

            _YF0_shest = getYF0_shest();
        }

        private double getYF0_shest()
        {
            double result = 0;
            double X = _zv_shest;

            if ((X >= 12) && (X < 16))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 12, 16, 2.42, 2.6);
            } 

            if ((X >= 16) && (X < 20))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 16, 20, 2.32, 2.42);
            }

            if ((X >= 20) && (X < 25))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 20, 25, 2.25, 2.32);
            }

            if ((X >= 25) && (X < 30))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 25, 30, 2.2, 2.25);
            }

            if ((X >= 30) && (X < 40))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 30, 40, 2.15, 2.2);
            }

            if ((X >= 40) && (X < 50))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 40, 50, 2.135, 2.15);
            }

            if ((X >= 50) && (X < 150))
            {
                result = 2.135;
            }

            return result;
        }

        private void calc_YF0_kol()
        {
            //_YF0_kol = 2.73;

            _YF0_kol = getYF0_kol();
        }

        private double getYF0_kol()
        {
            double result = 0;
            double X = _zv_kol;

            if ((X >= 18) && (X < 25))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 18, 25, 3.1, 3.6);
            }

            if ((X >= 25) && (X < 30))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 25, 30, 2.9, 3.1);
            }

            if ((X >= 30) && (X < 40))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 30, 40, 2.7, 2.9);
            }

            if ((X >= 40) && (X < 60))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 40, 60, 2.58, 2.7);
            }

            if ((X >= 60) && (X < 80))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 60, 80, 2.5, 2.58);
            }

            if ((X >= 80) && (X < 100))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 80, 100, 2.45, 2.5);
            }

            if ((X >= 100) && (X < 150))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 100, 150, 2.39, 2.45);
            }

            return result;
        }

        private void calc_zv_shest(double z_shest1, double beta_d)
        {
            _zv_shest = z_shest1 / Math.Pow(Math.Cos(DegreeToRadian(beta_d)), 3);
        }

        private void calc_zv_kol(double z_kol1, double beta_d)
        {
            _zv_kol = z_kol1 / Math.Pow(Math.Cos(DegreeToRadian(beta_d)), 3);
        }

        private void calc_ku_shest(double z_shest1)
        {
            _ku_shest = 1 + 0.25 * (z_shest1 / _zv_kol - 1);
        }

        private void calc_ku_kol(double z_kol1)
        {
            _ku_kol = 1 + 0.25 * (z_kol1 / _zv_shest - 1);
        }

        private void calc_YF_shest()
        {
            _YF_shest = _YF0_shest * _ku_shest;
        }

        private void calc_YF_kol()
        {
            _YF_kol = _YF0_kol * _ku_kol;
        }

        private void calc_Y_eps(double z_eps)
        {
            _Y_eps = z_eps;
        }

        private void calc_KF_alpha(int precision_plav)
        {
            switch (precision_plav)
            {
                case 6:
                    _KF_alpha = 1;
                    break;
                case 7:
                    _KF_alpha = 1;
                    break;
                case 8:
                    _KF_alpha = 1.04;
                    break;
                case 9:
                    _KF_alpha = 1.08;
                    break;
            }
        }

        private void calc_KF_beta(double K0_beta)
        {
            _KF_beta = 1 + (K0_beta - 1) * _KF_omega;
        }

        private void calc_KF_v(double Kj_delta, double Kve, double v)
        {
            if (v > 1)
                _KF_v = Kj_delta * Kve;
            else
                _KF_v = 1;
        }

        private void calc_sigma_FPO(int sigmaFlimb_c_star )
        {
            _sigma_FPO = sigmaFlimb_c_star * _YR * _KF_c;
        }

        private void calc_sigmaF_shest(double Ft, double bf_shest1, double mn)
        {
            _sigmaF_shest = Ft * _YF_shest * _Y_eps * _KF_alpha * _KF_beta * _KF_v * _KFmu_shest / (bf_shest1 * mn);
        }

        private void calc_sigmaF_kol(double Ft, double bf_kol1, double mn)
        {
            _sigmaF_kol = Ft * _YF_kol * _Y_eps * _KF_alpha * _KF_beta * _KF_v * _KFmu_kol / (bf_kol1 * mn);
        }

        private void calc_R1F_shest(double U_d1, double U_d2, double U0, double ksi, double KPF, double r0, int kinematicScheme)
        {
            if (kinematicScheme > 3)
                _R1F_shest = 500 * Math.Pow(_sigmaF_shest, 9) * U_d1 * U0 * ksi * KPF / (Constants.pi * r0);
            else
                _R1F_shest = 500 * Math.Pow(_sigmaF_shest, 9) * U_d1 * U_d2 * U0 * ksi * KPF / (Constants.pi * r0);
        }

        private void calc_R1F_kol(double U0, double ksi, double KPF, double U_d2, double r0, int kinematicScheme)
        {
            if (kinematicScheme > 3)
                _R1F_kol = 500 * Math.Pow(_sigmaF_kol, 9) * U0 * ksi * KPF / (Constants.pi * r0);
            else
                _R1F_kol = 500 * Math.Pow(_sigmaF_kol, 9) * U_d2 * U0 * ksi * KPF / (Constants.pi * r0);
        }

        private void calc_RF_lim(int NFO)
        {
            _RF_lim = Math.Pow(_sigma_FPO, 9) * NFO;
        }

        private void calc_LF_shest()
        {
            _LF_shest = _RF_lim / _R1F_shest;
        }

        private void calc_LF_kol()
        {
            _LF_kol = _RF_lim / _R1F_kol;
        }

        //Вспомогательные функции
        private double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        private double RadianToDegree(double angle)
        {
            return angle * (180.0 / Math.PI);
        }

        //Property
        public double KF_v
        {
            get { return _KF_v; }
            set { _KF_v = value; }
        }

        public double YF0_shest
        {
            get { return _YF0_shest; }
            set { _YF0_shest = value; }
        }

        public double YF0_kol 
        {
            get { return _YF0_kol; }
            set { _YF0_kol = value; }
        }

        public double ku_shest 
        {
            get { return _ku_shest; }
            set { _ku_shest = value; }
        }

        public double ku_kol 
        {
            get { return _ku_kol; }
            set { _ku_kol = value; }
        }

        public double zv_shest
        {
            get { return _zv_shest; }
            set { _zv_shest = value; }
        }

        public double zv_kol
        {
            get { return _zv_kol; }
            set { _zv_kol = value; }
        }

        public double YF_shest
        {
            get { return _YF_shest; }
            set { _YF_shest = value; }
        }

        public double YF_kol
        {
            get { return _YF_kol; }
            set { _YF_kol = value; }
        }
 
        public double Y_eps
        {
            get { return _Y_eps; }
            set { _Y_eps = value; }
        }

        public double KF_alpha
        {
            get { return _KF_alpha; }
            set { _KF_alpha = value; }
        }

        public double KF_beta
        {
            get { return _KF_beta; }
            set { _KF_beta = value; }
        }

        public double KF_omega
        {
            get { return _KF_omega; }
            set { _KF_omega = value; }
        }

        public double KFmu_shest
        {
            get { return _KFmu_shest; }
            set { _KFmu_shest = value; }
        }

        public double KFmu_kol
        {
            get { return _KFmu_kol; }
            set { _KFmu_kol = value; }
        }

        public int KF_x
        {
            get { return _KF_x; }
            set { _KF_x = value; }
        }

        public int YR
        {
            get { return _YR; }
            set { _YR = value; }
        }

        public double sigmaF_shest
        {
            get { return _sigmaF_shest; }
            set { _sigmaF_shest = value; }
        }

        public double sigmaF_kol
        {
            get { return _sigmaF_kol; }
            set { _sigmaF_kol = value; }
        }

        public double KF_c
        {
            get { return _KF_c; }
            set { _KF_c = value; }
        }

        public double sigma_FPO
        {
            get { return _sigma_FPO; }
            set { _sigma_FPO = value; }
        }

        public double R1F_shest
        {
            get { return _R1F_shest; }
            set { _R1F_shest = value; }
        }

        public double R1F_kol
        {
            get { return _R1F_kol; }
            set { _R1F_kol = value; }
        }

        public double RF_lim
        {
            get { return _RF_lim; }
            set { _RF_lim = value; }
        }

        public double LF_shest
        {
            get { return _LF_shest; }
            set { _LF_shest = value; }
        }

        public double LF_kol
        {
            get { return _LF_kol; }
            set { _LF_kol = value; }
        }
    }
}
