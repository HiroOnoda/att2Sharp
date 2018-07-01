using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StackSpace

{
    class Node     
    {
        public int inf;
        public Node next;
        public Node(int inf, Node next)            
        {
            this.inf = inf;
            this.next = next;
        }
    }

    class MyStack    
    {
        public Node head;                              
        public int count;                       
        public MyStack()                        
        {
            head = null;
            count = 0;
        }

        public void Clear()
        {
            head = null;
            count = 0;
        }

        public bool StackIsEmpty()              
        {
            return head == null;
        }

        public void PushStack(int inf)          
        {
            Node p = new Node(inf, head);
            head = p;
            count++;
        }

        public int PopStack()                   
        {
            int k = head.inf;
            head = head.next;
            count--;
            return k;
        }

        public string[] Printer() 
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

        public string[] TaskPrinter()
        {
            int L = 1;
            string[] st = new string[1];
            Node p = head;
            st[0] = Convert.ToString(p.inf);
            while (p != null)
            {
                if(Convert.ToString(p.inf) == st[L-1])
                {
                    p = p.next;
                }
                else
                {
                    Array.Resize<string>(ref st, ++L);
                    st[L - 1] = p.inf.ToString();
                    p = p.next;
                }                
            }
            return st;
        }
    }
}