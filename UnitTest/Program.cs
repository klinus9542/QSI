using MathNet.Numerics;
using System;

namespace UnitTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Control.UseNativeMKL();//Control.UseManaged();
            Console.WriteLine(Control.LinearAlgebraProvider);
            Console.WriteLine();
            TestSecCode.TestMethod();
            Console.ReadKey(true);
        }
    }
}
