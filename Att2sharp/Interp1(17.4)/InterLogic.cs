using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Интепретатор_0
{
    public enum Operation  // типы операций +-^  < > = <> <= >= and or xor
    {
        PLUS = 1,
        MINUS = 2,
        MULTIPLY = 3,
        NONE = 0
    }


    public abstract class Node  // узел
    {
        public abstract object DoOperation();
        public int x;
        public int y;
        protected object value;
        public object Value
        {
            get { return DoOperation(); }
        }
    }

    class NodeConst : Node  // узел CONST
    {
        public NodeConst(object value)
        {
            this.value = value;
        }
        public override object DoOperation()
        {
            return value;
        }
    }

    class NodeOperation : Node
    {
        public Operation typeOperation;
        public Node left;
        public Node right;

        public NodeOperation(Operation t, Node left, Node right)
        {
            this.left = left;
            this.right = right;
            this.typeOperation = t;
        }

        public override object DoOperation()
        {
            double a = Convert.ToDouble(left.DoOperation());
            double b = Convert.ToDouble(right.DoOperation());
            switch (typeOperation)
            {
                case Operation.PLUS:
                    value = a + b;
                    return value;
                case Operation.MINUS:
                    value = a - b;
                    return value;
                case Operation.MULTIPLY:
                    value = a * b;
                    return value;
                default: throw new NotImplementedException();
            }
        }
    }

    public class TParser
    {
        public byte error;
        public Node top;
        public Node selectNode;
        const int step = 50;

        public Bitmap bitmap;     // канва для рисования
        const int dh = 1;
        Graphics g;
        Pen myPen;
        SolidBrush myBrush = (SolidBrush)Brushes.White;
        Font myFont;

        public TParser(int VW, int VH)  // конструктор
        {
            top = null;
            bitmap = new Bitmap(VW, VH);
            myFont = new Font("Courier New", 10, FontStyle.Bold);
            error = 0;
        }

        public int TestForSymbol(string s)//работает
        {
            if (s == "(") { return 0; }
            else if (s == ")") { return 1; }
            else if ((s == "+") || (s == "-") || (s == "*")) { return 2; }
            else if ((s == "0") || (s == "1") || (s == "2") || (s == "3") || (s == "4") || (s == "5") || (s == "6") || (s == "7") || (s == "8") || (s == "9")) { return 3; }
            else { return 4; }
        }
        public string StringSplit(ref string S, out string Symbol)//работает
        {
            int Nleft = 0;
            int Nright = 0;
            string s = S;
            string Output = "";
            string BufferString = "";
            string Ch = "1";
            while (Ch == "1")
            {
                BufferString = s.Substring(0, 1);
                s = s.Substring(1);
                if (TestForSymbol(BufferString) == 0) { Nleft++; }
                if (TestForSymbol(BufferString) == 1) { Nright++; }
                if ((TestForSymbol(BufferString) == 2) && (Nleft - Nright == 0)) { Ch = BufferString; }
                else { Output = String.Concat(Output, BufferString); }
            }
            Symbol = Ch;
            S = s;
            //Output = Output.Substring(1);
            return Output;
        }

        public void Recyrse(string input, out Node D)
        {
            D = null;
            if (input.Length == 2)
            {
                if (TestForSymbol(input.Substring(0, 1)) == 0)
                {
                    D = new NodeConst(Int32.Parse(input.Substring(1, 1)));
                }
                else
                {
                    D = new NodeConst(Int32.Parse(input.Substring(0, 1)));

                }
                //Console.Write(N + " ");
                //Console.WriteLine(D.Value);
            }
            else if (TestForSymbol(input) == 3)
            {
                D = new NodeConst(Int32.Parse(input));
                //Console.Write(N + " ");
                //Console.WriteLine(input);
            }
            else if ((TestForSymbol(input.Substring(0, 1)) == 0) && (input.Length >= 5))
            {
                string s1 = "";
                string s2 = input;
                s2 = s2.Substring(1);
                s2 = s2.Substring(0, s2.Length - 1);
                string ch = "";
                Node D1, D2;
                s1 = StringSplit(ref s2, out ch);
                Recyrse(s1, out D1);
                Recyrse(s2, out D2);
                Operation t = Operation.NONE; ;
                if (ch == "+") { t = Operation.PLUS; }
                if (ch == "-") { t = Operation.MINUS; }
                if (ch == "*") { t = Operation.MULTIPLY; }
                //ch to operation

                D = new NodeOperation(t, D1, D2);
                //Console.Write(N + " ");
                //Console.WriteLine(ch);
                //Console.WriteLine(ch);
            }
        }


        //тут идет отрисовка

        public void Clear(ref TParser myTree)
        {
            myTree.top = null;
        }

        public void Delta(Node p, int dx, int dy)  // смещение поддерева
        {
            p.x -= dx; p.y -= dy;
            if ((p is NodeOperation) && ((p as NodeOperation).left != null))
                Delta((p as NodeOperation).left, dx, dy);
            if ((p is NodeOperation) && ((p as NodeOperation).right != null))
                Delta((p as NodeOperation).right, dx, dy);
        }

        public Node FindNode(Node p, int x, int y) // поиск по координатам 
        {
            Node result = null;
            if (p == null)
                return result;
            if (((p.x - x) * (p.x - x) + (p.y - y) * (p.y - y)) < 100)
                result = p;
            else
            {
                if ((p is NodeOperation) && ((p as NodeOperation).left != null))
                    result = FindNode((p as NodeOperation).left, x, y);
                if ((result == null) && (p is NodeOperation) && ((p as NodeOperation).right != null))
                    result = FindNode((p as NodeOperation).right, x, y);
            }
            return result;
        }

        void DrawNode(Node p)               // рисование дерева
        {
            int R = 17;
            if ((p is NodeOperation) && ((p as NodeOperation).left != null))
                g.DrawLine(myPen, p.x, p.y, (p as NodeOperation).left.x, (p as NodeOperation).left.y);
            if ((p is NodeOperation) && ((p as NodeOperation).right != null))
                g.DrawLine(myPen, p.x, p.y, (p as NodeOperation).right.x, (p as NodeOperation).right.y);

            myBrush = (SolidBrush)Brushes.LightYellow;
            //    myBrush = (SolidBrush)Brushes.LightYellow;
            g.FillEllipse(myBrush, p.x - R, p.y - R, 2 * R, 2 * R);
            g.DrawEllipse(myPen, p.x - R, p.y - R, 2 * R, 2 * R);
            string s = "";
            if (p is NodeOperation)
                switch ((p as NodeOperation).typeOperation)
                {
                    case Operation.PLUS: s = "+"; break;
                    case Operation.MINUS: s = "-"; break;
                    case Operation.MULTIPLY: s = "*"; break;
                }
            else
                s = Convert.ToString(p.Value);

            SizeF size = g.MeasureString(s, myFont);
            g.DrawString(s, myFont, Brushes.Black,
                p.x - size.Width / 2,
                p.y - size.Height / 2);

            if ((p is NodeOperation) && ((p as NodeOperation).left != null))
                DrawNode((p as NodeOperation).left);
            if ((p is NodeOperation) && ((p as NodeOperation).right != null))
                DrawNode((p as NodeOperation).right);
        }

        public void Draw()                 
        {
            using (g = Graphics.FromImage(bitmap))
            {
                Color cl = Color.FromArgb(255, 255, 255);
                g.Clear(cl);
                myPen = Pens.Black;
                if (top != null)
                    DrawNode(top);
            }
        }

        public void SetXY(Node p, int x, int y)
        {
            p.x = x; p.y = y;
            if ((p is NodeOperation) && ((p as NodeOperation).left != null))
                SetXY((p as NodeOperation).left, x - step, y + step);
            if ((p is NodeOperation) && ((p as NodeOperation).right != null))
                SetXY((p as NodeOperation).right, x + step, y + step);
        }
    }
}

