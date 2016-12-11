using MathNet.Numerics.LinearAlgebra.Complex;
using QuantumToolkit.Type;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace UnitTest
{
    class TestMeasureMatrix2
    {
        static public void TestMethod()
        {
            var list = new List<Matrix>();
            Complex[,] array1 = { {0.5,0.5},
                                  {0.5,0.5} };
            var value = (Matrix)Matrix.Build.DenseOfArray(array1);
            Console.WriteLine(value);
            list.Add(value);
            Complex[,] array2 = { {0.5,-0.5},
                                  {-0.5,0.5} };
            value = (Matrix)Matrix.Build.DenseOfArray(array2);
            Console.WriteLine(value);
            list.Add(value);
            Console.WriteLine("MeasureMatrix2");
            var measureMatrix2 = new MeasureMatrix2(list.ToArray());
            foreach (var matrix in measureMatrix2.Value)
            {
                Console.WriteLine(matrix);
            }
        }

        static public void TestMethod2()
        {
            var list = new List<Matrix>();
            Complex[,] array1 = { {0.5,0.5},
                                  {0.5,1} };
            var value = (Matrix)Matrix.Build.DenseOfArray(array1);
            Console.WriteLine(value);
            list.Add(value);
            Complex[,] array2 = { {0.5,-0.5},
                                  {-0.5,0.5} };
            value = (Matrix)Matrix.Build.DenseOfArray(array2);
            Console.WriteLine(value);
            list.Add(value);
            Console.WriteLine("MeasureMatrix2");
            var measureMatrix2 = new MeasureMatrix2(list.ToArray());
            foreach (var matrix in measureMatrix2.Value)
            {
                Console.WriteLine(matrix);
            }
        }
    }
}