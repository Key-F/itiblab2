﻿using System;
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
            myPane.Title.Text = "(t^2)*sin(t)";//подпись графика
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
        private void DrawPoint(int a, int b, double[] y)
        {
            // Получим панель для рисования
            GraphPane pane = zedGraphControl1.GraphPane;

            // Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
            pane.CurveList.Clear();

            // Создадим список точек
            PointPairList list = new PointPairList();
            int i = 0;
            // Интервал, в котором будут лежать точки
            int xmin = -2;
            int xmax = 2;

            int ymin = -1; 
            int ymax = 1;

            //int pointsCount = 50;

            //Random rnd = new Random();

            // Заполняем список точек
            
            for (double x = -1; x <= (b-0.01); x += 0.1)// чертим график 0.99 чтобы exeption не было
            {
                list.Add(x, y[i]);//расчитываем координаты
                i++;
            }

            // !!!
            // Создадим кривую с названием "Scatter".
            // Обводка ромбиков будут рисоваться голубым цветом (Color.Blue),
            // Опорные точки - ромбики (SymbolType.Diamond)
            LineItem myCurve = pane.AddCurve("Scatter", list, Color.Blue, SymbolType.Diamond);

            // !!!
            // У кривой линия будет невидимой
           // Form1 temp = new Form1();
            if (checkBox1.CheckState == CheckState.Checked)
            myCurve.Line.IsVisible = true;
            else
                myCurve.Line.IsVisible = false;

            // !!!
            // Цвет заполнения отметок (ромбиков) - голубой
            myCurve.Symbol.Fill.Color = Color.Blue;

            // !!!
            // Тип заполнения - сплошная заливка
            myCurve.Symbol.Fill.Type = FillType.Solid;

            // !!!
            // Размер ромбиков
            myCurve.Symbol.Size = 7;


            // Устанавливаем интересующий нас интервал по оси X
          //  pane.XAxis.Scale.Min = xmin;
           // pane.XAxis.Scale.Max = xmax;

            // Устанавливаем интересующий нас интервал по оси Y
            //pane.YAxis.Scale.Min = ymin;
            //pane.YAxis.Scale.Max = ymax;

            // Вызываем метод AxisChange (), чтобы обновить данные об осях. 
            // В противном случае на рисунке будет показана только часть графика, 
            // которая умещается в интервалы по осям, установленные по умолчанию
            zedGraphControl1.AxisChange();

            // Обновляем график
            zedGraphControl1.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
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
            DrawPoint(a, b, y);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int N = 20;
            int p = 6; // Исходное количество нейронов
            int nu = 1; // Коэффициент обучения
            int a = -1; // Нижняя граница временного интервала
            int b = 1; // Верхняя граница временного интервала
            double[] t = new double[N];
            double[] y = new double[N];
            t = formula.respred(a, b, N);
            for (int i = 0; i < t.Length; i++)
                y[i] = formula.tsin(t[i]);
            formula.obuch(y, t, p, N);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            double[] w = new double[15];
            double[] x = new double[40];
            double[] y = new double[20];
            double[] d = new double[20];
            int p = 6;
            double n = 0.3;
            int a = -1;
            int b = 1;
            double er = 0.0;
            double net = 0.0;
            double step = (b - a) / 20;
            for (int p1 = 0; p1 < 15; p1++)
            {
                int k = 0;
                int era = 25;
                
            }
        }
    }
}
