using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itiblab2_next
{
    class paramsNS
    {

        public static double net(double[] w, double[] x, int p, int n)
        {
            double net = 0;
            for (int k = 0; k < p - 1; k++)
            {
                net = net + w[k] * x[n - p + k - 1]; // add
            }
            return net = net + w[p - 1]; // + w0
        }
        public static double delta(double t, double y)
        {
            return t - y;
        }
        public static double[] pereshetW(double[] w, double[] x, double net_, double nu, double delta_)
        {

            for (int i = 0; i < w.Length - 1; i++)
                w[i] = w[i] + nu * delta_ * x[i];
            w[w.Length - 1] = w[w.Length - 1] + nu * delta_ * 1;
            return w;
        }
        public static double error(int N, double[] model, double[] real) // N - число точек
        {
            double temp = 0;
            for (int i = 0; i < N; i++)
                temp = temp + Math.Pow((model[i] - real[i]), 2);
            return Math.Sqrt(temp);
        }
    }
}
