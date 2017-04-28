using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itiblab2_next
{
    class formula
    {

        public static List<double[]> makeX(double[] t, int p) // Создаем матрицу 
        {
            int k = 0; // Смещение
            int k1 = 0;
            double[] temp1 = new double[p];
            List<double[]> X = new List<double[]>();
            do
            {
                k1 = k;
                for (int i = 0; i < p; i++)
                {
                    
                    temp1[i] = t[k1];
                    k1++;
                    double[] temp2 = new double[p];
                    
                    if (i == p - 1)
                    {
                        Array.Copy(temp1, temp2, i + 1);
                        X.Add(temp2);

                    }
                }
                k++;           
            }
            while (k1 != t.Length);
            return X;
        }
        public static double tsin(double t)
        {
            return t * t * Math.Sin(t);
        }
        public static List<double> converter(double[] x)
        {
            List<double> xc = new List<double>();
            for (int i = 0; i < x.Length; i++)
            {
                xc.Add(x[i]);
            }
            return xc;
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

        public static void obuch(double[] real, double[] t, int p, int n)  // real - значения, расчитанные по формуле, n = 20; t - распределение
        {
            
            int k = 0; // Счетчик эпох
            double prost = 1; // Штука для останоки
            List<epoha> Ep = new List<epoha>();
            //List<int[]> X = function.getx(4);
            //int[] F = function.getF(X);
            double[] dlta = new double[20];
            double[] net1 = new double[20];
            List<double[]> X = makeX(t, p);
            List<double> nextw = Enumerable.Repeat(0.0, p + 1).ToList(); // Значения для весов следующей эпохи, заполняем нулями, +1 для w0
            
                     
                do
                {
                    epoha Ep_ = new epoha(nextw);
                    Ep.Add(Ep_);
                    Ep[k].nomer = k;
                    int j = 0;
                    Ep[k].Y = new double[20];
                    double[] tempW = nextw.ToArray();  
                    for (int i = 0; i < p; i++)
                    {
                        net1[i] = paramsNS.net(tempW, X[i], p, n);
                        //double outz =  tsin(net1[i]);
                        Ep[k].Y[i] = net1[i];
                    }
                    Ep[k].E = paramsNS.error(n, Ep[k].Y, real);

                    if (Ep[k].E > 1)
                    {                        
                        for (int i = 0; i < X.Count; i++)
                        {
                            net1[i] = paramsNS.net(tempW, t, p, n);
                            
                            dlta[i] = paramsNS.delta(real[i], Ep[k].Y[i]);                            
                            double[] temp = paramsNS.pereshetW(tempW, X[i], net1[i], 0.3, dlta[i]);
                            nextw = converter(temp);                                           
                        }
                    }
                    prost = Ep[k].E;
                    
                    k++;                                      
                } while (prost < 0.4); //
                
            
        }
        }
    }

