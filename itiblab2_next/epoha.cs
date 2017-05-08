using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itiblab2_next
{
    class epoha
    {
        public int nomer;
        public double[] Y; // Выходной сигнал
        public double[] W; // Вектор весов
        public double E; // Суммарная ошибка


        public epoha(List<double> w)
        {

            W = w.ToArray();

        }
    }
}
