using MathNet.Numerics.LinearAlgebra.Complex;
using QuantumToolkit;
using System;
using System.Numerics;

namespace UnitTest
{
    public class TestMatrix
    {
        static public void TestMethod()
        {
            Complex[,] array1 = { {1,2},
                                  {3,4} };
            var matrix1 = (Matrix)Matrix.Build.DenseOfArray(array1);
            var matrix1ConjugateTranspose = (Matrix)matrix1.ConjugateTranspose();

            Console.WriteLine(matrix1.ToComplexString());
            Console.WriteLine("ConjugateTranspose");
            Console.WriteLine(matrix1ConjugateTranspose.ToComplexString());

            Complex[,] array2 = { {new Complex(1,0),new Complex(0,-1)},
                                  {new Complex(0,1),new Complex(2,0)} };
            var matrix2 = (Matrix)Matrix.Build.DenseOfArray(array2);
            Console.WriteLine(matrix1.ToComplexString());
            Console.WriteLine($"IsHermitian:{matrix2.IsHermitian()}");
            Console.WriteLine($"IsNorm:{matrix2.IsNorm()}");
            Console.WriteLine($"IsUnitary:{matrix2.IsUnitary()}");
            Console.WriteLine();

            Complex[,] array3 = { {new Complex(0,0),new Complex(0,-1)},
                                  {new Complex(0,1),new Complex(0,0)} };
            var matrix3 = (Matrix)Matrix.Build.DenseOfArray(array3);
            Console.WriteLine(matrix3.ToComplexString());
            Console.WriteLine($"IsHermitian:{matrix3.IsHermitian()}");
            Console.WriteLine($"IsNorm:{matrix3.IsNorm()}");
            Console.WriteLine($"IsUnitary:{matrix3.IsUnitary()}");
            Console.WriteLine();
        }
    }
}
