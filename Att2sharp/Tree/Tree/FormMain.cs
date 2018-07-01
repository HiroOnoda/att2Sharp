using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpaceTree
{
    public partial class FormMain : Form
    {
        bool drawing = false;
        Graphics g;
        MyTree myTree; 
        
        public FormMain()
        {
            InitializeComponent();
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            int L = textBox1.Lines.Count();
            for (int i = 0; i < L; i++)
            {
                if (textBox1.Lines[i] != "")
                {
                    int k = Convert.ToInt32(textBox1.Lines[i]);
                    myTree.Insert(ref myTree.top, k, 200, 40);
                }
            }
            MyDraw();
            textBox3.Text = myTree.SetStrSort(myTree.top);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            g = this.CreateGraphics();
            myTree = new MyTree();
            MyTree.bitmap = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
        }

        public void MyDraw()
        {
            if (MyTree.bitmap != null)
            {
                myTree.Draw();
                g.DrawImage(MyTree.bitmap, ClientRectangle);
            }
        }
        private void FormMain_Paint(object sender, PaintEventArgs e)
        {
            MyDraw();
        }

        private void FormMain_MouseDown(object sender, MouseEventArgs e)
        {
            myTree.DeSelect(myTree.top);
            myTree.selectNode = myTree.FindNode(myTree.top, e.X, e.Y);
            drawing = myTree.selectNode != null;
            if (drawing)
                myTree.selectNode.visit = true;
        }

        private void FormMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (drawing)
                myTree.Delta(myTree.selectNode, myTree.selectNode.x - e.X, myTree.selectNode.y - e.Y);
            else
            {
                myTree.DeSelect(myTree.top);
                myTree.selectNode = myTree.FindNode(myTree.top, e.X, e.Y);
                if (myTree.selectNode != null)
                    myTree.selectNode.visit = true;
            }
            MyDraw();
        }

        private void FormMain_MouseUp(object sender, MouseEventArgs e)
        {
            drawing = false;
        }

        private void buttonFind_Click(object sender, EventArgs e)
        {
            myTree.DeSelect(myTree.top);
            myTree.selectNode = myTree.FindNodeValStart(Convert.ToInt32(textBox2.Text));
            MyDraw();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            myTree.DeSelect(myTree.top);
            myTree.SearchStart(Convert.ToInt32(textBox2.Text));
            MyDraw();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            //if (myTree.SelectNode != null)
            {
                myTree.Delete(Convert.ToInt32(textBox2.Text), ref myTree.top);
                MyDraw();
            }
        }

        private void buttonRandom_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            textBox1.Text = "";
            for (int i = 0; i < 32; i++)
            {
                int k = rnd.Next(20);
                textBox1.Text += Convert.ToString(k)+Convert.ToChar(13)+Convert.ToChar(10);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
