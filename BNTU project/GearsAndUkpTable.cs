using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNTU_project
{
    public class GearsAndUkpTable
    {
        private struct GearsAndUkp//возможно сделать ее в методе локальной?
        {
            public int gearNumber;
            public double Ukp;
        }

        private List<GearsAndUkp> gearsToUkp; //таблица связи между gears и Uikp

        public GearsAndUkpTable()
        {
            gearsToUkp = new List<GearsAndUkp>();
        }

        public void add(int gear, double Ukp)
        {
            if (Ukp < 0)
            {
                throw new Exception("Передаточное число не может быть меньше 0");
            }
            var gearAndUkp = new GearsAndUkp();
            gearAndUkp.gearNumber = gear;
            gearAndUkp.Ukp = Ukp;
            gearsToUkp.Add(gearAndUkp);
        }

        public double getUkpByGear(int gear)
        {
            double result = -1;

            foreach (var gearAndUkp in gearsToUkp)
            {
                if (gearAndUkp.gearNumber == gear)
                {
                    result = gearAndUkp.Ukp;
                    break;
                }
            }

            if (result == -1)
            {
                throw new Exception("Для этой передачи передаточное число не найдено");
            }

            return result;
        }

        public void clearTable()
        {
            gearsToUkp = new List<GearsAndUkp>();
        }
    }
}
