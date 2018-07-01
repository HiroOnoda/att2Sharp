using System;
using System.IO;

namespace Queue_15._19_
{
    class Program
    {
        class MyNode
        {
            public int inf;
            public MyNode next;
            public MyNode(int inf, MyNode next)
            // конструктор
            {
                this.inf = inf;
                this.next = next;
            }
        }

        class MyList
        {
            public MyNode head;
            public MyNode tail;          
            public MyList()
            {
                head = null;
            }             
            public void Add(int inf)
            {

                MyNode p = new MyNode(inf, null);
                if (head == null)
                {
                    head = p;
                }
                if (tail == null)
                {
                    tail = p;
                }
                else
                {
                    tail.next = p;
                }
                tail = p;               
            }
            public string[] Printer()
            {
                string[] st = new string[0];
                int L = 0;
                MyNode p = head;
                if (p != null)
                    do
                    {
                        Array.Resize<string>(ref st, ++L);
                        st[L - 1] = p.inf.ToString();
                        p = p.next;
                    }
                    while (p != null);
                return st;

            }
            public void Insert(int index, int val)
            {
                if (index != 0)
                {
                    MyNode p = head;
                    for (int i = 0; i < index-1; i++)
                        p = p.next;
                    MyNode q = new MyNode(val, p.next);
                    p.next = q;
                }
                else
                {
                    MyNode q = new MyNode(val, head);
                    head = q;
                }
            }
        }

        static void Main(string[] args)
        {
            string path = "Input.txt";
            string str = "";
            using (StreamReader sr = new StreamReader(path))
            {
                str = sr.ReadLine();
            }            
            int d = 0;
            int n = 3;
            string[] words = str.Split(new char[] { ' ' });
            MyList list = new MyList();
            foreach (string s in words)
            {
                list.Add(Int32.Parse(s));
                Console.Write(s);
            }
            Console.WriteLine();
            list.Insert(n, d);
            words = list.Printer();
            foreach (string s in words)
            {
                Console.Write(s);
            }

            //Console.WriteLine("Hello World!");
            
            Console.ReadKey();
        }
    }
}