/*public enum Operation  // типы операций +-^  < > = <> <= >= and or xor
{
    PLUS = 1,
    MINUS = 2,
    MULTIPLY = 3,
    DIVIDE = 4,
    NONE = 0
}


public abstract class Node  // узел
{
    public abstract object DoOperation();
    public int x;
    public int y;
    protected object value;
    public object Value
    {
        get { return DoOperation(); }
    }
}

class NodeConst : Node  // узел CONST
{
    public NodeConst(object value)
    {
        this.value = value;
    }
    public override object DoOperation()
    {
        return value;
    }
}

class NodeOperation : Node
{
    public Operation typeOperation;
    public Node left;
    public Node right;

    public NodeOperation(Operation t, Node left, Node right)
    {
        this.left = left;
        this.right = right;
        this.typeOperation = t;
    }

    public override object DoOperation()
    {
        double a = Convert.ToDouble(left.DoOperation());
        double b = Convert.ToDouble(right.DoOperation());
        switch (typeOperation)
        {
            case Operation.PLUS:
                value = a + b;
                return value;
            case Operation.MINUS:
                value = a - b;
                return value;
            case Operation.MULTIPLY:
                value = a * b;
                return value;
            case Operation.DIVIDE:
                value = a / b;
                return value;
            default: throw new NotImplementedException();
        }
    }
}

public class TParser
{
    public byte error;
    public Node top;
    public Node selectNode;
    const int step = 50;

    public Bitmap bitmap;     // канва для рисования
    const int dh = 1;
    Graphics g;
    Pen myPen;
    SolidBrush myBrush = (SolidBrush)Brushes.White;
    Font myFont;


    public void Clear(ref TParser myTree)
    {
        myTree.top = null;
    }

    public TParser(int VW, int VH)  // конструктор
    {
        top = null;
        bitmap = new Bitmap(VW, VH);
        myFont = new Font("Courier New", 10, FontStyle.Bold);
        error = 0;
    }

    public void Delta(Node p, int dx, int dy)  // смещение поддерева
    {
        p.x -= dx; p.y -= dy;
        if ((p is NodeOperation) && ((p as NodeOperation).left != null))
            Delta((p as NodeOperation).left, dx, dy);
        if ((p is NodeOperation) && ((p as NodeOperation).right != null))
            Delta((p as NodeOperation).right, dx, dy);
    }

    public Node FindNode(Node p, int x, int y) // поиск по координатам 
    {
        Node result = null;
        if (p == null)
            return result;
        if (((p.x - x) * (p.x - x) + (p.y - y) * (p.y - y)) < 100)
            result = p;
        else
        {
            if ((p is NodeOperation) && ((p as NodeOperation).left != null))
                result = FindNode((p as NodeOperation).left, x, y);
            if ((result == null) && (p is NodeOperation) && ((p as NodeOperation).right != null))
                result = FindNode((p as NodeOperation).right, x, y);
        }
        return result;
    }

    void DrawNode(Node p)               // рисование дерева
    {
        int R = 17;
        if ((p is NodeOperation) && ((p as NodeOperation).left != null))
            g.DrawLine(myPen, p.x, p.y, (p as NodeOperation).left.x, (p as NodeOperation).left.y);
        if ((p is NodeOperation) && ((p as NodeOperation).right != null))
            g.DrawLine(myPen, p.x, p.y, (p as NodeOperation).right.x, (p as NodeOperation).right.y);

        myBrush = (SolidBrush)Brushes.LightYellow;
        //    myBrush = (SolidBrush)Brushes.LightYellow;
        g.FillEllipse(myBrush, p.x - R, p.y - R, 2 * R, 2 * R);
        g.DrawEllipse(myPen, p.x - R, p.y - R, 2 * R, 2 * R);
        string s = "";
        if (p is NodeOperation)
            switch ((p as NodeOperation).typeOperation)
            {
                case Operation.PLUS: s = "+"; break;
                case Operation.MINUS: s = "-"; break;
                case Operation.MULTIPLY: s = "*"; break;
                case Operation.DIVIDE: s = "/"; break;
            }
        else
            s = Convert.ToString(p.Value);

        SizeF size = g.MeasureString(s, myFont);
        g.DrawString(s, myFont, Brushes.Black,
            p.x - size.Width / 2,
            p.y - size.Height / 2);

        if ((p is NodeOperation) && ((p as NodeOperation).left != null))
            DrawNode((p as NodeOperation).left);
        if ((p is NodeOperation) && ((p as NodeOperation).right != null))
            DrawNode((p as NodeOperation).right);
    }

    public void Draw()                  // рисование дерева
    {
        using (g = Graphics.FromImage(bitmap))
        {
            Color cl = Color.FromArgb(255, 255, 255);
            g.Clear(cl);
            myPen = Pens.Black;
            if (top != null)
                DrawNode(top);
        }
    }

    public void SetXY(Node p, int x, int y)
    {
        p.x = x; p.y = y;
        if ((p is NodeOperation) && ((p as NodeOperation).left != null))
            SetXY((p as NodeOperation).left, x - step, y + step);
        if ((p is NodeOperation) && ((p as NodeOperation).right != null))
            SetXY((p as NodeOperation).right, x + step, y + step);
    }

    string Pop(ref string s, int n)
    {
        string result = s.Substring(0, n);
        s = s.Substring(n);
        s = s.Trim();
        return result;
    } // взять n символов

    char Peek(ref string s)
    {
        char result = s[0];
        return result;
    }

    bool Test(char ch, params char[] nums)
    {
        return Array.IndexOf(nums, ch) > 0;
    }


    string SignSplit(ref string s)
    {
        string s1 = "";
        int LBC = 0;
        int RBC = 0;
        if (s.Length != 0)
        {
            {
                do
                {
                    if (Peek(ref s) == '(')
                    {
                        LBC++;

                    };
                    if (Peek(ref s) == ')')
                    {
                        RBC++;
                    };
                    s1 = String.Concat(s1, Pop(ref s, 1));

                } while (((Peek(ref s).CompareTo('+') != 0) && (Peek(ref s).CompareTo('/') != 0) && (Peek(ref s).CompareTo('-') != 0) && (Peek(ref s).CompareTo('*') != 0)) && (LBC == RBC));

            }
        }          
        return s1;
    }

    public void Expression(ref string s, out Node D)  // выражение
    {
        D = null;
        s = s.Trim();
        if ((s.Length != 0) && (s != ""))
        {
            if (Test(s[0], '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'))
            {
                D = new NodeConst(Int32.Parse(Pop(ref s, 1)));
            }
            else if (Peek(ref s) == '(')
            {
                s = s.Substring(1);
                s = s.Substring(0,s.Length);
                string s1 = SignSplit(ref s);
                string sign = Pop(ref s, 1);
                Node DL, DR = null;
                Expression(ref s1, out DL);
                Expression(ref s, out DR);
                Operation op = Operation.NONE;
                if (sign == "+") { op = Operation.PLUS; }
                else if (sign == "-") { op = Operation.MINUS; }
                else if (sign == "/") { op = Operation.DIVIDE; }
                else if (sign == "*") { op = Operation.MULTIPLY; }
                D = new NodeOperation(op, DL, DR);
            }
        }
        else
        {
            D = null; error = 4; // ожидается выражение
        }
    }  // Expression

    public string errorToString()
    {
        string s = "";
        switch (error)
        {
            case 4: s = "ожидается выражение"; break;
            case 5: s = "ожидается слагаемое"; break;
            case 6: s = "ожидается множитель"; break;
            case 9: s = "ожидается )"; break;
        }
        return s;
    }
}*/
