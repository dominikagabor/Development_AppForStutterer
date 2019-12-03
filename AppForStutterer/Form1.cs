using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Reflection;


namespace AppForStutterer
{
    public partial class Form1 : Form
    {
        Graphics graphics;
        Point point = Point.Empty;
        Pen pen;
        Point[] points = new Point[10000];
        DateTime actualDetection = DateTime.Now;

        public static readonly Stopwatch watch = new Stopwatch();
        int r, g, b;
        static int numberOfAddedDrawings;
        static int counter = 0;
        int counterPoints = 0;

        public Form1()
        {
            InitializeComponent();

            pen = new Pen(Color.Black, 12);
            graphics = drawingPanel.CreateGraphics();
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;        
            pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            AddDrawing(3);
            UptadeTime();
        }

        public void UptadeTime()
        {
            duration.Text = GetTimeString(watch.Elapsed);
        }

        private string GetTimeString(TimeSpan elapsed)
        {
            string result = string.Empty;
            result = string.Format("{0}:{1}", elapsed.Seconds, elapsed.Milliseconds);
            return result;
        }

        private void AddDrawing(int theNumberOfDrawings)
        {
            for (int a = 1; a <= theNumberOfDrawings; a++)
            {
                chooseImage.Items.Add("Rysunek " + NewPanel.counterImage.ToString());
                NewPanel.counterImage++;
            }
            numberOfAddedDrawings = theNumberOfDrawings;
        }


        

private void chooseImage_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int a = 0; a < numberOfAddedDrawings; a++)
            {
                if (chooseImage.SelectedIndex == a)
                {
                    string Current_logged_serName = Environment.UserName;
                    ChangePanelBackgroundImage(System.IO.Path.Combine("Content\\", "C:/Users/"+ Current_logged_serName + "/Documents/Visual Studio 2015/Projects/AppForStutterer/AppForStutterer/Image/o" + a.ToString() + ".png"));
                }
            }

        }

        private Image ChangePanelBackgroundImage(string url)
        {
            Image NewPanelBackgroundImage = null;
            try
            {
                NewPanelBackgroundImage = new Bitmap(url);
                drawingPanel.BackgroundImage = NewPanelBackgroundImage;

            }

            catch
            {
                MessageBox.Show("Nie ma takiego obrazu");
            }

            return NewPanelBackgroundImage;
        }



        private void timer1_Tick_1(object sender, EventArgs e)
        {
            UptadeTime();
        }
 
        private void EventMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                point = e.Location;
            }
        }


        public void Draw(int numberBoard, int valueX, int valueY, int lessX, int greaterX, int lessY, int greaterY, MouseEventArgs e, int rc, int gc, int bc, int rf, int gf, int bf, int lessXe, int greaterXe, int lessYe, int greaterYe)
        {
           
            if (chooseImage.SelectedIndex == numberBoard)
            {
              
                    if ((valueX > lessX) && (valueX < greaterX) && (valueY > lessY) && (valueY < greaterY))
                {
                    label5.Text = CanDraw(rc, gc, bc, e).ToString();                 
                    watch.Start();
                    timer1.Enabled = true;
                  
                    if ((e.X > lessXe) && (e.X < greaterXe) && (e.Y > lessYe) && (e.Y < greaterYe))                 
                    {
                        timer1.Enabled = false;
                        MessageBox.Show("Gratulacje!");
                        listBox1.Items.Clear();
                        listBox2.Items.Clear();
                    }                  
                }


                else
                {
                    MessageBox.Show("Musisz zacząc w polu startowym");
                    listBox1.Items.Clear();
                    listBox2.Items.Clear();
                }

            }
        }
     

        private void EventMouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                Bitmap bmp = new Bitmap(drawingPanel.BackgroundImage);
                Color pointColor = bmp.GetPixel(e.X, e.Y);
                r = pointColor.R;
                g = pointColor.G;
                b = pointColor.B;


                if (e.Button == MouseButtons.Left)
                {

                    points[counterPoints++] = new Point(e.X, e.Y);
                    Color pointsColor = bmp.GetPixel(e.X, e.Y);
                    r = pointsColor.R;
                    g = pointsColor.G;
                    b = pointsColor.B;
                    listBox1.Items.Add(e.X);
                    listBox2.Items.Add(e.Y);
                    listBox3.Items.Add(r);
                    listBox4.Items.Add(g);
                    listBox5.Items.Add(b);


                    string X1 = listBox1.Items[0].ToString();
                    int valueX = int.Parse(X1);

                    string Y1 = listBox2.Items[0].ToString();
                    int valueY = int.Parse(Y1);
  
                    Draw(0, valueX, valueY, 50, 90, 20, 70, e, 34, 177, 76, 0, 0, 0, 470, 510, 320, 360);               
                    Draw(1, valueX, valueY, 35, 85, 30, 70, e, 63, 72, 204, 0, 0, 0, 55, 95, 330, 370);
                    Draw(2, valueX, valueY, 34, 72, 18, 45, e, 163, 73, 164, 0, 0, 0, 396, 430, 245, 280);

                    

                }
            }

            catch (Exception ex)
            {
                
            }
        }

        private int CanDraw(int r, int g, int b, MouseEventArgs e)
        {
            try
            {
                Bitmap bmp = new Bitmap(drawingPanel.BackgroundImage);
                Color pointColor = bmp.GetPixel(e.X, e.Y);
                int red = pointColor.R;
                int green = pointColor.G;
                int blue = pointColor.B;

                if ((red == r) && (green == g) && (blue == b))
                {
                    graphics.DrawLine(pen, point, e.Location);
                    point = e.Location;
                }

                else
                {
                    MessageBox.Show("Rysuj tylko po wskazanym polu!");
                    counter++;
                }
            }

            catch
            {
                
            }

            return counter;
        }

        private void Choose_Click(object sender, MouseEventArgs e)
        {
            counter = 0;
            watch.Reset();
        }

        private void ButtonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
