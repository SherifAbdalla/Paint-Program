using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint_Program
{
    public partial class Form1 : Form
    {
        bool CanPaint = false;
        Graphics g;
        int? prevX = null;
        int? prevY = null;
        Color color = Color.Black;
        bool drawSquar = false;
        bool drawRectangle = false;
        bool drawCrcle = false;
        public Form1()
        {
            InitializeComponent();
            g = panel1.CreateGraphics();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            CanPaint = true;
            if(drawSquar)
            {
                SolidBrush sb = new SolidBrush(color);
                g.FillRectangle(sb, e.X, e.Y, int.Parse(toolStripTextBox2.Text), int.Parse(toolStripTextBox2.Text));
                CanPaint = false;
                drawSquar = false;
            }
            else if (drawRectangle)
            {
                SolidBrush sb = new SolidBrush(color);
                g.FillRectangle(sb, e.X, e.Y, int.Parse(toolStripTextBox2.Text) * 2, int.Parse(toolStripTextBox2.Text));
                CanPaint = false;
                drawRectangle = false;
            }
            else if (drawCrcle)
            {
                SolidBrush sb = new SolidBrush(color);
                g.FillEllipse(sb, e.X, e.Y, 50, 50);
                CanPaint = false;
                drawSquar = false;
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            CanPaint = false;
            prevX = null;
            prevY = null;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if(CanPaint)
            {
                Pen p = new Pen(color, float.Parse(toolStripTextBox1.Text));
                g.DrawLine(p, new Point(prevX ?? e.X, prevY ?? e.Y), new Point(e.X, e.Y));
                prevX = e.X;
                prevY = e.Y;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                toolStripButton1.BackColor = cd.Color;
                color = cd.Color;
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            g.Clear(panel1.BackColor);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                g.Clear(cd.Color);
            }
        }

        private void sqToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawSquar = true;
        }

        private void rectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawRectangle = true;
        }

        private void cToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawCrcle = true;
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            string[] imagepath =(string[])e.Data.GetData(DataFormats.FileDrop);
            foreach(string path in imagepath)
            {
                g.DrawImage(Image.FromFile(path), new Point(0, 0));
            }
        }
    }
}
