using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNTU_project
{
    public abstract class TransferGearbox: ParentElement
    {  
        protected double _Ka; //кооэфициент межосевого расстояния [8.5 ... 9.6]
        protected double _aw1; //межосевое расстояние входного и промежуточного валов
        protected double _aw2; //межосевое расстояние промежуточного и выходных валов
        protected double _U1st; //передаточное число первой ступени
        protected double _U2st; //передаточное число второй ступени
        protected double _U_d1; //действительное передаточное число первой пары
        protected double _U_d2; //действительное передаточное число второй пары
        protected double _L1; //длина внутреннего объема раздаточной коробки
        protected double _L2; //длина раздаточной коробки
        protected double _ld; //длина дифференциала
        protected double _H1; //высота внутреннего периметра поперечного сечения
        protected double _H2; //высота внешнего периметра поперечного сечения
        protected double _delta = 7; //средняя толщина стенок картера
        protected double _B1; //ширина внутреннего периметра поперечного сечения
        protected double _B2; //ширина внешнего периметра поперечного сечения
        protected double _dmax; //диаметр наибольшего колеса или корпуса дифференциала
        protected double _M0; //крутящий момент на выходном валу коробки передач
        protected double _V_korp; //объем корпуса раздаточной коробки
        protected double _m_korp; //масса корпуса раздаточной коробки
        protected double _d1; //диаметр входного вала
        protected double _d2; //диаметр промежуточного вала
        protected double _d3; //диаметр выходных валов
        protected double _Vv1; //объем входного вала
        protected double _Vv2; //объем промежуточного вала
        protected double _Vv3; //объем выходных валов
        protected double _mv1; //масса входного вала
        protected double _mv2; //масса промежуточного вала
        protected double _mv3; //масса выходных валов
        protected double _Vsh1; //объем шестерни входного вала
        protected double _Vsh2; //объем шестерни промежуточного вала
        protected double _Vsh3; //объем шестерни выходного вала
        protected double _msh1; //масса шестерни входного вала
        protected double _msh2; //масса шестерни промежуточного вала
        protected double _msh3; //масса шестерни выходного вала
        protected double _msh4; //масса шестерни
        protected double _msh5; //масса шестерни
        protected double _mrk; //масса раздаточной коробки
        protected double _Supr = 50; //место отведенное под органы управления
        protected double _mupr = 0; //масса органов управления
        protected int _kinematicScheme; //номер кинематической схемы
        protected bool _needRecalculation = false;

        protected Car car;
        protected GearwheelPair gearwheelPair1;
        protected GearwheelPair gearwheelPair2;
        protected Differential differential;

        public abstract void calc_allStep1();
        public abstract void calc_allStep2();
        public abstract void calc_allStep3();

        public TransferGearbox(Car car, GearwheelPair gearwheelPair1, GearwheelPair gearwheelPair2, Differential differential)
        {
            this.car = car;
            this.gearwheelPair1 = gearwheelPair1;
            this.gearwheelPair2 = gearwheelPair2;
            this.differential = differential;

            this._outputPropertyList = new List<string>();
            this._inputPropertyList = new List<string>();

            inputPropertyList.AddRange(new String[] {
                        "Средняя толщина стенок картера",
                        "Коэффициент межосевого расстояния"
            });
        
            outputPropertyList.AddRange(new String[] {
                        "Масса раздаточной коробки",
                        "Межосевое расстояние входного и промежуточного валов",
                        "Межосевое расстояние промежуточного и выходных валов",
                        "Передаточное число первой ступени",
                        "Передаточное число второй ступени",
                        "Средний диаметр входного вала",
                        "Средний диаметр промежуточного вала",
                        "Средний диаметр выходных валов",
                        "Длина раздаточной коробки",
                        "Высота внутреннего периметра поперечного сечения",
                        "Высота внешнего периметра поперечного сечения",
                        "Ширина внутреннего периметра поперечного сечения",
                        "Ширина внешнего периметра поперечного сечения",
                        "Диаметр наибольшего колеса или корпуса дифференциала",
            });
        }

        public override object getByName(string name)
        {
            switch (name)
            {
                case "Масса раздаточной коробки":
                    return _mrk;
                case "Межосевое расстояние входного и промежуточного валов":
                    return _aw1;
                case "Межосевое расстояние промежуточного и выходных валов":
                    return _aw2;
                case "Передаточное число первой ступени":
                    return _U1st;
                case "Передаточное число второй ступени":
                    return _U2st;
                case "Коэффициент межосевого расстояния":
                    return _Ka;
                case "Средний диаметр входного вала":
                    return _d1;
                case "Средний диаметр промежуточного вала":
                    return _d2;
                case "Средний диаметр выходных валов":
                    return _d3;
                case "Длина раздаточной коробки":
                    return _L2;
                case "Высота внутреннего периметра поперечного сечения":
                    return _H1;
                case "Высота внешнего периметра поперечного сечения":
                    return _H2;
                case "Ширина внутреннего периметра поперечного сечения":
                    return _B1;
                case "Ширина внешнего периметра поперечного сечения":
                    return _B2;
                case "Диаметр наибольшего колеса или корпуса дифференциала":
                    return _dmax;
                case "Средняя толщина стенок картера":
                    return _delta;

                default:
                    return null;
            }
        }

        public override void setByName(string name, object value)
        {
            switch (name)
            {
                case "Средняя толщина стенок картера":
                    _delta = Convert.ToDouble(value);
                    break;
                case "Коэффициент межосевого расстояния":
                    _Ka = Convert.ToDouble(value);
                    break;
            }
        }

        //Property


        public Car Car
        {
            get { return car; }
            set { car = value; }
        }
        public GearwheelPair GearwheelPair1
        {
            get { return gearwheelPair1; }
            set { gearwheelPair1 = value; }
        }
        public GearwheelPair GearwheelPair2
        {
            get { return gearwheelPair2; }
            set { gearwheelPair2 = value; }
        }
        public Differential Differential
        {
            get { return differential; }
            set { differential = value; }
        }
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

        public double U_d1
        {
            get { return _U_d1; }
            set { _U_d1 = value; }
        }

        public double U_d2
        {
            get { return _U_d2; }
            set { _U_d2 = value; }
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

        public double d1
        {
            get { return _d1; }
        }

        public double d2
        {
            get { return _d2; }
        }

        public double d3
        {
            get { return _d3; }
        }

        public double dmax
        {
            get { return _dmax; }
        }

        public double Supr
        {
            get { return _Supr; }
            set { _Supr = value; }
        }

        public double mupr
        {
            get { return _mupr; }
            set { _mupr = value; }
        }

        public int kinematicScheme
        {
            get { return _kinematicScheme; }
        }

        public bool needRecalculation
        {
            get { return _needRecalculation; }
            set { _needRecalculation = value; }
        }
    }
}
