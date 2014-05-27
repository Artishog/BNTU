using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNTU_project
{
    public class Steel
    {
        public struct SteelKind //Характеристики для выбранного типа стали
        {
            public int id;
            public string steelGrade; //марка стали
            public string thermalTreatment; //термообработка
            public int surfaceHardness; //твердость поверхности
            public int coreHardness; //твердость сердцевины
            public int sigmaFlimb_c_star; //
            public int NFO; //циклы
            public int mF;
            public double PHlimb_star;
            public int NHO; //циклы
            public int mH; //циклы
            public int sigmaFlimM;
            public int PHlimM;
        }
        private List<SteelKind> steelList = new List<SteelKind>();
        private SteelKind _currentSteel;

        public Steel()
        {
            setDefaultSteelList();
        }

        public void setCurrentSteelByGrade(string steelGrade)
        {
            switch (steelGrade) 
            {
                case "12Х2Н4А":
                    _currentSteel = steelList[0];
                    break;
                case "12ХН3А":
                    _currentSteel = steelList[1];
                    break;
                case "15XГНТА":
                    _currentSteel = steelList[2];
                    break;
                case "15ХГН2ТА":
                    _currentSteel = steelList[3];
                    break;
                case "18ХГТ":
                    _currentSteel = steelList[4];
                    break;
                case "18Х2Н4ВА":
                    _currentSteel = steelList[5];
                    break;
                case "20Х2Н4А":
                    _currentSteel = steelList[6];
                    break;
            }
        }

        //функция для задания начальной таблица параметров стали
        private void setDefaultSteelList()
        {
            var steel = new SteelKind();
            steel.id = 1;
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

            steel = new SteelKind();
            steel.id = 2;
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

            steel = new SteelKind();
            steel.id = 3;
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

            steel = new SteelKind();
            steel.id = 4;
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

            steel = new SteelKind();
            steel.id = 5;
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

            steel = new SteelKind();
            steel.id = 6;
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

            steel = new SteelKind();
            steel.id = 7;
            steel.steelGrade = "20Х2Н4А";
            steel.thermalTreatment = "Цементация";
            steel.surfaceHardness = 63;
            steel.coreHardness = 41;
            steel.sigmaFlimb_c_star = 460;
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


        public SteelKind currentSteel
        {
            get { return _currentSteel; }
        }
    }
}
