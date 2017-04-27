using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace itiblab2_next
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            int N = 20;
            int p = 4; // Исходное количество нейронов
            int nu = 1; // Коэффициент обучения
            int a = -1; // Нижняя граница временного интервала
            int b = 1; // Верхняя граница временного интервала
            int[,] X = new int[p, N + N]; // N точек первого интервала и N точек следующего интервала
            double[] t = new double[N];
            double[] y = new double[N];
            t = formula.respred(a, b, N);
            for (int i = 0; i < t.Length; i++)
                y[i] = formula.tsin(t[i]);
            DrawGraph(a, b, y);
            
        }
        public void DrawGraph(int a, int b, double[] y)
        {
            int i = 0;
            Dictionary<double, double> coordinats = new Dictionary<double, double>();// coordinats-хранит координаты точек функции
            for (double x = -1; x <= 0.99; x += 0.1)// чертим график 0.99 чтобы exeption не было
            {
                coordinats.Add(x, y[i]);//расчитываем координаты
                i++;
            }
            GraphPane myPane = new GraphPane();
            zedGraphControl1.GraphPane = myPane;
            myPane.XAxis.Title.Text = "Координата X";//подпись оси X
            myPane.YAxis.Title.Text = "Координата Y";//подпись оси Y
            myPane.Title.Text = "gr";//подпись графика
            myPane.Fill = new Fill(Color.White);//фон графика заливаем градиентом
            myPane.Chart.Fill.Type = FillType.None;
            myPane.Legend.Position = LegendPos.Float;
            myPane.Legend.IsHStack = false;
            LineItem myCurve = myPane.AddCurve("Функция", coordinats.Keys.ToArray(), coordinats.Values.ToArray(), Color.Black, SymbolType.None);//строим график, цвет линии синий
            myCurve.Line.Width = 2.0F;
            myCurve.Symbol.Fill = new Fill(Color.White);
            zedGraphControl1.AxisChange();
            zedGraphControl1.Refresh();
            zedGraphControl1.Visible = true;
        }
    }
}
