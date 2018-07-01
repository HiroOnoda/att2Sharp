using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Drawing.Drawing2D;
using System.Collections;



namespace Graph_19._4_
{
    public struct Edge
    {
        public int A;            // вес дуги
        public int numNode;      // № узла
        public int x1c, x2c, yc; // геометрия дуги
        public Color color;      // цвет
        public bool select;
    }

    public class Node
    {
        public string name;  // 4+4*Name.Length
        public Edge[] Edge; // 4+L2*(5*4) - ребра
        //public bool visit;
        public int x, y;     // 4+4
        public int numVisit; // № визита
        public Color color;  // цвет
        //public int dist, oldDist; // для алгоритма Дейкстры
    }

    public partial class Graph
    {
        const int hx = 50, hy = 10;
        public Bitmap bitmap;
        public static Node[] Nodes = new Node[0];       // узлы
        public static Node SelectNode;                  // выделенный
        public static Node SelectNodeBeg;
        public string[] Cycles = new string[0];
        public bool visibleA = false;
        public int x1;
        public int x2;
        public int y1;
        public int y2;
        public int[,] A;
        public Graphics g;
        Font MyFont;
        /*Color[] Colors = new Color[7]
        { Color.White, Color.Yellow, Color.Lime, Color.Gray, Color.Red, Color.Azure, Color.SandyBrown };*/

        public void Clear() // очистить граф 
        {
            int N = Nodes.Length;
            for (int i = 0; i < N; i++)
                Array.Resize<Edge>(ref Nodes[i].Edge, 0);
            Array.Resize<Node>(ref Nodes, 0);
        }

        public void AddNode(/*int x, int y*/) // добавить узел 
        {
            int N = Nodes.Length;
            Array.Resize<Node>(ref Nodes, ++N);
            Nodes[N - 1] = new Node();
            Nodes[N - 1].name = "Node " + Convert.ToString(N - 1);
            //Nodes[N - 1].x = x;
            //Nodes[N - 1].y = y;
            Nodes[N - 1].color = Color.White;
        }

        public void AddEdge(int NodeNumber,int num)  // добавить ребро
        {
            //int n = -1; bool ok = false;
            int Ln = Nodes.Length;
            /*while ((n < Ln - 1) && !ok)
                ok = (Nodes[++n] == SelectNode);*/

            int L = 0;
            if (Nodes[NodeNumber - 1].Edge != null)
                L = Nodes[NodeNumber - 1].Edge.Length;
            Array.Resize<Edge>(ref Nodes[NodeNumber-1].Edge, ++L);
            Nodes[NodeNumber - 1].Edge[L - 1].numNode = num;
            /*double a1 = SelectNodeBeg.x;
            double b1 = SelectNodeBeg.y;
            double a2 = SelectNode.x;
            double b2 = SelectNode.y;*/

            /*SelectNodeBeg.Edge[L - 1].A = (int)Math.Sqrt((a2 - a1) * (a2 - a1) + (b2 - b1) * (b2 - b1));
            SelectNodeBeg.Edge[L - 1].x1c = x1 - SelectNodeBeg.x;
            SelectNodeBeg.Edge[L - 1].x2c = x2 - SelectNode.x;
            SelectNodeBeg.Edge[L - 1].yc = (SelectNode.y + SelectNodeBeg.y) / 2;
            SelectNodeBeg.Edge[L - 1].color = Color.Silver;*/
        }

        public void Recyrse(int start,int current,string route)
        {
            //()
            string s = "";
            string s1 = "";
            foreach (Edge e in Nodes[current-1].Edge)
            {
                s = Convert.ToString(current) + "-" + Convert.ToString(e.numNode);
                s1 = Convert.ToString(e.numNode) + "-" + Convert.ToString(current);
                if (!(route.Contains(s) || route.Contains(s1)))
                {
                   Recyrse(start,e.numNode, route +  Convert.ToString(current) + ";"+s + ";");
                }
            }
            //if(start == current)
            //Array.Resize<String>(ref Cycles, Cycles.Length + 1);
            //Cycles[Cycles.Length - 1] = route;
        }       
    }

    
    class GraphLogic
    {
    }
}
