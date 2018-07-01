using System;

namespace Sort_16._9_
{
    class Program
    {
        static void InsertSort(int[,] arr)
        {
            int n = arr.Length/2;
            int buffer0 = 0;
            int buffer1 = 0;
            for (int j = 1; j < n; j++)
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
            }
        }

        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            int[,] arr = new int[8,2];
            arr[0,0] = 6; 
            arr[1,0] = 5;
            arr[2,0] = 3; arr[2, 1] = 5;
            arr[3,0] = 1;
            arr[4,0] = 3; arr[4, 1] = 3;
            arr[5,0] = 7;
            arr[6,0] = 2;
            arr[7,0] = 4;

            InsertSort(arr);
            for(int i = 0;i < 8; i++)
            {
                Console.Write(arr[i, 0]);
                Console.Write(" ");
                Console.Write(arr[i, 1]);
                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}
