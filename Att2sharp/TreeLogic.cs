using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Att2sharp
{
    public class Node
    {
        public object data;
        public Node left;
        public Node right;
        public int x;
        public int y;
        public bool visit;
        public int count;

        public Node(Node left, Node right, object data, int x, int y) // конструктор
        {
            this.left = left;
            this.right = right;
            this.data = data;
            this.x = x;
            this.y = y;
            this.visit = false;
            this.count = 1;
        }
    }
    public class MyTree
    {
            Node top;                 // вершина дерева
            public Node SelectNode;   // выделенный узел
            public Bitmap bitmap;     // канва для рисования
            //public class Node         // класс узла
            const int step = 30;
            const int Geom = 1;
            Graphics g;
            Pen MyPen;
            SolidBrush MyBrush;
            Font MyFont;

            public void Search(int val)  // поиск и вставка
            public Node FindNode(int x, int y)// поиск по координатам
            Node FindNodeVal(int val)//поиск по значению
            public void Insert(ref Node t, int data, int x, int y) // вставка
            {
                if (t == null)
                    t = new Node(null, null, data, x, y);
                else
                  if (data <= Convert.ToInt32(t.data))
                    Insert(ref t.left, data, t.x - step,
                        t.y + dh * step);
                else
                    Insert(ref t.right, data, t.x + step,
                        t.y + dh * step);
            }
            void DeSelect() //снятие признака посещения
            public void Delta(Node p, int dx, int dy)// смещение поддерева
            public void Delete(Node p)   // удаление узла
            public void Draw()           // рисование дерева


    
    }
