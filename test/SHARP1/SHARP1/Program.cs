using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHARP1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("HELLOW WORLD");
            stack stk = new stack();
            //for (int i = 0; i < 10; i++)
            //    stk.push(i);
            //for (int i = 0; i < 10; i++)
            //    Console.WriteLine(stk.pop());
            int i=-10, j=12;
            Console.WriteLine("i:{0} j:{1}", i, j);
            swapy(ref i,ref j);
            Console.WriteLine("i:{0} j:{1}", i, j);
        }
        static void swapy (ref int i,ref int j)
        {
            i = i + j;
            j = i - j;
            i = i - j;
        }
    }
}
