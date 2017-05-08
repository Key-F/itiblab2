using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itiblab2_next
{
    class paramsNS
    {

        public static double net(double[] w, List<double> x, int p) // x - полученные значения
        {
            double net = 0;
            for (int k = 1; k < p; k++)
            {
                net = net + w[k] * x[k - 1]; // add
            }
            return net = net + w[0]; // + w0
        }
        public static double delta(double t, double y)
        {
            return t - y;
        }
        
        public static double[] pereshetW(double[] w, List<double> x, double nu, double delta_)
        {

            for (int i = 0; i < w.Length - 1; i++)
                w[i] = w[i] + nu * delta_ * x[i];
            w[w.Length - 1] = w[w.Length - 1] + nu * delta_ * 1;
            return w;
        }
        public static double error(int N, List<double> model, double[] real) // N - число точек
        {
            double temp = 0;
            for (int i = 0; i < N; i++) // N
                temp = temp + Math.Pow((model[i] - real[i]), 2);
            return Math.Sqrt(temp);
        }
    }
}
