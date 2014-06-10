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
        private double _N_delta = 0.1; // - 
        private double _delta0; //Расчетная производственная погрешность зубчатых колес

        public Contact()
        {
        }

        //Методы
        public void calc_All(double Mp, double U_d1, double beta_d, double bw, double mn, 
            double np, double z_kol1, double z_shest1, double d_shest1, double bf_shest1, 
            double d_kol1, double Ukp1, double ksi, double r0, double PHlimb_star, double KPH, int NHO, int kinematicScheme)
        {
            calc_Ft(d_shest1, Mp);
            calc_ZH(U_d1, beta_d);
            calc_eps_beta(bw, beta_d, mn);
            calc_eps_alpha(z_shest1, z_kol1, beta_d);
            calc_z_eps();
            calc_KH_psi();
            calc_KH_gamma();
            calc_KH_alpha();
            calc_psi_bd(bw, d_shest1);
            calc_v(d_shest1, U_d1, np);
            calc_KH_omega();
            calc_K0_beta(kinematicScheme);
            calc_KH_beta();
            calc_Kve();
            calc_delta0(mn);
            calc_Kj_delta(bf_shest1, bw, d_shest1, U_d1);
            calc_KH_v();
            calc_PH_shest(bf_shest1, d_shest1);
            calc_PH_kol(bw, d_kol1);
            calc_Phpo(PHlimb_star);
            calc_R1H_shest(Ukp1, ksi, KPH, r0);
            calc_R1H_kol(Ukp1, ksi, KPH, r0, U_d1);
            calc_RH_lim(NHO);
            calc_LH_shest();
            calc_LH_kol();
        }

        public bool isValid(int L0)
        {
            var result = true;

            if (_LH_shest < L0)
                result = false;

            if (_LH_kol < L0)
                result = false;

            return result;
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

        private void calc_eps_alpha(double z_shest1, double z_kol1, double beta_d)
        {
            _eps_alpha = (1.88 - 3.02 * (1 / z_shest1 + 1 / z_kol1)) * Math.Cos(DegreeToRadian(beta_d));
        }

        //Заглушка, необходимо считать по графику
        private void calc_z_eps()
        {
            _z_eps = 0.65;
            _z_eps = getZ_eps();
        }

        private double getZ_eps()
        {
            double result = 0;

            var eps_alpha1 = 1.9;
            var eps_alpha2 = 1.8;
            var tempRes1 = getZ_eps_case1();
            var tempRes2 = getZ_eps_case2();
            if ((_eps_alpha > eps_alpha2) && (_eps_alpha <= eps_alpha1))
            {
                result = tempRes2 - (tempRes2 - tempRes1) * (_eps_alpha - eps_alpha2) / (eps_alpha1 - eps_alpha2);
            }

            eps_alpha1 = 1.8;
            eps_alpha2 = 1.7;
            tempRes1 = getZ_eps_case2();
            tempRes2 = getZ_eps_case3();
            if ((_eps_alpha > eps_alpha2) && (_eps_alpha <= eps_alpha1))
            {
                result = tempRes2 - (tempRes2 - tempRes1) * (_eps_alpha - eps_alpha2) / (eps_alpha1 - eps_alpha2);
            }

            eps_alpha1 = 1.7;
            eps_alpha2 = 1.6;
            tempRes1 = getZ_eps_case3();
            tempRes2 = getZ_eps_case4();
            if ((_eps_alpha > eps_alpha2) && (_eps_alpha <= eps_alpha1))
            {
                result = tempRes2 - (tempRes2 - tempRes1) * (_eps_alpha - eps_alpha2) / (eps_alpha1 - eps_alpha2);
            }

            eps_alpha1 = 1.6;
            eps_alpha2 = 1.5;
            tempRes1 = getZ_eps_case4();
            tempRes2 = getZ_eps_case5();
            if ((_eps_alpha > eps_alpha2) && (_eps_alpha <= eps_alpha1))
            {
                result = tempRes2 - (tempRes2 - tempRes1) * (_eps_alpha - eps_alpha2) / (eps_alpha1 - eps_alpha2);
            }

            eps_alpha1 = 1.5;
            eps_alpha2 = 1.4;
            tempRes1 = getZ_eps_case5();
            tempRes2 = getZ_eps_case6();
            if ((_eps_alpha > eps_alpha2) && (_eps_alpha <= eps_alpha1))
            {
                result = tempRes2 - (tempRes2 - tempRes1) * (_eps_alpha - eps_alpha2) / (eps_alpha1 - eps_alpha2);
            }

            eps_alpha1 = 1.4;
            eps_alpha2 = 1.3;
            tempRes1 = getZ_eps_case6();
            tempRes2 = getZ_eps_case7();
            if ((_eps_alpha > eps_alpha2) && (_eps_alpha <= eps_alpha1))
            {
                result = tempRes2 - (tempRes2 - tempRes1) * (_eps_alpha - eps_alpha2) / (eps_alpha1 - eps_alpha2);
            }

            eps_alpha1 = 1.3;
            eps_alpha2 = 1.2;
            tempRes1 = getZ_eps_case7();
            tempRes2 = getZ_eps_case8();
            if ((_eps_alpha > eps_alpha2) && (_eps_alpha <= eps_alpha1))
            {
                result = tempRes2 - (tempRes2 - tempRes1) * (_eps_alpha - eps_alpha2) / (eps_alpha1 - eps_alpha2);
            }

            eps_alpha1 = 1.2;
            eps_alpha2 = 1.1;
            tempRes1 = getZ_eps_case8();
            tempRes2 = getZ_eps_case9();
            if ((_eps_alpha > eps_alpha2) && (_eps_alpha <= eps_alpha1))
            {
                result = tempRes2 - (tempRes2 - tempRes1) * (_eps_alpha - eps_alpha2) / (eps_alpha1 - eps_alpha2);
            }

            eps_alpha1 = 1.1;
            eps_alpha2 = 1;
            tempRes1 = getZ_eps_case9();
            tempRes2 = getZ_eps_case10();
            if ((_eps_alpha > eps_alpha2) && (_eps_alpha <= eps_alpha1))
            {
                result = tempRes2 - (tempRes2 - tempRes1) * (_eps_alpha - eps_alpha2) / (eps_alpha1 - eps_alpha2);
            }

            eps_alpha1 = 1;
            eps_alpha2 = 0.9;
            tempRes1 = getZ_eps_case10();
            tempRes2 = getZ_eps_case11();
            if ((_eps_alpha > eps_alpha2) && (_eps_alpha <= eps_alpha1))
            {
                result = tempRes2 - (tempRes2 - tempRes1) * (_eps_alpha - eps_alpha2) / (eps_alpha1 - eps_alpha2);
            }

            return result;
        }

        private double getZ_eps_case1()
        {
            double result = 0;
            var X = _eps_beta;

            if ((X >= 0.1) && (X < 0.11))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.1, 0.11, 0.91, 1);
            } 

            if ((X >= 0.11) && (X < 0.125))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.11, 0.125, 0.833, 0.91);
            } 

            if ((X >= 0.125) && (X < 0.14))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.125, 0.14, 0.789, 0.833);
            } 

            if ((X >= 0.14) && (X < 0.17))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.14, 0.17, 0.714, 0.783);
            } 

            if ((X >= 0.17) && (X < 0.27))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.17, 0.27, 0.625, 0.714);
            } 

            if ((X >= 0.27) && (X < 0.37))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.27, 0.37, 0.588, 0.625);
            } 

            if ((X >= 0.37) && (X < 0.525))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.37, 0.525, 0.556, 0.588);
            } 

            if ((X >= 0.525) && (X < 0.7))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.525, 0.7, 0.535, 0.556);
            } 

            if ((X >= 0.7) && (X < 1))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.7, 1, 0.53, 0.535);
            } 

            if ((X >= 1) && (X < 1.1))
            {
                result = GraphHelper.GetYbyX_increasing(X, 1, 1.1, 0.53, 0.55);
            } 

            if ((X >= 1.1) && (X < 1.5))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 1.1, 1.5, 0.53, 0.55);
            } 

            if ((X >= 1.5) && (X < 1.8))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 1.5, 1.8, 0.526, 0.53);
            }

            return result;
        }

        private double getZ_eps_case2()
        {
            double result = 0;
            var X = _eps_beta;

            if ((X >= 0.1) && (X < 0.2))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.1, 0.2, 0.1, 1.5);
            }

            if ((X >= 0.2) && (X < 0.25))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.2, 0.25, 0.833, 1);
            }

            if ((X >= 0.25) && (X < 0.3))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.25, 0.3, 0.789, 0.833);
            }

            if ((X >= 0.3) && (X < 0.35))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.3, 0.35, 0.714, 0.789);
            }

            if ((X >= 0.35) && (X < 0.4))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.35, 0.4, 0.676, 0.714);
            }

            if ((X >= 0.4) && (X < 0.52))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.4, 0.52, 0.625, 0.676);
            }

            if ((X >= 0.52) && (X < 0.7))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.52, 0.7, 0.588, 0.625);
            }

            if ((X >= 0.7) && (X < 1))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.7, 1, 0.556, 0.588);
            }

            if ((X >= 1) && (X < 1.1))
            {
                result = GraphHelper.GetYbyX_increasing(X, 1, 1.1, 0.556, 0.58);
            }

            if ((X >= 1.1) && (X < 1.2))
            {
                result = GraphHelper.GetYbyX_increasing(X, 1.1, 1.2, 0.58, 0.6);
            }

            if ((X >= 1.2) && (X < 1.5))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 1.2, 1.5, 0.57,0.6);
            }

            if ((X >= 1.5) && (X < 1.8))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 1.5, 1.8, 0.55, 0.57);
            }

            return result;
        }

        private double getZ_eps_case3()
        {
            double result = 0;
            var X = _eps_beta;

            if ((X >= 0.2) && (X < 0.3))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.2, 0.3, 1, 1.32);
            }

            if ((X >= 0.3) && (X < 0.33))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.3, 0.33, 0.91, 1);
            }

            if ((X >= 0.33) && (X < 0.38))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.33, 0.38, 0.833, 0.91);
            }

            if ((X >= 0.38) && (X < 0.43))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.38, 0.43, 0.789, 0.833);
            }

            if ((X >= 0.43) && (X < 0.5))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.43, 0.5, 0.714, 0.789);
            }

            if ((X >= 0.5) && (X < 0.6))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.5, 0.6, 0.666, 0.714);
            }

            if ((X >= 0.6) && (X < 0.75))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.6, 0.75, 0.625, 0.666);
            }

            if ((X >= 0.75) && (X < 1))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.75, 1, 0.588, 0.625);
            }

            if ((X >= 1) && (X < 1.15))
            {
                result = GraphHelper.GetYbyX_increasing(X, 1, 1.15, 0.588, 0.625);
            }

            if ((X >= 1.15) && (X < 1.3))
            {
                result = GraphHelper.GetYbyX_increasing(X, 1.15, 1.3, 0.625, 0.645);
            }

            if ((X >= 1.3) && (X < 1.6))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 1.3, 1.6, 0.61, 0.645);
            }

            if ((X >= 1.6) && (X < 1.8))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 1.6, 1.8, 0.6, 0.61);
            }

            return result;
        }

        private double getZ_eps_case4()
        {
            double result = 0;
            var X = _eps_beta;

            if ((X >= 0.3) && (X < 0.4))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.3, 0.4, 1, 1.25);
            }

            if ((X >= 0.4) && (X < 0.5))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.4, 0.5, 0.833, 1);
            }

            if ((X >= 0.5) && (X < 0.6))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.5, 0.6, 0.77, 0.833);
            }

            if ((X >= 0.6) && (X < 0.67))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.6, 0.67, 0.714, 0.77);
            }

            if ((X >= 0.67) && (X < 0.8))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.67, 0.8, 0.666, 0.714);
            }

            if ((X >= 0.8) && (X < 1))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.8, 1, 0.625, 0.666);
            }

            if ((X >= 1) && (X < 1.2))
            {
                result = GraphHelper.GetYbyX_increasing(X, 1, 1.2, 0.625, 0.666);
            }

            if ((X >= 1.2) && (X < 1.4))
            {
                result = GraphHelper.GetYbyX_increasing(X, 1.2, 1.4, 0.666, 0.7);
            }

            if ((X >= 1.4) && (X < 1.6))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 1.4, 1.6, 0.666, 0.7);
            }

            if ((X >= 1.6) && (X < 1.8))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 1.6, 1.8, 0.64, 0.67);
            }

            return result;
        }

        private double getZ_eps_case5()
        {
            double result = 0;
            var X = _eps_beta;

            if ((X >= 0.4) && (X < 0.5))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.4, 0.5, 1, 1.18);
            }

            if ((X >= 0.5) && (X < 0.6))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.5, 0.6, 0.87, 1);
            }

            if ((X >= 0.6) && (X < 0.64))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.6, 0.64, 0.833, 0.87);
            }

            if ((X >= 0.64) && (X < 0.72))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.64, 0.72, 0.789, 0.833);
            }

            if ((X >= 0.72) && (X < 0.83))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.72, 0.83, 0.714, 0.789);
            }

            if ((X >= 0.83) && (X < 1))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.83, 1, 0.666, 0.714);
            }

            if ((X >= 1) && (X < 1.25))
            {
                result = GraphHelper.GetYbyX_increasing(X, 1, 1.25, 0.666, 0.714);
            }

            if ((X >= 1.25) && (X < 1.5))
            {
                result = GraphHelper.GetYbyX_increasing(X, 1.25, 1.5, 0.714, 0.776);
            }

            if ((X >= 1.5) && (X < 1.65))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 1.5, 1.65, 0.714, 0.776);
            }

            if ((X >= 1.65) && (X < 1.8))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 1.65, 1.8, 0.69, 0.714);
            }

            return result;
        }

        private double getZ_eps_case6()
        {
            double result = 0;
            var X = eps_beta;

            if ((X >= 0.5) && (X < 0.6))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.5, 0.6, 1, 1.15);
            }

            if ((X >= 0.6) && (X < 0.67))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.6, 0.67, 0.91, 1);
            }

            if ((X >= 0.67) && (X < 0.75))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.67, 0.75, 0.833, 0.91);
            }

            if ((X >= 0.75) && (X < 0.85))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.75, 0.85, 0.789, 0.833);
            }

            if ((X >= 0.85) && (X < 1))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.85, 1, 0.714, 0.789);
            }

            if ((X >= 1) && (X < 1.3))
            {
                result = GraphHelper.GetYbyX_increasing(X, 1, 1.3, 0.714, 0.789);
            }

            if ((X >= 1.3) && (X < 1.6))
            {
                result = GraphHelper.GetYbyX_increasing(X, 1.3, 1.6, 0.789, 0.81);
            }

            if ((X >= 1.6) && (X < 1.7))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 1.6, 1.7, 0.789, 0.81);
            }

            if ((X >= 1.7) && (X < 1.8))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 1.7, 1.8, 0.76, 0.789);
            }

            return result;
        }

        private double getZ_eps_case7()
        {
            double result = 0;
            var X = eps_beta;

            if ((X >= 0.6) && (X < 0.7))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.6, 0.7, 1, 1.15);
            }

            if ((X >= 0.7) && (X < 0.8))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.7, 0.8, 0.89, 1);
            }

            if ((X >= 0.8) && (X < 0.86))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.8, 0.86, 0.833, 0.89);
            }

            if ((X >= 0.86) && (X < 1))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.86, 1, 0.789, 0.833);
            }

            if ((X >= 1) && (X < 1.45))
            {
                result = GraphHelper.GetYbyX_increasing(X, 1, 1.45, 0.789, 0.833);
            }

            if ((X >= 1.45) && (X < 1.7))
            {
                result = GraphHelper.GetYbyX_increasing(X, 1.45, 1.7, 0.833, 0.85);
            }

            if ((X >= 1.7) && (X < 1.74))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 1.7, 1.74, 0.833, 0.85);
            }

            if ((X >= 1.74) && (X < 1.8))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 1.74, 1.8, 0.82, 0.833);
            }

            return result;
        }

        private double getZ_eps_case8()
        {
            double result = 0;
            var X = eps_beta;

            if ((X >= 0.7) && (X < 0.8))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.7, 0.8, 1, 1.15);
            }

            if ((X >= 0.8) && (X < 0.87))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.8, 0.87, 0.91, 1);
            }

            if ((X >= 0.87) && (X < 1))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.87, 1, 0.833, 0.91);
            }

            if ((X >= 1) && (X < 1.4))
            {
                result = GraphHelper.GetYbyX_increasing(X, 1, 1.4, 0.833, 0.88);
            }

            if ((X >= 1.4) && (X < 1.8))
            {
                result = GraphHelper.GetYbyX_increasing(X, 1.4, 1.8, 0.88, 0.895);
            }

            return result;
        }

        private double getZ_eps_case9()
        {
            double result = 0;
            var X = eps_beta;

            if ((X >= 0.8) && (X < 0.9))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.8, 0.9, 1, 1.15);
            }

            if ((X >= 0.9) && (X < 1))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.9, 1, 0.91, 1);
            }

            if ((X >= 1) && (X < 1.5))
            {
                result = GraphHelper.GetYbyX_increasing(X, 1, 1.5, 0.91, 0.955);
            }

            if ((X >= 1.5) && (X < 1.8))
            {
                result = 0.955;
            }

            return result;
        }

        private double getZ_eps_case10()
        {
            double result = 0;
            var X = eps_beta;

            if ((X >= 0) && (X < 1.8))
            {
                result = 1;
            }

            return result;
        }

        private double getZ_eps_case11()
        {
            double result = 0;
            var X = eps_beta;

            if ((X >= 0.5) && (X < 1))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 0.5, 1, 1.111, 1.23);
            }

            if ((X >= 1) && (X < 1.1))
            {
                result = GraphHelper.GetYbyX_increasing(X, 1, 1.1, 1.111, 1.22);
            }

            if ((X >= 1.1) && (X < 1.5))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 1.1, 1.5, 1.13, 1.22);
            }

            if ((X >= 1.5) && (X < 1.8))
            {
                result = GraphHelper.GetYbyX_decreasing(X, 1.5, 1.8, 1.111, 1.13);
            }

            return result;
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

            if (_eps_beta > 0.95)
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
        private void calc_K0_beta(int kinematicScheme)
        {
            _K0_beta = 1.09;
            getK0_beta(kinematicScheme);
        }

        private double getK0_beta(int kinematicScheme)
        {
            double result = 0;
            var X = _psi_bd;
            X = 0.5;

            if (kinematicScheme <= 3)
            {
                if ((X >= 0) && (X < 0.2))
                {
                    result = GraphHelper.GetYbyX_increasing(X, 0, 0.2, 1, 1.08);
                }

                if ((X >= 0.2) && (X < 0.4))
                {
                    result = GraphHelper.GetYbyX_increasing(X, 0.2, 0.4, 1.08, 1.17);
                }

                if ((X >= 0.4) && (X < 0.6))
                {
                    result = GraphHelper.GetYbyX_increasing(X, 0.4, 0.6, 1.17, 1.27);
                }

                if ((X >= 0.6) && (X < 0.8))
                {
                    result = GraphHelper.GetYbyX_increasing(X, 0.6, 0.8, 1.27, 1.4);
                }

                if ((X >= 0.8) && (X < 0.88))
                {
                    result = GraphHelper.GetYbyX_increasing(X, 0.8, 0.88, 1.4, 1.46);
                }
            }
            else
            {
                if ((X >= 0) && (X < 0.2))
                {
                    result = GraphHelper.GetYbyX_increasing(X, 0, 0.2, 1, 1.06);
                } 

                if ((X >= 0.2) && (X < 0.4))
                {
                    result = GraphHelper.GetYbyX_increasing(X, 0.2, 0.4, 1.06, 1.125);
                } 

                if ((X >= 0.4) && (X < 0.6))
                {
                    result = GraphHelper.GetYbyX_increasing(X, 0.4, 0.6, 1.125, 1.2);
                } 

                if ((X >= 0.6) && (X < 0.8))
                {
                    result = GraphHelper.GetYbyX_increasing(X, 0.6, 0.6, 1.2, 1.28);
                } 

                if ((X >= 0.8) && (X < 1))
                {
                    result = GraphHelper.GetYbyX_increasing(X, 0.8, 1, 1.28, 1.38);
                } 

                if ((X >= 1) && (X < 1.12))
                {
                    result = GraphHelper.GetYbyX_increasing(X, 1, 1.12, 1.38, 1.46);
                } 
            }

            return result;
        }

        private void calc_KH_beta()
        {
            _KH_beta = 1 + (_K0_beta - 1) * _KH_omega;
        }

        //Заглушка, необходимо считать по графику
        private void calc_Kve()
        {
            _Kve = 1.2;
            _Kve = getKve();
        }

        private double getKve()
        {
            double result = 0;
            var X = _v;

            if ((X >= 0.4) && (X < 6))
            {
                result = GraphHelper.GetYbyX_increasing(X, 0.4, 6, 1, 1.23);
            } 

            if ((X >= 6) && (X < 8))
            {
                result = GraphHelper.GetYbyX_increasing(X, 6, 8, 1.23, 1.28);
            } 

            if ((X >= 8) && (X < 9))
            {
                result = GraphHelper.GetYbyX_increasing(X, 8, 9, 1.28, 1.295);
            } 

            if ((X >= 9) && (X < 10))
            {
                result = GraphHelper.GetYbyX_increasing(X, 9, 10, 1.295, 1.3);
            } 

            return result;
        }

        private void calc_delta0(double mn)
        {
            if ((mn >= 1) && (mn < 2.5)) 
            {
                switch (_precision_sheroh)
                {
                    case 6:
                        _delta0 = 10;
                        break;
                    case 7:
                        _delta0 = 18;
                        break;
                    case 8:
                        _delta0 = 28;
                        break;
                }
            }

            if ((mn >= 2.5) && (mn < 4))
            {
                switch (_precision_sheroh)
                {
                    case 6:
                        _delta0 = 12;
                        break;
                    case 7:
                        _delta0 = 20;
                        break;
                    case 8:
                        _delta0 = 32;
                        break;
                }
            }

            if ((mn >= 4) && (mn < 6))
            {
                switch (_precision_sheroh)
                {
                    case 6:
                        _delta0 = 14;
                        break;
                    case 7:
                        _delta0 = 22;
                        break;
                    case 8:
                        _delta0 = 36;
                        break;
                }
            }

            if ((mn >= 6) && (mn < 8))
            {
                switch (_precision_sheroh)
                {
                    case 6:
                        _delta0 = 16;
                        break;
                    case 7:
                        _delta0 = 25;
                        break;
                    case 8:
                        _delta0 = 42;
                        break;
                }
            }

            if ((mn >= 8) && (mn < 10))
            {
                switch (_precision_sheroh)
                {
                    case 6:
                        _delta0 = 18;
                        break;
                    case 7:
                        _delta0 = 28;
                        break;
                    case 8:
                        _delta0 = 48;
                        break;
                }
            }

            if (mn >= 10)
            {
                switch (_precision_sheroh)
                {
                    case 6:
                        _delta0 = 20;
                        break;
                    case 7:
                        _delta0 = 34;
                        break;
                    case 8:
                        _delta0 = 56;
                        break;
                }
            }
        }

        private void calc_Kj_delta(double bf_shest1, double bw, double d_shest1, double U_d1)
        {
            _Kj_delta = 1 + (_v * _N_delta * 0.5 * (bf_shest1 + bw)) * Math.Sqrt(d_shest1 * (U_d1 + 1) * _delta0 / (1000 * U_d1)) / _Ft;
        }

        private void calc_KH_v()
        {
            if (_v <= 1)
                _KH_v = 1;
            else
                _KH_v = Math.Sqrt(_Kj_delta * _Kve);
        }

        private void calc_PH_shest(double bf_shest1, double d_shest1)
        {
            _Ph_shest = (_Ft * _ZH * _z_eps * _KH_alpha * _KH_beta * _KH_v) / (bf_shest1 * d_shest1);
        }

        private void calc_PH_kol(double bw, double d_kol1)
        {
            _Ph_kol = (_Ft * _ZH * _z_eps * _KH_alpha * _KH_beta * _KH_v) / (bw * d_kol1);
        }

        private void calc_Phpo(double PHlimb_star)
        {
            _Phpo = PHlimb_star * _ZR;
        }

        private void calc_R1H_shest(double Ukp1, double ksi, double KPH, double r0)
        {
            _R1H_shest = (500 * Math.Pow(_Ph_shest, 3) * Ukp1 * ksi * KPH) / (Constants.pi * r0); 
        }

        private void calc_R1H_kol(double Ukp1, double ksi, double KPH, double r0, double U_d1)
        {
            _R1H_kol = (500 * Math.Pow(_Ph_kol, 3) * Ukp1 * U_d1 * ksi * KPH) / (Constants.pi * r0);
        }

        private void calc_RH_lim(int NHO)
        {
            _RH_lim = Math.Pow(_Phpo, 3) * NHO;
        }

        private void calc_LH_shest()
        {
            _LH_shest = _RH_lim / _R1H_shest;
        }

        private void calc_LH_kol()
        {
            _LH_kol = _RH_lim / _R1H_kol;
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

        public double Ph_shest
        {
            get { return _Ph_shest; }
            set { _Ph_shest = value; }
        }

        public double Ph_kol
        {
            get { return _Ph_kol; }
            set { _Ph_kol = value; }
        }

        public double Phpo
        {
            get { return _Phpo; }
            set { _Phpo = value; }
        }

        public double Ph_limb
        {
            get { return _Ph_limb; }
            set { _Ph_limb = value; }
        }

        public double ZR
        {
            get { return _ZR; }
            set { _ZR = value; }
        }

        public double Ft
        {
            get { return _Ft; }
            set { _Ft = value; }
        }

        public double ZH
        {
            get { return _ZH; }
            set { _ZH = value; }
        }

        public double alpha_n
        {
            get { return _alpha_n; }
            set { _alpha_n = value; }
        }

        public double eps_beta 
        {
            get { return _eps_beta; }
            set { _eps_beta = value; }
        }

        public double eps_alpha
        {
            get { return _eps_alpha; }
            set { _eps_alpha = value; }
        }

        public double z_eps
        {
            get { return _z_eps; }
            set { _z_eps = value; }
        }

        public double KH_alpha 
        {
            get { return _KH_alpha; }
            set { _KH_alpha = value; }
        }

        public double KH_beta
        {
            get { return _KH_beta; }
            set { _KH_beta = value; }
        }

        public double KH_psi
        {
            get { return _KH_psi; }
            set { _KH_psi = value; }
        }

        public double KH_gamma 
        {
            get { return _KH_gamma; }
            set { _KH_gamma = value; }
        }

        public int precision_plav 
        {
            get { return _precision_plav; }
            set { _precision_plav = value; }
        }

        public int precision_sheroh
        {
            get { return _precision_sheroh; }
            set { _precision_sheroh = value; }
        }

        public double psi_bd
        {
            get { return _psi_bd; }
            set { _psi_bd = value; }
        }

        public double K0_beta
        {
            get { return _K0_beta; }
            set { _K0_beta = value; }
        }

        public double KH_omega
        {
            get { return _KH_omega; }
            set { _KH_omega = value; }
        }

        public double v 
        {
            get { return _v; }
            set { _v = value; }
        }

        public double KH_v 
        {
            get { return _KH_v; }
            set { _KH_v = value; }
        }

        public double Kj_delta
        {
            get { return _Kj_delta; }
            set { _Kj_delta = value; }
        }

        public double Kve
        {
            get { return _Kve; }
            set { _Kve = value; }
        }

        public double R1H_shest
        {
            get { return _R1H_shest; }
            set { _R1H_shest = value; }
        }

        public double R1H_kol
        {
            get { return _R1H_kol; }
            set { _R1H_kol = value; }
        }

        public double RH_lim
        {
            get { return _RH_lim; }
            set { _RH_lim = value; }
        }

        public double LH_shest 
        {
            get { return _LH_shest; }
            set { _LH_shest = value; }
        }

        public double LH_kol
        {
            get { return _LH_kol; }
            set { _LH_kol = value; }
        }
    }
}
