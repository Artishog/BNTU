using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNTU_project
{
    public class Gearwheel: ParentElement
    {
        private double _x_kol = -0.28; //смещение (для низшей пары)
        private double _x_shest = 0.28; //смещение (для низшей пары)
        private int _beta = 22; //угол наклона зубьев [7 .. 22]
        private double _mn; //нормальный модуль [3.75 ..6.5] с шагом 0.25
        private double _coef_bw; //коэффициент ширины зубчатых венцов [0.19 .. 0.22] с шагом 0.01
        private int _alpha = 20; //параметр исходного контура 
        private int _ha_star = 1; //параметр исходного контура
        private double _hf_star = 1.25; //параметр исходного контура
        private double _c_star = 0.25; //параметр исходного контура

        public Gearwheel()
        {
            this._inputPropertyList = new List<string>();
            inputPropertyList.AddRange(new String[] {
                    "Смещение ведомого колеса (для низшей пары)",
                    "Смещение ведущего колеса (для низшей пары)",
                    "Угол наклона зубьев",
                    "Нормальный модуль",
                    "Коэффициент ширины зубчатых венцов",
                    "Угол главного профиля",
                    "Коэффициент высоты головки зуба",
                    "Коэффициент высоты ножки",
                    "Коэффициент радиального зазора",
            });
        }

        public override object getByName(string name)
        {
            switch (name)
            {
                case "Смещение ведомого колеса (для низшей пары)":
                    return _x_kol;
                    break;
                case "Смещение ведущего колеса (для низшей пары)":
                    return _x_shest;
                    break;
                case "Угол наклона зубьев":
                    return _beta;
                    break;
                case "Нормальный модуль":
                    return _mn;
                    break;
                case "Коэффициент ширины зубчатых венцов":
                    return _coef_bw;
                    break;
                case "Угол главного профиля":
                    return _alpha;
                    break;
                case "Коэффициент высоты головки зуба":
                    return _ha_star;
                    break;
                case "Коэффициент высоты ножки":
                    return _hf_star;
                    break;
                case "Коэффициент радиального зазора":
                    return _c_star;
                    break;

                default:
                    return null;
                    break;
            }
        }

        public override void setByName(string name, object value)
        {
            switch (name)
            {
                case "Смещение ведомого колеса (для низшей пары)":
                    _x_kol = Convert.ToDouble(value);
                    break;
                case "Смещение ведущего колеса (для низшей пары)":
                    _x_shest = Convert.ToDouble(value);
                    break;
                case "Угол наклона зубьев":
                    _beta = Convert.ToInt32(value);
                    break;
                case "Нормальный модуль":
                    _mn = Convert.ToDouble(value);
                    break;
                case "Коэффициент ширины зубчатых венцов":
                    _coef_bw = Convert.ToDouble(value);
                    break;
                case "Угол главного профиля":
                    _alpha = Convert.ToInt32(value);
                    break;
                case "Коэффициент высоты головки зуба":
                    _ha_star = Convert.ToInt32(value);
                    break;
                case "Коэффициент высоты ножки":
                    _hf_star = Convert.ToDouble(value);
                    break;
                case "Коэффициент радиального зазора":
                    _c_star = Convert.ToDouble(value);
                    break;
            }
        }

        //Property
        public double x_kol
        {
            get { return _x_kol; }
            set { _x_kol = value; }
        }

        public double x_shest
        {
            get { return _x_shest; }
            set { _x_shest = value; }
        }

        public int beta
        {
            get { return _beta; }
            set { _beta = value; }
        }

        public double mn
        {
            get { return _mn; }
            set { _mn = value; }
        }

        public double coef_bw
        {
            get { return _coef_bw; }
            set { _coef_bw = value; }
        }

        public int alpha
        {
            get { return _alpha; }
            set { _alpha = value; }
        }

        public int ha_star
        {
            get { return _ha_star; }
            set { _ha_star = value; }
        }

        public double hf_star
        {
            get { return _hf_star; }
            set { _hf_star = value; }
        }

        public double c_star
        {
            get { return _c_star; }
            set { _c_star = value; }
        }
    }
}
