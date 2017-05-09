using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;
using System.Globalization; // Для работы с точкой в double (из textbox)

namespace itiblab2_next
{
    class formula
    {

       
        public static double ParseDouble1(string s)
        {
            double d;

            if (!Double.TryParse(s, NumberStyles.Float, CultureInfo.CurrentCulture, out d))
                Double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out d);

            return d;
        }
        public static List<double> converter(double[] x) // из массива в List
        {
            List<double> xc = new List<double>();
            for (int i = 0; i < x.Length; i++)
            {
                xc.Add(x[i]);
            }
            return xc;
        }


        public static double[] respred(double a, double b, int N)
        {
            double N1 = N; // Для деления 
            double shag = Math.Abs(a - b) / N1;
            double[] t = new double[N];
            t[0] = a;
            for (int i = 1; i < N; i++)
                t[i] = t[i - 1] + shag;
            return t;
        }

         public static List<double> getT(double[] t, int start, int end)
         {
             List<double> result = new List<double>();
             for (int i = start; i < end ; i++) 
                 result.Add(t[i]);
             return result;	
        }
       
        public static epoha obuch(double[] real, double[] t, int p, int n, int epoch, double nu)  // real - значения, расчитанные по формуле, n = 20; t - распределение
        {
            int k = 0; // Счетчик эпох
            List<epoha> Ep = new List<epoha>();            
            double dlta;
            double net1;
            List<double[]> netlist = new List<double[]>();
            List<double> nextw = Enumerable.Repeat(0.0, p + 1).ToList(); // Значения для весов следующей эпохи, заполняем нулями, +1 для w0
            double[] xline = t;                     
                do
                {
                    epoha Ep_ = new epoha(nextw);
                    Ep.Add(Ep_);
                    Ep[k].nomer = k;
                    //Ep[k].Y = new double[14];
                    double[] tempW = nextw.ToArray();
                    for (int i = 0; i < n - p; i++)
                    {
                        List<double> tempT = getT(real, i, i + p);
                        net1 = paramsNS.net(tempW, tempT, p);
                        dlta = paramsNS.delta(real[i + p], net1);                       
                        //Ep[k].Y[i] = net1;
                        tempW = paramsNS.pereshetW(tempW, tempT, nu, dlta);                        
                        nextw = converter(tempW);
                        if (k == epoch - 1) // Считаем ошибку только для последней эпохи
                            Ep[k].E = Math.Sqrt(dlta * dlta);
                    }	
                    k++;                     
                } while (k != epoch); // пока не пройдем эпохи
                return Ep[epoch - 1];               
        }
    }
}

