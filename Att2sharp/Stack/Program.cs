using System;
using System.IO;

namespace Stack_15._34_
{
    class Program
    {
        class MyNode
        {
            public string inf;
            public MyNode next;
            public MyNode(string inf, MyNode next)
            // конструктор
            {
                this.inf = inf;
                this.next = next;
            }
        }

        class MyList
        {
            public MyNode head;
            public int count;
            public MyList()
            {
                head = null;
                count = 0;
            }
            public void Add(string inf)
            {
                MyNode p = new MyNode(inf, head);
                head = p;
                count++;
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

            public string TaskPrinter(string[] st)
            {
                string output = "";
                int L;

                for (int i = 0; i < st.Length; i++)
                {
                    st[i] = st[i].Trim();
                    if ((st[i] == "1") || (st[i] == "2") || (st[i] == "3") || (st[i] == "4") || (st[i] == "5") || (st[i] == "6") || (st[i] == "7") || (st[i] == "8") || (st[i] == "9") || (st[i] == "0"))
                    {
                        output = output + st[i];
                    }
                }
                //int.TryParse(output, out L);
                return output;
            }

            /*public MyNode FindNode(int val)
            {
                MyNode p = head;
                bool ok = false;
                while ((p != null) && !ok)
                {
                    ok = p.inf == val;
                    if (!ok)
                        p = p.next;
                }
                return p;
            }*/
        }

        static void Main(string[] args)
        {
            string path = "Input.txt";
            string str = "";
            using (StreamReader sr = new StreamReader(path))
            {
                str = sr.ReadLine();
            }
            string[] words = str.Split(new char[] { ' ' });
            MyList list = new MyList();
            foreach (string s in words)
            {
                list.Add(s);
                Console.Write(s + " ");
            }
            words = list.Printer();
            Console.WriteLine();
            foreach (string s in words)
            {
                Console.Write(s + " ");
            }
            Console.WriteLine();
            string i = list.TaskPrinter(words);
            Console.Write(i);
            Console.ReadKey();

        }
    }
}