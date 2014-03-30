using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNTU_project
{
    class DataGenerator
    {
        //Параметры геометрического рассчета

        private int ma; //полная масса автомобиля
        private int Pemax; //максимальная мощность
        private int Memax; //максимальный крутящий момент
        private int np; //частота при максимальной мощности
        private int nm; //частота при максимальном крутящем моменте
        private int Vamax; //максимальная скорость автомобиля
        private double r0; //радиус качения колеса
        private string vehicleType; //тип автомобиля (легковой, грузовой, автобус городской, автобус междугородний, самосвал, многоприводный автомобиль)
        private int L0; //гарантированный пробег
        private double x_kol; //смещение 
        private double x_shest;

        
        private int gears; //количество передач
        private double Ukp; //передаточное число

        private struct GearsAndUkp//возможно сделать ее в методе локальной?
        {
            public int gears;
            public double Ukp;
        }
        private List<GearsAndUkp> gearsToUkp; //таблица связи между gears и Uikp    

        //Диапазоны
        private float Ka; //коэффициент межосевого расстояния [8.5 .. 9.6] с шагом 0.1
        private int beta; //угол наклона зубьев для ручного ввода [7 .. 22], в проге 22
        private float mn; //нормальный модуль [3.75 .. 6.5] с шагом 0.25
        private float coef_bw; //коэффициент ширины зубчатых венцов [0.19 .. 0.22] с шагом 0.01

        //Ограничение
        private int zmin; //минимальное количество зубьев, должно быть >= 12

        //Константы, параметры исходного контура
        public const int alpha = 20;
        public const int ha_star = 1;
        public const double hf_star = 1.25;
        public const double c_star = 0.25;
        
        //Просто константы
        public const double N = 0.1; //для косозубых передач
        public const double g = 9.81;
        public const double pi = 3.1415926535897932384626433832795;
        public const int K_alpha = 1;
        public const int k_tau = 1;
        public const int kp = 1;
        public const double KF_mu_kol = 0.95;
        public const double KF_mu_shest = 1.05;
        public const int KFx = 1; //коэффициент учитывающий влияние размеров зубчатых колес
        public const int YR = 1; //коэффициент учитывающий особенности технологии обработки переходной кривой зуба
        public const double KFC = 1.3;
        public const double beta_c = 1.7; //коэффициент запаса зацепления


        //Параметры проверочного рассчета

        private List<double> ksiList; //относительно использование передач в общем, считается по огромной таблице
        private float z_eps; //коэффициент учитывающий осевое перекрытие, находится по злоебучему графику
        private float KH_psi; //коэффициент учитывающий повышение интенсивности нагрузки на околополосных участках контактных линий
        private int degreeOfAccuracy; //степень точности
        private double KH_gamma; //коэффициент учитывающий неточность распределения нагрузки между зубьями
        private double KF_alpha; //коэффициент какойто
        private double K_beta_0; //коэффициент учитывающий неравномерность распределения нагрузки по ширине венца в начальный период работы передачи, считается по графику на основе выбраной картинки
        private int delta0; //расчетная производственная погрешность зубчатых колес, по таблице
        private double Kve; //коэффициент учитывающий влияние внешних динамических нагрузок, по графику
        private int classOfRoughness; //класс шероховатости
        private int degreeOfEvenness; //степень плавности
        private double ZR; //коэффициент учитывающий шероховатость активной поверхности зубьев, по таблице
        private double YF_0;
        private double KHw; //по таблице
        private double KFw;
        private string steelGrade;

        public struct ParametersOfSteel //Характеристики для выбранного типа стали
        {
            public string steelGrade; //марка стали
            public string thermalTreatment; //термообработка
            public int surfaceHardness; //твердость поверхности
            public int coreHardness; //твердость сердцевины
            public int sigmaFlimb_c_star;
            public int NFO; //циклы
            public int mF;
            public double PHlimb_star;
            public int NHO; //циклы
            public int mH; //циклы
            public int sigmaFlimM;
            public int PHlimM;
        }
        private List<ParametersOfSteel> steelList;


        //функция для задания начальной таблица параметров стали
        private void setDefaultSteelList()
        {
            var steel = new ParametersOfSteel();

            steel.steelGrade = "12Х2Н4А";
            steel.thermalTreatment = "Цементация";
            steel.surfaceHardness = 63;
            steel.coreHardness = 41;
            steel.sigmaFlimb_c_star = 430;
            steel.NFO = 4000000;
            steel.mF = 9;
            steel.PHlimb_star = 19;
            steel.NHO = 120000000;
            steel.mH = 3;
            steel.sigmaFlimM = 1900;
            steel.PHlimM = 190;
            this.steelList.Add(steel);

            steel = new ParametersOfSteel();
            steel.steelGrade = "12ХН3А";
            steel.thermalTreatment = "Цементация";
            steel.surfaceHardness = 63;
            steel.coreHardness = 33;
            steel.sigmaFlimb_c_star = 380;
            steel.NFO = 4000000;
            steel.mF = 9;
            steel.PHlimb_star = 18.5;
            steel.NHO = 120000000;
            steel.mH = 3;
            steel.sigmaFlimM = 1850;
            steel.PHlimM = 190;
            this.steelList.Add(steel);

            steel.steelGrade = "15XГНТА";
            steel.thermalTreatment = "Цементация";
            steel.surfaceHardness = 63;
            steel.coreHardness = 42;
            steel.sigmaFlimb_c_star = 410;
            steel.NFO = 4000000;
            steel.mF = 9;
            steel.PHlimb_star = 19;
            steel.NHO = 120000000;
            steel.mH = 3;
            steel.sigmaFlimM = 1700;
            steel.PHlimM = 190;
            this.steelList.Add(steel);

            steel.steelGrade = "15ХГН2ТА";
            steel.thermalTreatment = "Цементация";
            steel.surfaceHardness = 63;
            steel.coreHardness = 42;
            steel.sigmaFlimb_c_star = 420;
            steel.NFO = 4000000;
            steel.mF = 9;
            steel.PHlimb_star = 19;
            steel.NHO = 120000000;
            steel.mH = 3;
            steel.sigmaFlimM = 1750;
            steel.PHlimM = 190;
            this.steelList.Add(steel);

            steel.steelGrade = "18ХГТ";
            steel.thermalTreatment = "Цементация";
            steel.surfaceHardness = 63;
            steel.coreHardness = 35;
            steel.sigmaFlimb_c_star = 370;
            steel.NFO = 4000000;
            steel.mF = 9;
            steel.PHlimb_star = 18;
            steel.NHO = 120000000;
            steel.mH = 3;
            steel.sigmaFlimM = 1600;
            steel.PHlimM = 190;
            this.steelList.Add(steel);

            steel.steelGrade = "18Х2Н4ВА";
            steel.thermalTreatment = "Цементация";
            steel.surfaceHardness = 63;
            steel.coreHardness = 40;
            steel.sigmaFlimb_c_star = 430;
            steel.NFO = 4000000;
            steel.mF = 9;
            steel.PHlimb_star = 21;
            steel.NHO = 120000000;
            steel.mH = 3;
            steel.sigmaFlimM = 1950;
            steel.PHlimM = 190;
            this.steelList.Add(steel);

            //необходимо дополнить
        }

        //функция для получения параметров стали исходя из марки стали
        public ParametersOfSteel getParametersOfSteel(string steelGrade)
        {
            var parameters = new ParametersOfSteel();
            bool findFlag = false;

            foreach (var steel in this.steelList)
            {
                if (steel.steelGrade == steelGrade)
                {
                    parameters = steel;
                    findFlag = true;
                }
            }

            if (findFlag == false)
            {
                throw new ArgumentException("Марка стали не найдена");
            }

            return parameters;
        }
        
    }
}
