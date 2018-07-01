using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Интепретатор_0;

namespace Interp1_17._4_
{
    public partial class Form1 : Form
    {
        bool drawing = false;
        Graphics g;
        TParser tree;
        public Form1()
        {
            InitializeComponent();
        }

        public void MyDraw()
        {
            tree.Draw();
            g.DrawImage(tree.bitmap, ClientRectangle);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s = textBox1.Text;
            tree = new TParser(ClientRectangle.Width, ClientRectangle.Height);
            tree.Recyrse(s, out tree.top);
            tree.SetXY(tree.top, 200, 60);
            label1.Text = "= " + Convert.ToString(tree.top.Value);
            MyDraw();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            MyDraw();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            g = CreateGraphics();
            string s = textBox1.Text;
            tree = new TParser(ClientRectangle.Width, ClientRectangle.Height);
            tree.Recyrse(s, out tree.top);
            tree.SetXY(tree.top, 200, 60);
            label1.Text = "= " + Convert.ToString(tree.top.Value);
            MyDraw();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            tree.selectNode = tree.FindNode(tree.top, e.X, e.Y);
            drawing = tree.selectNode != null;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (drawing)
            {
                tree.Delta(tree.selectNode, tree.selectNode.x - e.X, tree.selectNode.y - e.Y);
                MyDraw();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            drawing = false;
        }
    }
}
