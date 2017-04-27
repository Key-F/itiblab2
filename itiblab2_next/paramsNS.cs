using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itiblab2_next
{
    class paramsNS
    {

        public static double net(double[] w, int[] x, int p, int n)
        {
            double net = 0;
            for (int k = 0; k < p; k++)
            {
                net = net + w[k] * x[n - p + k - 1]; // add
            }
            return net = net + w[4]; // + w0
        }
        public static double[] pereshetW(double[] w, int[] x, double net_, double nu, double delta_)
        {

            for (int i = 0; i < w.Length - 1; i++)
                w[i] = w[i] + nu * delta_ * x[i];
            w[4] = w[4] + nu * delta_ * 1;
            return w;
        }
    }
}
