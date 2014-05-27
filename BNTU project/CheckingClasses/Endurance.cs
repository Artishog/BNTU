using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNTU_project
{
    public class Endurance
    {
        private double _Mj_max; //Максимальный динамический момент
        private double _Kj_M; //Коэффициент максимальной динамической нагрузки
        private double _Kj = 1.5; //Коэффициент динамичности
        private double _sigmaFmax_shest; //Максимальное расчетное изгибное напряжение ведущего колеса
        private double _sigmaFmax_kol; //Максимальное расчетное изгибное напряжение ведомого колеса
        private double _PHmax_shest; //Максимальное расчетное контактное напряжение ведущего колеса
        private double _PHmax_kol; //Максимальное расчетное контактное напряжение ведомого колеса
        private double _sigmaHmax; //Предельное контактное напряжение

        public Endurance()
        {
        }

        //Методы
        public void calc_All(double Mc, double Mp, double sigmaF_shest, double sigmaF_kol, double Ph_shest, double Ph_kol)
        {
            calc_Mj_max(Mc);
            calc_Kj_M(Mp);
            calc_sigmaFmax_shest(sigmaF_shest);
            calc_sigmaFmax_kol(sigmaF_kol);
            calc_PHmax_shest(Ph_shest);
            calc_PHmax_kol(Ph_kol);
        }

        public bool isValid(int PHlimM, int sigmaFlimM)
        {
            var result = true;

            if (_PHmax_shest > 0.95 * PHlimM)
                result = false;

            if (_PHmax_kol > 0.95 * PHlimM)
                result = false;

            if (_sigmaFmax_shest > 0.95 * sigmaFlimM)
                result = false;

            if (_sigmaFmax_kol > 0.95 * sigmaFlimM)
                result = false;

            return result;
        }

        private void calc_Mj_max(double Mc)
        {
            _Mj_max = 1.35 * Mc;
        }

        private void calc_Kj_M(double Mp)
        {
            _Kj_M = _Mj_max / Mp;
        }

        private void calc_sigmaFmax_shest(double sigmaF_shest)
        {
           // _sigmaFmax_shest = _Kj * Ft * YF_shest * Y_eps * KF_alpha * KF_beta * KF_mu * KF_v / bf_shest1 / mn;
            _sigmaFmax_shest = sigmaF_shest * _Kj / 1000000;
        }

        private void calc_sigmaFmax_kol(double sigmaF_kol)
        {
            _sigmaFmax_kol = sigmaF_kol * _Kj / 1000000;
        }

        private void calc_PHmax_shest(double Ph_shest)
        {
            _PHmax_shest = Ph_shest * _Kj;
        }

        private void calc_PHmax_kol(double Ph_kol)
        {
            _PHmax_kol = Ph_kol * _Kj;
        }

        public double Mj_max
        {
            get { return _Mj_max; }
            set { _Mj_max = value; }
        }

        public double Kj_M
        {
            get { return _Kj_M; }
            set { _Kj_M = value; }
        }

        public double Kj
        {
            get { return _Kj; }
            set { _Kj = value; }
        }

        public double sigmaFmax_shest
        {
            get { return _sigmaFmax_shest; }
            set { _sigmaFmax_shest = value; }
        }

        public double sigmaFmax_kol
        {
            get { return _sigmaFmax_kol; }
            set { _sigmaFmax_kol = value; }
        }

        public double PHmax_shest
        {
            get { return _PHmax_shest; }
            set { _PHmax_shest = value; }
        }

        public double PHmax_kol
        {
            get { return _PHmax_kol; }
            set { _PHmax_kol = value; }
        }
    }
}
