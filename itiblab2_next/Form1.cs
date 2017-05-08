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
            else return  Math.Sin(t);
        }
        public void DrawGraph(double a, double b, double[] y)
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
        private void Draw2Point(double a, double b, double[] y, double[] realy, double shag)
        {
            
            
            // Получим панель для рисования
            GraphPane pane = zedGraphControl1.GraphPane;

            pane.XAxis.Title.Text = "Координата X"; //подпись оси X
            pane.YAxis.Title.Text = "Координата Y"; //подпись оси Y
            pane.Title.Text = "(x^2)*sin(x)"; //подпись графика

            // Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
            pane.CurveList.Clear();

            // Создадим список точек
            PointPairList list1 = new PointPairList(); // Для y
            PointPairList list2 = new PointPairList(); // Для realy
            int i = 0;


           // double shag = (Math.Abs(a) + Math.Abs(b)) / N;

            for (double x = a; x <= (b - 0.0000000001); x += shag)// чертим график 0.99 чтобы exeption не было
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
        private void DrawPoint(double a, double b, double[] y)
        {
            // Получим панель для рисования
            GraphPane pane = zedGraphControl1.GraphPane;

            // Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
            pane.CurveList.Clear();

            // Создадим список точек
            PointPairList list = new PointPairList();
            int i = 0;
            // Интервал, в котором будут лежать точки
            //int xmin = -2;
            //int xmax = 2;

            //int ymin = -1; 
            //int ymax = 1;

            //int pointsCount = 50;

            //Random rnd = new Random();

            // Заполняем список точек
            
            for (double x = a; x <= (b-0.01); x += 0.1)// чертим график 0.99 чтобы exeption не было
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
            double shag = (Math.Abs(a) + Math.Abs(b)) / N;
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
                //finalresult = formula.UpdateSample(finalresult, result);
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
            zedGraphControl1.AxisChange();
            zedGraphControl1.Refresh();
        }

        

       
    }
}
