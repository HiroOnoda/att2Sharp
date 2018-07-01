using System;

namespace Sort_16._9_
{
    class Program
    {

        struct Point
        {
            int x ;
            int y ;
            public void Create(int dx,int dy) 
            {
                dx = x;
                dy = y;
            }
            public void PrintElement(out string dx, out string dy) 
            {
                dx = Convert.ToString(x);
                dy = Convert.ToString(y);
            }

            static int ComparePoints(Point a, Point b)
            {
                if (a.x > b.x) 
                {
                    return 0; 
                }
                else if ((a.x == b.x) && (a.y > b.y))
                {
                    return 1;
                }
                else 
                {
                    return 2;
                }
            }

            static void Swap(Point a, Point b)
            {
                int bufferX = a.x;
                int bufferY = a.y;
                a.x = b.x;
                a.y = b.y;
                b.x = bufferX;
                b.y = bufferY;
            }

            public static void InsertSort(Point[] arr)
            {
                int n = arr.Length / 2;
                int buffer0 = 0;
                int buffer1 = 0;
                for (int i = 1; i < n; i++)
                    for (int j = i; j > 0; j--)
                    {
                        if (ComparePoints(arr[j - 1], arr[j]) == 0)
                        {
                            Swap(arr[j - 1], arr[j]);
                            /*buffer0 = arr[j, 0];
                            buffer1 = arr[j, 1];

                            arr[j, 0] = arr[j - 1, 0];
                            arr[j, 1] = arr[j - 1, 1];

                            arr[j - 1, 0] = buffer0;
                            arr[j - 1, 1] = buffer1;*/
                        }
                        else if (ComparePoints(arr[j - 1], arr[j]) == 1)
                        {
                            Swap(arr[j - 1], arr[j]);
                            /*buffer1 = arr[j, 1];

                            arr[j, 1] = arr[j - 1, 1];

                            arr[j - 1, 1] = buffer1;*/
                        }

                    }
            }

        }



          
   




            //////////////
            /*for (int j = 1; j < n; j++)
            {
                for (int i = j-1; i >= 0; i--)
                {
                    if((i == 0) && 
                        ((arr[i,0] > arr[j,0]) || 
                        ((arr[i,0] == arr[j,0]) && (arr[i,1] > arr[j,1])) || 
                        ((arr[i, 0] == arr[j, 0]) && (arr[i, 1] == arr[j, 1])))) //для нулевого элемента
                    {
                        buffer0 = arr[j, 0];
                        buffer1 = arr[j, 1];
                        for (int k = j; k > i; k--)
                        {
                            arr[k,0] = arr[k - 1,0];
                            arr[k, 1] = arr[k - 1, 1];
                        }
                        arr[i, 0] = buffer0;
                        arr[i, 1] = buffer1;
                    }
                    if(i > 0)
                    {
                        if (
                        (i > 0) &&
                        ((arr[i - 1, 0] < arr[j, 0]) || ((arr[i - 1, 0] == arr[j, 0]) && (arr[i - 1, 1] < arr[j, 1]))) &&
                        ((arr[i, 0] > arr[j, 0]) || ((arr[i, 0] == arr[j, 0]) && (arr[i, 1] > arr[j, 1])) || ((arr[i, 0] == arr[j, 0]) && (arr[i, 1] == arr[j, 1])))
                        ) //для ненулевого элемента
                        {
                            buffer0 = arr[j, 0];
                            buffer1 = arr[j, 1];
                            for (int k = j; k > i; k--)
                            {
                                arr[k, 0] = arr[k - 1, 0];
                                arr[k, 1] = arr[k - 1, 1];
                            }
                            arr[i, 0] = buffer0;
                            arr[i, 1] = buffer1;
                        }
                    }
                    
                }
            }*/
        

        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            Point[] arr = new Point[8];

            arr[0].Create(6,0);
            arr[1].Create(5,0);
            arr[2].Create(3, 5);
            arr[3].Create(1,0);
            arr[4].Create(3,3);
            arr[5].Create(7,0);
            arr[6].Create(2,0);
            arr[7].Create(4,0);
            string dx = "";
            string dy = "";
            Point.InsertSort(arr);
            for(int i = 0;i < 8; i++)
            {

                arr[i].PrintElement(out dx, out dy);
                Console.Write(dx);
                Console.Write(" ");
                Console.Write(dy);
                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}
