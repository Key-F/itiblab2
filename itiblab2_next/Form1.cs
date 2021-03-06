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
using org.mariuszgromada.math.mxparser;

namespace itiblab2_next
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.XAxis.Title.Text = "Координата X"; //подпись оси X
            pane.YAxis.Title.Text = "Координата Y"; //подпись оси Y
            pane.Title.Text = "График";//подпись графика
            zedGraphControl1.Refresh();
            ToolTip t = new ToolTip();
            t.SetToolTip(textBox6, "Опасно");
            t.SetToolTip(groupBox1, "Опасно");
        }
        public double tsin(double t)
        {         
            if (radioButton1.Checked == true)
            return t * t * Math.Sin(t);
            if (radioButton2.Checked == true)
                return  t * Math.Sin(t);
            if (radioButton4.Checked == true) // Не очень рационально, нужно вынести определение функции отдельно
            {
                string strokafunc = textBox7.Text;
                Argument x = new Argument("x");               
                Expression f = new Expression(strokafunc, x); 
                x.setArgumentValue(t);
                double m = f.calculate();
                return f.calculate();
            }
            else return Math.Sin(t);
        }
     
        private void Draw2Point(double a, double b, double[] y, double[] realy, double shag)
        {           
            // Получим панель для рисования
            GraphPane pane = zedGraphControl1.GraphPane;

            pane.XAxis.Title.Text = "Координата X"; //подпись оси X
            pane.YAxis.Title.Text = "Координата Y"; //подпись оси Y
            if (radioButton1.Checked == true) //подпись графика
                pane.Title.Text = "(x^2)*sin(x)"; 
            if (radioButton2.Checked == true)
                pane.Title.Text = "x*sin(x)"; 
            if (radioButton4.Checked == true)
            {
                pane.Title.Text = textBox7.Text;           
            }
            if (radioButton3.Checked == true)
                pane.Title.Text = "sin(x)"; 

            // Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
            pane.CurveList.Clear();

            // Создадим список точек
            PointPairList list1 = new PointPairList(); // Для y
            PointPairList list2 = new PointPairList(); // Для realy
            int i = 0;

            for (double x = a; x <= (b - 0.0000000001); x += shag) // чертим график 0.99 чтобы exeption не было
            {
                list1.Add(x, y[i]); //расчитываем координаты
                list2.Add(x, realy[i]);
                i++;
            }
            
            
            // Обводка ромбиков будут рисоваться голубым цветом (Color.Blue),
            // Опорные точки - ромбики (SymbolType.Diamond)
            LineItem myCurve = pane.AddCurve("Test", list1, Color.Blue, SymbolType.Diamond);
            LineItem myCurve_ = pane.AddCurve("Real", list2, Color.Red, SymbolType.Diamond);

            // !!!
            // У кривой линия будет невидимой
            // Form1 temp = new Form1();
            if ((checkBox2.CheckState == CheckState.Checked)&&(checkBox3.CheckState == CheckState.Checked))
            {
                myCurve.Line.IsVisible = true;  
                myCurve_.Line.IsVisible = true; 
            }

            if ((checkBox2.CheckState != CheckState.Checked) && (checkBox3.CheckState == CheckState.Checked))
            {
                myCurve.Line.IsVisible = false;
                myCurve_.Line.IsVisible = true;
            }
            if ((checkBox2.CheckState != CheckState.Checked) && (checkBox3.CheckState != CheckState.Checked))
            {
                myCurve.Line.IsVisible = false;
                myCurve_.Line.IsVisible = false;
            }
            if ((checkBox2.CheckState == CheckState.Checked) && (checkBox3.CheckState != CheckState.Checked))
            {
                myCurve.Line.IsVisible = true;
                myCurve_.Line.IsVisible = false;
            }
            // !!!
            // Цвет заполнения отметок (ромбиков) - голубой
            myCurve.Symbol.Fill.Color = Color.Blue;
            myCurve_.Symbol.Fill.Color = Color.Green;

            // !!!
            // Тип заполнения - сплошная заливка
            myCurve.Symbol.Fill.Type = FillType.Solid;
            myCurve_.Symbol.Fill.Type = FillType.Solid;

           
            myCurve.Symbol.Size = 4;

            myCurve_.Symbol.Size = 6;
            if (checkBox1.CheckState == CheckState.Checked)
            {
                // !!!
                // Включаем отображение сетки напротив крупных рисок по оси X
                pane.XAxis.MajorGrid.IsVisible = true;

                // Задаем вид пунктирной линии для крупных рисок по оси X:
                // Длина штрихов равна 10 пикселям, ... 
                pane.XAxis.MajorGrid.DashOn = 10;

                // затем 5 пикселей - пропуск
                pane.XAxis.MajorGrid.DashOff = 5;


                // Включаем отображение сетки напротив крупных рисок по оси Y
                pane.YAxis.MajorGrid.IsVisible = true;

                // Аналогично задаем вид пунктирной линии для крупных рисок по оси Y
                pane.YAxis.MajorGrid.DashOn = 10;
                pane.YAxis.MajorGrid.DashOff = 5;


                // Включаем отображение сетки напротив мелких рисок по оси X
                pane.YAxis.MinorGrid.IsVisible = true;

                // Задаем вид пунктирной линии для крупных рисок по оси Y: 
                // Длина штрихов равна одному пикселю, ... 
                pane.YAxis.MinorGrid.DashOn = 1;

                // затем 2 пикселя - пропуск
                pane.YAxis.MinorGrid.DashOff = 2;

                // Включаем отображение сетки напротив мелких рисок по оси Y
                pane.XAxis.MinorGrid.IsVisible = true;

                // Аналогично задаем вид пунктирной линии для крупных рисок по оси Y
                pane.XAxis.MinorGrid.DashOn = 1;
                pane.XAxis.MinorGrid.DashOff = 2;
            }
            else
            {
                pane.XAxis.MajorGrid.IsVisible = false;
                pane.XAxis.MinorGrid.IsVisible = false;
                pane.YAxis.MajorGrid.IsVisible = false;
                pane.YAxis.MinorGrid.IsVisible = false;
            }
            zedGraphControl1.AxisChange();

            
            zedGraphControl1.Invalidate();
           
        }
       
        private void button3_Click(object sender, EventArgs e)
        {
            int N = Convert.ToInt32(textBox6.Text); // Число точек по X
            int p = Convert.ToInt32(textBox3.Text); // Исходное количество нейронов
            if (p >= N)
            {
                MessageBox.Show("p должно быть меньше N", "Ошибка", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                return;
            }
            double nu = formula.ParseDouble1(textBox2.Text); // Коэффициент обучения
            double a = formula.ParseDouble1(textBox4.Text); // Нижняя граница временного интервала
            double b = formula.ParseDouble1(textBox5.Text); // Верхняя граница временного интервала
            double shag = Math.Abs(a - b) / N;
            double[] t = new double[N];
            double[] y = new double[N];
            int numepoh = Convert.ToInt32(textBox1.Text);
            t = formula.respred(a, b, N);
            for (int i = 0; i < t.Length; i++)
                y[i] = tsin(t[i]);
            epoha EP = formula.obuch(y, t, p, N, numepoh, nu);
            double result;
            double[] finalresult = new double [2 * N];
            for (int dd = 0; dd < y.Length; dd++)
                finalresult[dd] = y[dd];
            List<double> result_vector = new List<double>();
            for (int i = N - p; i < 2 * N - p; i++)
            {
                List<double> tempT = formula.getT(finalresult, i, i + p);
                result = paramsNS.net(EP.W, tempT, p);
                result_vector.Add(result);
                finalresult[i + p] = result;
            }
            
            double[] realy = new double[2 * N];
            int ii = 0;
            for (double i = a; i < 2 * b - a; i += shag)
            {
                realy[ii] = tsin(i);
                ii++;
            }
            Draw2Point(a, 2 * b - a , finalresult, realy, shag);
            label7.Text = "Среднеквадратическая ошибка: " + Convert.ToString(EP.E);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.CurveList.Clear();
            pane.XAxis.Scale.MinAuto = true;
            pane.XAxis.Scale.MaxAuto = true;
            pane.YAxis.Scale.MinAuto = true;
            pane.YAxis.Scale.MaxAuto = true;            
            label7.Text = "";
            pane.XAxis.MajorGrid.IsVisible = false;
            pane.XAxis.MinorGrid.IsVisible = false;
            pane.YAxis.MajorGrid.IsVisible = false;
            pane.YAxis.MinorGrid.IsVisible = false;
            pane.Title.Text = "График";
            zedGraphControl1.AxisChange();
            zedGraphControl1.Refresh();
        }       
    }
}
