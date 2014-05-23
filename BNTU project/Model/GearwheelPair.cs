using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNTU_project
{
    public class GearwheelPair: ParentElement
    {
        private double _beta_d; //угол наклона зубьев
        private double _mt; //окружной модуль
        private int _z_sum; //суммарное число зубьев кинематической пары
        private int _z_shest; //число зубьев ведущего колеса
        private int _z_kol; //число зубьев ведомого колеса
        private int _zmin; //минимальное количество зубьев >=12
        private double _U; //требуемое передаточное число пары
        private double _U_d; //действительное передаточное число
        private double _delta_U; //погрешность
        private double _d_kol; //делительный диаметр ведомого колеса
        private double _d_shest; //делительный диаметр ведущего колеса
        private double _da_kol; //диаметр вершин зубьев ведомого колеса
        private double _da_shest; //диаметр вершин зубьев ведущего колеса
        private double _df_kol; //диаметр впадин зубьев ведомого колеса
        private double _df_shest; //диаметр впадин зубьев ведущего колеса
        private double _bw; //ширина зубчатого венца
        private double _bf_shest; //рабочая ширина зубчатого венца
        private double _aw2_d; //действительное межосевое расстояние

        public GearwheelPair()
        {
            this._outputPropertyList = new List<string>();

            outputPropertyList.AddRange(new String[] {
                        "Угол наклона зубьев",
                        "Окружной модуль",
                        "Суммарное число зубьев кинематической пары",
                        "Число зубьев ведущего колеса",
                        "Число зубьев ведомого колеса",
                        "Требуемое передаточное число пары",
                        "Действительное передаточное число",
                        "Погрешность",
                        "Делительный диаметр ведомого колеса",
                        "Делительный диаметр ведущего колеса",
                        "Диаметр вершин зубьев ведомого колеса",
                        "Диаметр вершин зубьев ведущего колеса",
                        "Диаметр впадин зубьев ведомого колеса",
                        "Диаметр впадин зубьев ведущего колеса",
                        "Ширина зубчатого венца",
                        "Рабочая ширина зубчатого венца",
            });
        }

        public override object getByName(string name)
        {
            switch (name)
            {
                case "Угол наклона зубьев":
                    return _beta_d;
                    break;
                case "Окружной модуль":
                    return _mt;
                    break;
                case "Суммарное число зубьев кинематической пары":
                    return _z_sum;
                    break;
                case "Число зубьев ведущего колеса":
                    return _z_shest;
                    break;
                case "Число зубьев ведомого колеса":
                    return _z_kol;
                    break;
                case "Требуемое передаточное число пары":
                    return _U;
                    break;
                case "Действительное передаточное число":
                    return _U_d;
                    break;
                case "Погрешность":
                    return _delta_U;
                    break;
                case "Делительный диаметр ведомого колеса":
                    return _d_kol;
                    break;
                case "Делительный диаметр ведущего колеса":
                    return _d_shest;
                    break;
                case "Диаметр вершин зубьев ведомого колеса":
                    return _da_kol;
                    break;
                case "Диаметр вершин зубьев ведущего колеса":
                    return _da_shest;
                    break;
                case "Диаметр впадин зубьев ведомого колеса":
                    return _df_kol;
                    break;
                case "Диаметр впадин зубьев ведущего колеса":
                    return _df_shest;
                    break;
                case "Ширина зубчатого венца":
                    return _bw;
                    break;
                case "Рабочая ширина зубчатого венца":
                    return _bf_shest;
                    break;

                default:
                    return null;
                    break;
            }
        }

        public override void setByName(string name, object value)
        {
        }

        //Рассчет параметров
        public void calc_FirstPair(double aw, int beta, double mn, int ha_star, double hf_star, double c_star, double coef_bw, double U1st)
        {
            _U = U1st;
            calc_z_sum(aw, beta, mn);
            calc_beta_d(aw, mn);
            calc_mt(mn);
            calc_z_shest();
            calc_z_kol();
            calc_U_d();
            calc_delta_U();
            calc_d_shest();
            calc_d_kol();
            calc_da_shest(mn, ha_star);
            calc_da_kol(mn, ha_star);
            calc_df_shest(mn, hf_star, c_star);
            calc_df_kol(mn, hf_star, c_star);
            calc_bw(coef_bw, aw);
            calc_bf_shest();
        }

        public void calc_SecondPair(GearwheelPair gearwheelPair1, int beta, double mn, int ha_star, double hf_star, double c_star, double Urk, double U2st)
        {
            _U = U2st;
            _mt = gearwheelPair1.mt;
            _z_shest = gearwheelPair1._z_kol;
            _d_shest = gearwheelPair1.d_kol;
            _da_shest = gearwheelPair1.da_kol;
            _df_shest = gearwheelPair1.df_kol;
            _bw = gearwheelPair1.bw;
            _bf_shest = gearwheelPair1.bw;
            _beta_d = gearwheelPair1.beta_d;
            calc2_z_kol(gearwheelPair1.z_kol, Urk, gearwheelPair1.U_d);
            calc2_z_sum();
            calc_U_d();
            calc_delta_U();
            calc_d_kol();
            calc_da_kol(mn, ha_star);
            calc_df_kol(mn, hf_star, c_star);
            calc2_aw2_d(gearwheelPair1.beta_d, mn);
        }

        public void calc2_aw2_d(double betd_d, double mn)
        {
            _aw2_d = (mn * _z_sum) / (2 * Math.Cos(DegreeToRadian(beta_d)));
        }

        public void calc2_z_kol(double z1_kol, double Urk, double U1_d)
        {
            _z_kol = (int)Math.Round(z1_kol * Urk / U1_d);
        }

        public void calc2_z_sum()
        {
            _z_sum = _z_kol + _z_shest;
        }

        public void calc_z_sum(double aw, int beta, double mn)
        {
            _z_sum = (int)Math.Round((2 * aw * Math.Cos(DegreeToRadian(beta))) / mn);
        }

        public void calc_beta_d(double aw, double mn)
        {
            _beta_d = RadianToDegree(Math.Acos((0.5 * mn * _z_sum) / aw));
        }

        public void calc_mt(double mn)
        {
            _mt = mn / Math.Cos(DegreeToRadian(_beta_d));
        }

        public void calc_z_shest()
        {
            _z_shest = (int)Math.Round(_z_sum / (1 + _U));
        }

        public void calc_z_kol()
        {
            _z_kol = _z_sum - _z_shest;
        }

        public void calc_U_d()
        {
            _U_d = (double)_z_kol / (double)_z_shest;
        }

        public void calc_delta_U()
        {
            _delta_U = Math.Abs((_U - _U_d) / _U);
        }

        public void calc_d_shest()
        {
            _d_shest = (double)_z_shest * _mt;
        }

        public void calc_d_kol()
        {
            _d_kol = (double)_z_kol *_mt;
        }

        public void calc_da_shest(double mn, int ha_star)
        {
            _da_shest = _d_shest + 2 * mn * ha_star;
        }

        public void calc_da_kol(double mn, int ha_star)
        {
            _da_kol = _d_kol + 2 * mn * ha_star;
        }

        public void calc_df_shest(double mn, double hf_star, double c_star)
        {
            _df_shest = _d_shest - 2 * mn * (hf_star + c_star); 
        }

        public void calc_df_kol(double mn, double hf_star, double c_star)
        {
            _df_kol = _d_kol - 2 * mn * (hf_star + c_star);
        }

        public void calc_bw(double coef_bw, double aw)
        {
            _bw = Math.Ceiling(coef_bw * aw);
        }

        public void calc_bf_shest()
        {
            _bf_shest = Math.Ceiling(1.1 * _bw);
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
        public int z_sum
        {
            get { return _z_sum; }
            set { _z_sum = value; }
        }

        public double beta_d
        {
            get { return _beta_d; }
            set { _beta_d = value; }
        }

        public double mt
        {
            get { return _mt; }
            set { _mt = value; }
        }

        public int z_shest
        {
            get { return _z_shest; }
            set { _z_shest = value; }
        }

        public double U
        {
            get { return _U; }
            set { _U = value; }
        }

        public int z_kol
        {
            get { return _z_kol; }
            set { _z_kol = value; }
        }

        public int zmin
        {
            get { return _zmin; }
        }

        public double U_d
        {
            get { return _U_d; }
            set { _U_d = value; }
        }

        public double delta_U
        {
            get { return _delta_U; }
            set { _delta_U = value; }
        }

        public double d_shest
        {
            get { return _d_shest; }
            set { _d_shest = value; }
        }

        public double d_kol
        {
            get { return _d_kol; }
            set { _da_kol = value; }
        }

        public double da_shest
        {
            get { return _da_shest; }
            set { _da_shest = value; }
        }

        public double da_kol
        {
            get { return _da_kol; }
            set { _da_kol = value; }
        }

        public double df_shest
        {
            get { return _df_shest; }
            set { _df_shest = value; }
        }

        public double df_kol
        {
            get { return _df_kol; }
            set { _df_kol = value; }
        }

        public double bw
        {
            get { return _bw; }
            set { _bw = value; }
        }

        public double bf_shest
        {
            get { return _bf_shest; }
            set { _bf_shest = value; }
        }

        public double aw2_d
        {
            get { return _aw2_d; }
            set { _aw2_d = value; }
        }
    }
}
