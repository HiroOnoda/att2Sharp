using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SpaceTree
{
    public class Node                 // узел
    {
        public int weight = 0;
        public Node A;
        public Node B;
        public int x;
        public int y;
        public bool visit;
        public int routeweight = 0;
        public bool positive = true;
        public bool negative = true;
        public int depth = 0;
        public string route;

        public Node(Node A, Node B, int weight, int x, int y) // конструктор
        {
            this.A = A;
            this.B = B;
            this.weight = weight;
            this.x = x;
            this.y = y;
            this.visit = false;
        }

        public void DrawNode(Graphics g)                          // рисование дерева
        {
            Pen myPen = Pens.Black;
            SolidBrush myBrush = (SolidBrush)Brushes.White;
            Font myFont = new Font("Courier New", 10, FontStyle.Bold);

            int R = 17;
            if (A != null)
                g.DrawLine(myPen, x, y, A.x, A.y);
            if (B != null)
                g.DrawLine(myPen, x, y, B.x, B.y);

            if (visit)
                myBrush = (SolidBrush)Brushes.Yellow;
            else
                myBrush = (SolidBrush)Brushes.LightYellow;

            g.FillEllipse(myBrush, x - R, y - R, 2 * R, 2 * R);
            g.DrawEllipse(myPen, x - R, y - R, 2 * R, 2 * R);
            string s = Convert.ToString(weight);
            SizeF size = g.MeasureString(s, myFont);
            g.DrawString(s, myFont, Brushes.Black,
                x - size.Width / 2,
                y - size.Height / 2);

            if (A != null)
                A.DrawNode(g);
            if (B != null)
                B.DrawNode(g);
        }
    }

    class MyTree
    {
        //string[] Positive = ;
        //string[] Negative;
        public int Depth = 1;
        public Node top = new Node(null, null, 0, 200, 60);             // вершина дерева
        public Node selectNode;      // выделенный узел
        Node q;                      // вспомогательный

        public static Bitmap bitmap; // канва для рисования
        const int step = 50;
        const int dh = 1;

        Graphics g;
        Pen myPen = Pens.Black;
        SolidBrush myBrush = (SolidBrush)Brushes.White;
        Font myFont = new Font("Courier New", 10, FontStyle.Bold);

        public MyTree(int Depth)              // конструктор
        {
            this.Depth = Depth;
        }

        public void Insert(ref Node node, int data, int x, int y)  // вставка
        {
            if (node == null)
            {
                node = new Node(null, null, data, x, y);
            }
            else
            {
                if (data < Convert.ToInt32(node.weight))
                {
                    Insert(ref node.A, data, node.x - step, node.y + dh * step);
                }               
                else
                {
                    if (data > Convert.ToInt32(node.weight))
                    {
                        Insert(ref node.B, data, node.x + step, node.y + dh * step);
                    }
                    else
                    {
                        if (data == Convert.ToInt32(node.weight))
                        {
                        }
                    }
                }
                    
            }
                
        }

        public void Delete(int data, ref Node node)    // Удаление
        {
            if (node != null)
                if (data < Convert.ToInt32(node.weight))
                    Delete(data, ref node.A);
                else
                    if (data > Convert.ToInt32(node.weight))
                        Delete(data, ref node.B);
                    else  // нашли
                    {     // data == node.data
                        q = node;
                        if (q.B == null)
                            node = q.A;
                        else
                            if (q.A == null)
                                node = q.B;
                            else
                                Del(ref q.A);
                    } //Dispose(q);
        }

        void Del(ref Node node)
        {
            if (node.B != null)
                Del(ref node.B);
            else
            {
                q.weight = node.weight;
                q = node;
                node = node.A;
            }
        }

        void Search(ref Node node, int val, int x, int y) // поиск и вставка
        {
            if (node == null)
                node = new Node(null, null, val, x, y);
            else
            {
                node.visit = true;
                if (val < Convert.ToInt32(node.weight))
                {
                    Search(ref node.A, val, node.x - step, node.y + dh * step);

                }
                else
                {
                    if (val < Convert.ToInt32(node.weight))
                    {
                        Search(ref node.A, val, node.x - step, node.y + dh * step);

                    }
                    else
                    {
                        if (val == Convert.ToInt32(node.weight))
                        {
                        }
                    }
                }

            }
        }

        public void SearchStart(int val)               // поиск и вставка
        {
            Search(ref top, val, top.x, top.y);
        }

        Node FindNodeVal(Node node, int val)              // поиск по значению
        {
            Node result = null;
            if (Convert.ToInt32(node.weight) == val)
            {
                node.visit = true;
                result = node;
            }
            else
            {
                if (node.A != null)
                    result = FindNodeVal(node.A, val);
                if ((result == null) & (node.B != null))
                    result = FindNodeVal(node.B, val);
            }
            return result;
        }

        public Node FindNodeValStart(int val)          // поиск по значению
        {
            if (top != null)
                return FindNodeVal(top, val);
            else
                return null;
        }

        public void DeSelect(Node node)                   // снятие признака посещения
        {
            if (node != null)
            {
                node.visit = false;
                DeSelect(node.A);
                DeSelect(node.B);
            }
        }

        public void Delta(Node node, int dx, int dy)      // смещение поддерева
        {
            node.x -= dx; node.y -= dy;
            if (node.A != null)
                Delta(node.A, dx, dy);
            if (node.B != null)
                Delta(node.B, dx, dy);
        }

        public Node FindNode(Node node, int x, int y)     // поиск по координатам 
        {
            Node result = null;
            if (node == null)
                return result;

            if (((node.x - x) * (node.x - x) + (node.y - y) * (node.y - y)) < 100)
                result = node;
            else
            {
                result = FindNode(node.A, x, y);
                if (result == null)
                    result = FindNode(node.B, x, y);
            }
            return result;
        }



        public void Draw()                             // рисование дерева
        {
            using (g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
                if (top != null)
                    top.DrawNode(g);
            }
        }

        public string SetStrSort(Node node)               // упорядоченная строка
        {
            string s = "";
            if (node == null)
                return s;

            if (node.B != null)
                s += SetStrSort(node.B);

            s += Convert.ToString(node.weight) + "\r\n";

            if (node.A != null)
                s += SetStrSort(node.A);

            return s;
        }

        public void FillNode(Node node)
        {
            if (node.depth < Depth)
            {
                node.A = new Node(null, null, 1, node.x, node.y);
                node.A.routeweight = node.routeweight + node.A.weight;
                if (node.A.routeweight < 0) { node.A.positive = false; }
                if (node.A.routeweight > 0) { node.A.negative = false; }
                node.A.depth = node.depth + 1;
                node.A.route = String.Concat(node.route, "A");
                FillNode(node.A);
                node.B = new Node(null, null, -1, node.x, node.y);
                node.B.routeweight = node.routeweight + node.B.weight;
                if (node.B.routeweight < 0) { node.A.positive = false; }
                if (node.B.routeweight > 0) { node.A.negative = false; }
                node.B.depth = node.depth + 1;
                node.B.route = String.Concat(node.route, "B");
                FillNode(node.B);
                //Console.WriteLine(node.A.route);
                //Console.WriteLine(node.B.route);
            }
            else if (node.depth == Depth)
            {
                //Console.WriteLine(node.route);
                /*if (node.positive == true)
                {

                }
                if (node.negative == true)
                {

                }*/
            }
        }

        public void CreateTree(MyTree myTree)
        {
            top.weight = 0;
            top.x = 200;
            top.y = 60;
            myTree.top.routeweight = 0;
            myTree.top.positive = true;
            myTree.top.negative = true;
            myTree.top.depth = 1;
            myTree.top.route = "C";
            if (Depth > 1)
            {
                FillNode(myTree.top);
            }
        }
    }
}
            