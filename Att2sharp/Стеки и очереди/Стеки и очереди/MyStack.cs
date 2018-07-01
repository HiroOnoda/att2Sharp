using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Стеки_и_очереди
{
    class Node     // Узел для списка, стека, очереди
    {
        public int inf;
        public Node next;
        public Node(int inf, Node next)            // Конструктор
        {
            this.inf = inf;
            this.next = next;
        }
    }

    class MyStack    // Класс Стек
    {
        Node head;                              // голова 
        public int count;                       // число элементов
        public MyStack()                        // конструктор
        {
            head = null;
            count = 0;
        }

        public void Clear()
        {
            head = null;
            count = 0;
        }

        public bool StackIsEmpty()              // проверка на пустоту
        {
            return head == null;
        }

        public void PushStack(int inf)          // положить в стек
        {
            Node p = new Node(inf, head);
            head = p;
            count++;
        }

        public int PopStack()                   // взять из стека
        {
            int k = head.inf;
            head = head.next;
            count--;
            return k;
        }

        public string[] Printer() // вывод в массив строк
        {
            int L = 0;
            string[] st = new string[0];
            Node p = head;
            while (p != null)
            {
                Array.Resize<string>(ref st, ++L);
                st[L - 1] = p.inf.ToString();
                p = p.next;
            }
            return st;
        }
    }   

    class C
    {
        public static MyStack stack;
        public static MyStack tmpS;
        //public static MyQueue queue;
        //public static MyQueue tmpQ; // = new MyQueue();

        public static void DelMinus()
        {
            tmpS = new MyStack();
            while (!stack.StackIsEmpty())
            {
                int n = stack.PopStack();
                if (n >= 0)
                    tmpS.PushStack(n);
            }
            //while (!tmp.StackIsEmpty())
            //{
            //    int n = tmp.PopStack();
            //    stack.PushStack(n);
            //}
        }

        /*public static void DelMinusQueue()
        {
            tmpQ = new MyQueue();
            while (!stack.StackIsEmpty())
            {
                int n = stack.PopStack();
                if (n >= 0)
                    tmpQ.InQueue(n);
            }
            //while (!tmp.QueueIsEmpty())
            //{
            //    int n = tmp.OutQueue();
            //    stack.PushStack(n);
            //}
        }*/
    }
}
