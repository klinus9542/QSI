using MathNet.Numerics.LinearAlgebra.Complex;
using QuantumToolkit;
using System;
using System.Numerics;

namespace UnitTest
{
    public class TestPermuteSys
    {
        static public void TestMethod()
        {
            var matrix = (Matrix)Matrix.Build.Dense(8, 8);
            for (var i = 0; i < 8; i++)
            {
                for (var j = 0; j < 8; j++)
                {
                    matrix.At(i, j, i * 8 + j);
                }
            }
            var orderArray = new int[3] { 2, 0, 1 };
            matrix = ClassicalAlgorithm.PermuteSys(matrix, orderArray);
            Console.WriteLine(matrix);
        }
    }
}
