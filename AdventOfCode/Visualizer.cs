using AdventOfCode.Days.Nineteen;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AdventOfCode
{
    internal class Visualizer : Form
    {
        private Panel panel1;
        private List<Wire> wires;
        private TrackBar trackBar1;
        private float scale = 1.5f;

        public Visualizer(List<Wire> wire)
        {
            this.wires = wire;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1280, 720);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(12, 748);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(268, 45);
            this.trackBar1.TabIndex = 1;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            //  
            // Visualizer
            // 
            this.ClientSize = new System.Drawing.Size(1306, 823);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.panel1);
            this.Name = "Visualizer";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            var p = sender as Panel;
            var g = e.Graphics;

            Random rand = new Random();
            foreach (var wire in wires)
            {
                Pen pen = new Pen(Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255)))
                {
                    Width = 5f * scale
                };

                List<PointF> points = new List<PointF>();
                foreach (var point in wire.AllPointsVisited)
                {
                    PointF pnt = new PointF(point.X * scale, point.Y * scale * 2);
                    pnt.Y -= 400;
                    pnt.Y *= -1;
                    pnt.X += 200;
                    points.Add(pnt);
                }
                g.DrawLines(pen, points.ToArray());
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            var x = sender as TrackBar;
            this.scale = ((float)x.Value + 1f) / 100;
            panel1.Update();
            this.Update();
            this.Refresh();
        }
    }
}