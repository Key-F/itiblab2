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
        private void Draw2Point(int a, int b, double[] y, double[] realy)
        {
            // Получим панель для рисования
            GraphPane pane = zedGraphControl1.GraphPane;

            // Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
            pane.CurveList.Clear();

            // Создадим список точек
            PointPairList list1 = new PointPairList(); // Для y
            PointPairList list2 = new PointPairList(); // Для realy
            int i = 0;
            

            // Заполняем список точек

            for (double x = -1; x <= (b - 0.01); x += 0.1)// чертим график 0.99 чтобы exeption не было
            {
                list1.Add(x, y[i]);//расчитываем координаты
                list2.Add(x, realy[i]);
                i++;
            }
            

            // !!!
            // Создадим кривую с названием "Scatter".
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

           
            myCurve.Symbol.Size = 7;

            zedGraphControl1.AxisChange();

            
            zedGraphControl1.Invalidate();
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
            int numepoh = Convert.ToInt32(textBox1.Text);
            t = formula.respred(a, b, N);
            for (int i = 0; i < t.Length; i++)
                y[i] = formula.tsin(t[i]);
            epoha EP = formula.obuch(y, t, p, N, numepoh);
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
                //finalresult = formula.UpdateSample(finalresult, result);
            }
            //DrawPoint(a, 2*b - a, finalresult);
            double[] newt = formula.respred(a, 2*b - a, 2 * N);
            double[] realy = new double[2 * N];
            for (int i = 0; i < newt.Length; i++)
                realy[i] = formula.tsin(newt[i]);
            Draw2Point(a, 2 * b - a, finalresult, realy);
        }

       
    }
}
