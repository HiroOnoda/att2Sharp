using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Стеки_и_очереди
{
    public partial class FormMain : Form
    {
        MyStack stack;
        //MyQueue queue;
        public FormMain()
        {
            InitializeComponent();
            C.stack = new MyStack();
            //C.queue = new MyQueue();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            C.stack.Clear();
            for (int i = 0; i < textBox1.Lines.Length; i++)
                C.stack.PushStack(Convert.ToInt32(textBox1.Lines[i]));
            textBox2.Lines = C.stack.Printer();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            C.stack.PushStack(Convert.ToInt32(textBox3.Text));
            textBox2.Lines = C.stack.Printer();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!C.stack.StackIsEmpty())
            {
                textBox3.Text = C.stack.PopStack().ToString();
                textBox2.Lines = C.stack.Printer();
            }
            else
                MessageBox.Show("Стек пуст!");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            C.DelMinus();
            textBox2.Lines = C.tmpS.Printer();
        }
    }
}
