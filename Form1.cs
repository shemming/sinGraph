using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Input;

namespace Lab11_NET
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private const int LINES = 80;

        private double[] x = new double[LINES];
        private double[] y = new double[LINES];

        void graphPoints(double[] x, double[] y, Graphics g)
        {
            
            // Clear the form space.
            g.Clear(this.BackColor);

            // Determine the size of the area in which we will draw.
            // Size size = this.ClientSize;
            Size size = pictureBox1.ClientSize;
            
            size.Height -= 5;

            // Get the max and min x and y values.
            double maxX = maxValue(x);
            double maxY = maxValue(y);
            double minX = minValue(x);
            double minY = minValue(y);
            double[] px = new double[x.Length];
            double[] py = new double[y.Length];
            Point[] points = new Point[x.Length];

            // Scale the points to pixels.
            for (int ip = 0; ip < x.Length; ip++)
            {
                px[ip] = (x[ip] - minX) / (maxX - minX) * size.Width;
                py[ip] = size.Height - (y[ip] - minY) / (maxY - minY) * size.Height;
                points[ip].X = (int) px[ip];
                points[ip].Y = (int) py[ip];
            }

            Pen bluePen = new Pen(Color.Blue);
            g.DrawCurve(bluePen, points);
        }

        double maxValue(double[] x)
        {
            double max = x[0];
            foreach (double a in x)
            {
                if (max < a) max = a;
            }
            return max;
        }

        double minValue(double[] x)
        {
            double min = x[0];
            foreach (double a in x)
            {
                if (min > a) min = a;
            }
            return min;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

            if (startTextBox.Text.Length == 0 || endTextBox.Text.Length == 0)
            {
                return;
            }
            
            // Get the current graphics object.
            Graphics g = e.Graphics;

            g.Clear(this.BackColor);

            Size size = pictureBox1.ClientSize;

            graphPoints(x, y, g);

        }

        private void pictureBox1_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void endTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (startTextBox.Text.Length == 0 || endTextBox.Text.Length == 0)
                {
                    return;
                }

                double start, end;
                if (!double.TryParse(startTextBox.Text.ToString(), out start))
                {
                    start = 0;
                }

                if (!double.TryParse(endTextBox.Text.ToString(), out end))
                {
                    end = start + 5;
                }

                if (end < start)
                {
                    return;
                }

                double tmp = start;
                double span = end - start;
                double increment = span / LINES;

                for (int i = 0; i < LINES; i++)
                {
                    x[i] = tmp;
                    y[i] = Math.Sin(tmp);
                    tmp += increment;
                }

                pictureBox1.Invalidate();

            }
        }
    }
}
