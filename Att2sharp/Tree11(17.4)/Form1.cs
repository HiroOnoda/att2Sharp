using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpaceTree;
namespace Tree11_17._4_
{
    public partial class Form1 : Form
    {

        //bool drawing = false;
        Graphics g;
        MyTree myTree;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            g = this.CreateGraphics();
            myTree = new MyTree(2);
            MyTree.bitmap = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            myTree.Depth = 3;
            myTree.CreateTree(myTree);
            MyDraw();
            textBox1.Lines = myTree.Pos;
            textBox2.Lines = myTree.Neg;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            MyDraw();
        }

        public void MyDraw()
        {
            if (MyTree.bitmap != null)
            {
                myTree.Draw();
                g.DrawImage(MyTree.bitmap, ClientRectangle);
            }
        }
    }
}
