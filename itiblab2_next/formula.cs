using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itiblab2_next
{
    class formula
    {
        public static double Err()
        {
            return 0;
        }
        public static double tsin(double t)
        {
            return t * t * Math.Sin(t);
        }

        public static double[] respred(int a, int b, int N)
        {
            double N1 = N; // Для деления 
            double shag = (Math.Abs(a) + Math.Abs(b)) / N1;
            double[] t = new double[N];
            t[0] = a;
            for (int i = 1; i < N; i++)
                t[i] = t[i - 1] + shag;
            return t;
        }
    }
}
