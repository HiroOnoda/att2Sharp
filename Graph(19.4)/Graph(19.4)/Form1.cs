using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Graph_19._4_;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Graph_19._4_
{
    public partial class Form1 : Form
    {
        Bitmap bitmap;
        public Form1()
        {
            InitializeComponent();
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
        }
        public string minroute = "";
        public int minlength = 0;
        public int NumberOfNodes = 0;

        Graph G = new Graph();
        Font myFont;
        void ReadNode(int position)
        {
            NumberOfNodes++;
            int NodeNumber = Int32.Parse(textBox1.Lines[position + 4].Substring(10, textBox1.Lines[position + 4].Length - 11));
            G.AddNode();
            Graph.Nodes[NumberOfNodes - 1].name = textBox1.Lines[position + 3].Substring(7, textBox1.Lines[position + 3].Length - 8);
            Graph.Nodes[NumberOfNodes - 1].x = Int32.Parse(textBox1.Lines[position + 1].Substring(4, textBox1.Lines[position + 1].Length - 5));
            Graph.Nodes[NumberOfNodes - 1].y = Int32.Parse(textBox1.Lines[position + 2].Substring(4, textBox1.Lines[position + 2].Length - 5));
            textBox1.AppendText(Environment.NewLine + Graph.Nodes[NumberOfNodes - 1].name);
            textBox1.AppendText(Environment.NewLine + "Номер узла:" + NumberOfNodes);
            textBox1.AppendText(Environment.NewLine + "x:" + Graph.Nodes[NumberOfNodes - 1].x);
            textBox1.AppendText(Environment.NewLine + "y:" + Graph.Nodes[NumberOfNodes - 1].y);
            //textBox1.AppendText(Environment.NewLine + NodeName);
            //textBox1.AppendText(Environment.NewLine + NodeNumber);
        }
        void ReadEdge(int position,int Node)
        {
            int EdgeNumber = Int32.Parse(textBox1.Lines[position + 1].Substring(10, textBox1.Lines[position + 1].Length - 11));
            textBox1.AppendText(Environment.NewLine + EdgeNumber);
            G.AddEdge(NumberOfNodes, EdgeNumber);
            /*Graph.Nodes[NumberOfNodes - 1].Edge[Graph.Nodes[NumberOfNodes - 1].Edge.Length-1].A = Int32.Parse(textBox1.Lines[position + 1].Substring(4, textBox1.Lines[position + 1].Length - 6));
            Graph.Nodes[NumberOfNodes - 1].Edge[Graph.Nodes[NumberOfNodes - 1].Edge.Length - 1].x1c = Int32.Parse(textBox1.Lines[position + 2].Substring(5, textBox1.Lines[position + 2].Length - 6));
            Graph.Nodes[NumberOfNodes - 1].Edge[Graph.Nodes[NumberOfNodes - 1].Edge.Length - 1].x2c = Int32.Parse(textBox1.Lines[position + 3].Substring(5, textBox1.Lines[position + 3].Length - 6));
            Graph.Nodes[NumberOfNodes - 1].Edge[Graph.Nodes[NumberOfNodes - 1].Edge.Length - 1].yc = Int32.Parse(textBox1.Lines[position + 4].Substring(5, textBox1.Lines[position + 4].Length -6));
            int NodeNumber = Int32.Parse(textBox1.Lines[position + 4].Substring(10, textBox1.Lines[position + 4].Length - 11));
            //textBox1.AppendText(Environment.NewLine + "tset:" + textBox1.Lines[position + 4].Substring(5,textBox1.Lines[position + 4].Length - 6));
            textBox1.AppendText(Environment.NewLine + "A: " + Graph.Nodes[NumberOfNodes - 1].Edge[Graph.Nodes[NumberOfNodes - 1].Edge.Length - 1].A);
            textBox1.AppendText(Environment.NewLine + "x1c: " + Graph.Nodes[NumberOfNodes - 1].Edge[Graph.Nodes[NumberOfNodes - 1].Edge.Length - 1].x1c);
            textBox1.AppendText(Environment.NewLine + "x2c: " + Graph.Nodes[NumberOfNodes - 1].Edge[Graph.Nodes[NumberOfNodes - 1].Edge.Length - 1].x2c);
            textBox1.AppendText(Environment.NewLine + "yc: " + Graph.Nodes[NumberOfNodes - 1].Edge[Graph.Nodes[NumberOfNodes - 1].Edge.Length - 1].yc);*/
        }

        void ScanInput()
        {
            NumberOfNodes = 0;
            for (int i = 0;i < textBox1.Lines.Length; i++)
            {
                if(textBox1.Lines[i] == " node:")
                {
                    ReadNode(i);
                    i = i + 1;
                }
                if (textBox1.Lines[i] == "edge:")
                {
                    ReadEdge(i, NumberOfNodes);
                    i = i + 1;
                }
            }
            //textBox1.AppendText(Environment.NewLine + );
        }

        public void Recyrse(int start, int current, string route)
        {
            //()
            string s = "";
            string s1 = "";          
            foreach (Edge e in Graph.Nodes[current - 1].Edge)
            {
                s = Convert.ToString(current) + "-" + Convert.ToString(e.numNode);
                s1 = Convert.ToString(e.numNode) + "-" + Convert.ToString(current);
                if (!(route.Contains(s) || route.Contains(s1)))
                { 
                    Recyrse(start, e.numNode, route + Convert.ToString(current) + ";" + s + ";");
                }
            }
            if ((start == current) && (route != ""))
            {
                if (minroute == "") { minroute = route; }
                if (route.Length < minroute.Length)
                {
                    minroute = route;
                    length(route);
                }
                textBox1.AppendText(Environment.NewLine + route);
            }
            //if(start == current)
            //Array.Resize<String>(ref Cycles, Cycles.Length + 1);
            //Cycles[Cycles.Length - 1] = route;
        }

        public void DrawGraph()
        {
            Graphics g = Graphics.FromImage(bitmap);
            //g.DrawEllipse(Pens.Black, 100, 100, 1, 1);
            for (int i = 0;i< Graph.Nodes.Length;i++)
            {                
                for (int j = 0; j < Graph.Nodes[i].Edge.Length; j++)
                {
                    g.DrawLine(Pens.Black, Graph.Nodes[i].x+10, Graph.Nodes[i].y+10, Graph.Nodes[Graph.Nodes[i].Edge[j].numNode-1].x+10, Graph.Nodes[Graph.Nodes[i].Edge[j].numNode-1].y+10);
                }              
            }
            for (int i = 0; i < Graph.Nodes.Length; i++)
            {
                myFont = new Font("Courier New", 10, FontStyle.Bold);
                g.DrawEllipse(Pens.Black, Graph.Nodes[i].x, Graph.Nodes[i].y, 20, 20);
                g.FillEllipse((SolidBrush)Brushes.White, Graph.Nodes[i].x, Graph.Nodes[i].y, 20, 20);
                g.DrawString(Convert.ToString(i+1), myFont, Brushes.Black, new PointF(Graph.Nodes[i].x+4, Graph.Nodes[i].y+4));
            }
        }

        public void length(string str)
        {
            int s = 0;
            for(int i = 0;i < str.Length; i++)
            {
                if (str.Substring(i, 1) == "-")
                {
                    s++;
                }
            }
            if ((minlength == 0) || (minlength > s))
            {
                minlength = s;
            }          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //g.DrawEllipse(MyPen, 250, 100, 200, 200);
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "Выберите файл";
            openFileDialog1.ShowDialog();
            textBox1.Text = File.ReadAllText(openFileDialog1.FileName, Encoding.Default);

            ScanInput();
            //for(int i = 0; i < NumberOfNodes; i++)
            //{ 
            //int i = 1;
            textBox1.AppendText(Environment.NewLine + "Все циклы: ");
            for(int i = 1;i < NumberOfNodes+1;i++)
            {
                Recyrse(i, i, "");
            }


            label2.Text = minroute;
            label3.Text = Convert.ToString(minlength);
            /*pictureBox1.BackColor = Color.White;
            pictureBox1.Visible = true;*/
            //DrawGraph();
            //pictureBox1.Image = bitmap;
            textBox1.AppendText(Environment.NewLine + "Минимальный цикл: " + minroute);
            /*foreach(string s in G.Cycles)
            {
                textBox1.AppendText(Environment.NewLine + s);
            }*/
            //textBox1.AppendText(Environment.NewLine + G.Recyrse(1, i, ""));
            //G.Recyrse(1, i, "");
            //}
            //G.Re
            /*openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = File.ReadAllText(openFileDialog1.FileName, Encoding.Default);
            }*/
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
