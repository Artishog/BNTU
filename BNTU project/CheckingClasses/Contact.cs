using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNTU_project
{
    public class Contact
    {
        private double _Ph_shest; //Параметр расчетного контактного напряжения ведущего колеса
        private double _Ph_kol; //Параметр расчетного контактного напряжения ведомого колеса
        private double _Phpo; //Предельное контактное напряжение
        private double _Ph_limb; //Параметр предела контактной выносливости
        private double _NHO = 120000000; //Количество циклов базы испытаний
        private double _ZR = 1; //Коэффициент учитывающий шероховатость активной поверхности зубьев
        private double _Ft; //Расчетная окружная сила
        private double _ZH; //Единичное контактное напряжение
        private double _alpha_n = 20; //Угол профиля в нормальном сечении
        private double _eps_beta; //Коэффициент осевого перекрытия
        private double _eps_alpha; //Коэффициент торцевого перекрытия
        private double _z_eps; //Коэффициент учитывающий перекрытие при расчете контактных напряжений
        private double _KH_alpha; //Коэффициент учитывающий вид зуба и точность его изготовления
        private double _KH_psi; //Коэффициент учитывающий повышение интенсивности нагрузки на наклонных контактных линиях
        private double _KH_gamma; //Коэффициент учитывающий неточность распределения нагрузки между зубьями
        private int _precision_plav = 7; //Степень точности передачи по нормам плавности
        private int _precision_sheroh = 7; //Степень точности передачи по нормам шероховатости
        private double _psi_bd; //Отношение ширины венца к диаметру шестерни
        private double _K0_beta; //Коэффициент учитывающий неравномерность распределения нагрузки по ширине венца в начальный период работы передачи
        private double _KH_omega; //Коэффициент учитывающий влияние при работе зубьев в процессе эксплуатации
        private double _v; //Окружная скорость
        private double _KH_beta; //Коэффициент учитывающий неравномерность распределения нагрузки по ширине венцов
        private double _KH_v; //Коэффициент учитывающий влияние динамических нагрузок на усталость зубчатых колес
        private double _Kj_delta; //Коэффициент учитывающий динамическую нагрузку обусловленную погрешностями зубчатых колес
        private double _Kve; //Коэффициент учитывающий динамическую нагрузку от воздействия звеньев внешних по отношению к зубчатой передаче
        private double _R1H_shest; //Циклонапряженность ведущего колеса на 1 км пробега автомобиля
        private double _R1H_kol; //Циклонапряженность ведомого колеса на 1 км пробега автомобиля
        private double _RH_lim; //Циклостойкость по контактным напряжениям
        private double _LH_shest; //Ресурс ведущего колеса в км общего пробега автомобиля
        private double _LH_kol; //Ресурс ведомого колеса в км общего пробега автомобиля


        public Contact()
        {
        }

        //Методы
        public void calc_All(double Mp, double U_d1, double beta_d, double bw, double mn, double np, double z_kol2, double z_shest2, double d_shest1)
        {
            calc_Ft(d_shest1, Mp);
            calc_ZH(U_d1, beta_d);
            calc_eps_beta(bw, beta_d, mn);
            calc_eps_alpha(z_shest2, z_kol2, beta_d);
            calc_z_eps();
            calc_KH_psi();
            calc_KH_gamma();
            calc_KH_alpha();
            calc_psi_bd(bw, d_shest1);
            calc_v(d_shest1, U_d1, np);
            calc_KH_omega();
            calc_K0_beta();
            calc_KH_beta();
            calc_KH_v();
            calc_Kve();

        }

        private void calc_Ft(double d_shest1, double Mp)
        {
            _Ft = 2000 * Mp / d_shest1;
        }

        private void calc_ZH(double U_d1, double beta_d)
        {
            _ZH = (2 * (U_d1 + 1) * Math.Cos(DegreeToRadian(beta_d)) * Math.Cos(DegreeToRadian(beta_d))) / (U_d1 * Math.Sin(DegreeToRadian(2 * _alpha_n)));
        }

        private void calc_eps_beta(double bw, double beta_d, double mn)
        {
            _eps_beta = (bw * Math.Sin(DegreeToRadian(beta_d))) / (Constants.pi * mn);
        }

        private void calc_eps_alpha(double z_shest2, double z_kol2, double beta_d)
        {
            _eps_alpha = (1.88 - 3.02 * (1 / z_shest2 + 1 / z_kol2)) * Math.Cos(DegreeToRadian(beta_d));
        }

        //Заглушка, необходимо считать по графику
        private void calc_z_eps()
        {
            _z_eps = 0.55;
        }

        private void calc_KH_psi()
        {
            if (_eps_beta <= 0.5)
                _KH_psi = 1.16;

            if ((_eps_beta > 0.5) && (_eps_beta <= 0.55))
                _KH_psi = 1.17;

            if ((_eps_beta > 0.55) && (_eps_beta <= 0.6))
                _KH_psi = 1.19;

            if ((_eps_beta > 0.6) && (_eps_beta <= 0.65))
                _KH_psi = 1.21;

            if ((_eps_beta > 0.65) && (_eps_beta <= 0.7))
                _KH_psi = 1.22;

            if ((_eps_beta > 0.7) && (_eps_beta <= 0.75))
                _KH_psi = 1.24;

            if ((_eps_beta > 0.75) && (_eps_beta <= 0.8))
                _KH_psi = 1.26;

            if ((_eps_beta > 0.8) && (_eps_beta <= 0.85))
                _KH_psi = 1.28;

            if ((_eps_beta > 0.85) && (_eps_beta <= 0.9))
                _KH_psi = 1.30;

            if ((_eps_beta > 0.9) && (_eps_beta <= 0.95))
                _KH_psi = 1.32;

            if (_eps_beta > 1)
                _KH_psi = 1.33;
        }

        private void calc_KH_gamma()
        {
            switch (_precision_plav)
            {
                case 6:
                    _KH_gamma = 1;
                    break;
                case 7:
                    _KH_gamma = 1.05;
                    break;
                case 8:
                    _KH_gamma = 1.1;
                    break;
                case 9:
                    _KH_gamma = 1.15;
                    break;
                default:
                    _KH_gamma = 1.1;
                    break;
            }

        }

        private void calc_KH_alpha()
        {
            _KH_alpha = _KH_psi * _KH_gamma;
        }

        private void calc_psi_bd(double bw, double d_shest1)
        {
            _psi_bd = bw / d_shest1;
        }

        private void calc_v(double d_shest1, double U_d1, double np)
        {
            _v = (Constants.pi * d_shest1 * np * 0.6) / (60 * 1000 * U_d1);
        }

        private void calc_KH_omega()
        {
            if (_v <= 1)
                _KH_omega = 0.8;

            if ((_v > 1) && (_v <= 2))
                _KH_omega = 0.85;

            if ((_v > 2) && (_v <= 4))
                _KH_omega = 0.96;

            if ((_v > 4) && (_v <= 6))
                _KH_omega = 1;

            if (_v > 6)
                _KH_omega = 1;
        }

        //Заглушка, необходимо считать по графику
        private void calc_K0_beta()
        {
            _K0_beta = 1.05;
        }

        private void calc_KH_beta()
        {
            _KH_beta = 1 + (_K0_beta - 1) * _KH_omega;
        }

        private void calc_KH_v()
        {
            if (_v <= 1)
                _KH_v = 1;
            else
                _KH_v = Math.Sqrt(_Kj_delta * _Kve);
        }

        //Заглушка, необходимо считать по графику
        private void calc_Kve()
        {
            _Kve = 1;
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
    }
}
