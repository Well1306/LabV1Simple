using System;
using System.Runtime.InteropServices;

namespace laba33
{
    class Program
    {/*
        [DllImport("dll.dll", CallingConvention = CallingConvention.Cdecl)]
        //
        public static extern void mkl_sin(int n, double[] a, double[] y, char m);
        //public static extern void ppp();
        */
        static void Main(string[] args)
        {

            VMBenchmark test = new();
            test.Add(500);
            test.Add(10000, 0, 100);
            test.Add(10000000, -1000, 1000);
            test.Add(100, 0, 0);
            test.Add(1, 0, 10);
            Console.WriteLine(test.ToString());
            test.Add(10000000);
            test.SaveAsText("t.txt");
        }
    }
}
