using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SpaceTree;

namespace Tree
{
    public partial class TreeForm1 : Form
    {

        bool drawing = false;
        Graphics g;
        MyTree myTree = new MyTree();

        public TreeForm1()
        {
            InitializeComponent();
        }

        public void MyDraw()
        {
            if (MyTree.bitmap != null)
            {
                myTree.Draw();
                g.DrawImage(MyTree.bitmap, ClientRectangle);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //int L = Int32.Parse(textBox1.Text);

            myTree.Depth = 2;
            myTree.CreateTree();
            MyDraw();
        }

        private void TreeForm1_Load(object sender, EventArgs e)
        {
            g = this.CreateGraphics();
            myTree = new MyTree();
            MyTree.bitmap = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
        }

        private void TreeForm1_Paint(object sender, PaintEventArgs e)
        {
            MyDraw();
        }
    }
}
