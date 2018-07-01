using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Интепретатор_0;

namespace Tree_Parser
{
    public partial class FormMain : Form
    {
        bool drawing = false;
        Graphics g;
        TParser tree;
        public FormMain()
        {
            InitializeComponent();
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            string s = textBox1.Text;
            tree = new TParser(ClientRectangle.Width, ClientRectangle.Height);
            tree.Expression(ref s, out tree.top);
            if (tree.error == 0)
            {
                tree.SetXY(tree.top, 200, 60);
                label1.Text = "= " + Convert.ToString(tree.top.Value);
                MyDraw();
            }
            else
                MessageBox.Show("Error = " + tree.error.ToString()+" "+tree.errorToString());
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            g = CreateGraphics();
            string s = textBox1.Text;
            tree = new TParser(ClientRectangle.Width, ClientRectangle.Height);
            tree.Expression(ref s, out tree.top);
            if (tree.error == 0)
            {
                tree.SetXY(tree.top, 200, 60);
                label1.Text = "= " + Convert.ToString(tree.top.Value);
                MyDraw();
            }
            else
                MessageBox.Show("Error = " + tree.error.ToString());
        }

        public void MyDraw()
        {
            tree.Draw();
            g.DrawImage(tree.bitmap, ClientRectangle);
        }

        private void FormMain_Paint(object sender, PaintEventArgs e)
        {
            MyDraw();
        }

        private void FormMain_MouseDown(object sender, MouseEventArgs e)
        {
            tree.selectNode = tree.FindNode(tree.top, e.X, e.Y);
            drawing = tree.selectNode != null;
        }

        private void FormMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (drawing)
            {
                tree.Delta(tree.selectNode, tree.selectNode.x - e.X, tree.selectNode.y - e.Y);
                MyDraw();
            }
        }

        private void FormMain_MouseUp(object sender, MouseEventArgs e)
        {
            drawing = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
