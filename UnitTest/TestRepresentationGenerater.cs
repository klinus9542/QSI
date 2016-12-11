using MathNet.Numerics.LinearAlgebra.Complex;
using System;
using System.Numerics;
using QuantumToolkit;
using System.Collections.Generic;

namespace UnitTest
{
    class TestRepresentationGenerater
    {
        static public void TestMethod()
        {
            var matrixArray0 = new List<Matrix>();
            Complex[,] array0 = { {new Complex(0,1),1},
                                  {1,new Complex(0,1)} };
            matrixArray0.Add((Matrix)Matrix.Build.DenseOfArray(array0));


            var matrixArray1 = new List<Matrix>();
            Complex[,] array1 = { {1,new Complex(0,1)},
                                  {new Complex(0,1),1} };
            matrixArray1.Add((Matrix)Matrix.Build.DenseOfArray(array0));
            matrixArray1.Add((Matrix)Matrix.Build.DenseOfArray(array1));

            var ret0 = ClassicalAlgorithm.RepresentationGenerater(matrixArray0);
            var ret1 = ClassicalAlgorithm.RepresentationGenerater(matrixArray1);

            Console.WriteLine("ret0");
            Console.WriteLine(ret0);
            Console.WriteLine("ret1");
            Console.WriteLine(ret1);
        }
    }
}
