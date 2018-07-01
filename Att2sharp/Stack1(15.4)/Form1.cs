using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StackSpace;

namespace Stack1_15._4_
{
    public partial class Form1 : Form
    {
        MyStack stack;
        public Form1()
        {
            InitializeComponent();
            stack = new MyStack();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            stack.Clear();
            for (int i = 0; i < textBox1.Lines.Length; i++)
                stack.PushStack(Convert.ToInt32(textBox1.Lines[i]));
            textBox2.Lines = stack.Printer();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            stack.PushStack(Convert.ToInt32(textBox3.Text));
            textBox2.Lines = stack.Printer();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!stack.StackIsEmpty())
            {
                textBox3.Text = stack.PopStack().ToString();
                textBox2.Lines = stack.Printer();
            }
            else
                MessageBox.Show("Стек пуст!");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!stack.StackIsEmpty())
            {
                textBox1.Lines = stack.TaskPrinter();
            }
            else
                MessageBox.Show("Стек пуст!");
        }
    }
}
