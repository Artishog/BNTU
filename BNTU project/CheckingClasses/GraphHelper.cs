using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNTU_project
{
    public static class GraphHelper
    {
        public static double GetYbyX_increasing(double X, double minX, double maxX, double minY, double maxY)
        {
            double Y = -1;

            if (X <= minX)
                X = minX;
            
            if (X >= maxX)
                X = maxX;
            
            Y = minY + (X - minX) / (maxX - minX) * (maxY - minY);

            return Y;
        }

        public static double GetYbyX_decreasing(double X, double minX, double maxX, double minY, double maxY)
        {
            double Y = -1;

            if (X <= minX)
                X = minX;

            if (X >= maxX)
                X = maxX;

            Y = maxY - (X - minX) / (maxX - minX) * (maxY - minY);

            return Y;
        }
    }
}
