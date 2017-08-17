using MathNet.Numerics.LinearAlgebra.Complex;
using QuantumToolkit;
using QuantumToolkit.Type;
using System;
using System.Collections.Generic;
using System.Numerics;
using static System.Math;

namespace UnitTest
{
    class TestQBit
    {
        static public void TestMethod()
        {
            var qBit1 = new QBit(false);
            Complex[,] array1 = { {new Complex(0,0),new Complex(1,0)},
                                  {new Complex(1,0),new Complex(0,0)} };
            var matrix1 = (Matrix)Matrix.Build.DenseOfArray(array1);
            Console.WriteLine(qBit1.Value.ToComplexString());
            qBit1.UnitaryTrans2(matrix1);
            Console.WriteLine(qBit1.Value.ToComplexString());
        }

        static public void TestMethod2()
        {
            var list = new List<Matrix>();
            Complex[,] array1 = { {0.5,0.5},
                                  {0.5,0.5} };
            var value = (Matrix)Matrix.Build.DenseOfArray(array1);
            list.Add(value);
            Complex[,] array2 = { {0.5,-0.5},
                                  {-0.5,0.5} };
            value = (Matrix)Matrix.Build.DenseOfArray(array2);
            list.Add(value);
            var measureMatrix2 = new MeasureMatrix2(list.ToArray());
            for (;;)
            {
                var ret = new int[2];
                for (var i = 0; i < 1000; i++)
                {
                    var qBit = new QBit(false);
                    ret[qBit.Measu2ResultIndex(measureMatrix2)]++;
                }
                for (var i = 0; i < ret.Length; i++)
                {
                    Console.WriteLine($"{i} is {ret[i]}");
                }
            }
        }

        static public void TestMethod3()
        {
            var list = new List<Matrix>();
            Complex[,] array1 = { {0.5,0.5},
                                  {0.5,0.5} };
            var value = (Matrix)Matrix.Build.DenseOfArray(array1);
            list.Add(value);
            Complex[,] array2 = { {0.5,-0.5},
                                  {-0.5,0.5} };
            value = (Matrix)Matrix.Build.DenseOfArray(array2);
            list.Add(value);
            var measureMatrix2 = new MeasureMatrix2(list.ToArray());
            Complex[,] array3 = { {1/Sqrt(2),1/Sqrt(2)},
                                  {1/Sqrt(2),-1/Sqrt(2)} };
            var unitaryTransMatrix = (Matrix)Matrix.Build.DenseOfArray(array3);
            for (;;)
            {
                var ret = new int[2];
                for (var i = 0; i < 100; i++)
                {
                    var qBit = new QBit(false);
                    qBit.UnitaryTrans2(unitaryTransMatrix);
                    ret[qBit.Measu2ResultIndex(measureMatrix2)]++;
                }
                for (var i = 0; i < ret.Length; i++)
                {
                    Console.WriteLine($"{i} is {ret[i]}");
                }
            }
        }
    }
}
