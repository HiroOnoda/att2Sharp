using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestBedSharp
{
    class Program
    {
        /*static char Peek(ref string s)
        {
            char result = s[0];
            return result;
        }*/

        static bool Test(char ch, params char[] nums)
        {
            return Array.IndexOf(nums, ch) >= 0;
        }

        static void Main(string[] args)
        {          
            string str = "hello";
            bool b = Test(str[0],'h');
            int i = Array.IndexOf(nums, ch);
            Console.WriteLine(b);
            Console.WriteLine(str);
            Console.ReadKey();
        }
    }
}
