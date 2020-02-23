using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Triangulate
{
    public struct Points
    {
        public Points(double xCoord, double yCoord)
        {
            this.xCoord = xCoord;
            this.yCoord = yCoord;
        }

        public double xCoord { get; set; }
        public double yCoord { get; set; }
    }

    public partial class Main : Form
    {
        double leftMargin, rightMargin, topMargin, bottomMargin;
        int amountVertical, amountHorizontal;

        Graphics graph;

        List<Points> createdPoints;

        public Main()
        {
            InitializeComponent();
            graph = pcbDrawField.CreateGraphics();
            graph.Clear(Color.White);
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            Points center = new Points(pcbDrawField.Width / 2, pcbDrawField.Height / 2);
            //graph.TranslateTransform((float)center.xCoord, (float)center.yCoord);

            leftMargin = center.xCoord - 150;
            rightMargin = center.xCoord + 150;
            topMargin = center.yCoord - 150;
            bottomMargin = center.yCoord + 150;

            createdPoints = new List<Points>();

            /*
            double height = ;
            double width = 
            */

            graph.Clear(Color.White);

            Pen pen = new Pen(Color.Black);

            if(chkDebug.Checked)
            {
                graph.DrawLine(pen, (float)leftMargin, (float)topMargin, (float)rightMargin, (float)topMargin);
                graph.DrawLine(pen, (float)rightMargin, (float)topMargin, (float)rightMargin, (float)bottomMargin);
                graph.DrawLine(pen, (float)rightMargin, (float)bottomMargin, (float)leftMargin, (float)bottomMargin);
                graph.DrawLine(pen, (float)leftMargin, (float)bottomMargin, (float)leftMargin, (float)topMargin);
            }

            amountHorizontal = Convert.ToInt32(txtHorizontal.Text);
            amountVertical = Convert.ToInt32(txtVertical.Text);

            double stepVertical = Math.Abs(topMargin - bottomMargin) / (amountVertical - 1);
            double stepHorizontal = Math.Abs(rightMargin - leftMargin) / (amountHorizontal - 1);

            double xCurrent = leftMargin, yCurrent = topMargin;

            for (int heightCount = 0; heightCount < amountVertical; heightCount++)
            {
                for (int widthCount = 0; widthCount < amountHorizontal; widthCount++)
                {
                    createdPoints.Add(new Points(xCurrent, yCurrent));
                    xCurrent += stepHorizontal;
                }
                xCurrent = leftMargin;
                yCurrent += stepVertical;
            }

            foreach(Points point in createdPoints)
            {
                DrawPoint(point);
            }
        }

        private void DrawPoint(Points point)
        {
            SolidBrush brush = new SolidBrush(Color.Red);

            graph.FillEllipse(brush, (float)point.xCoord - 2, (float)point.yCoord - 2, 4, 4);
        }
    }
}
