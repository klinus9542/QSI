using MathNet.Numerics.LinearAlgebra.Complex;
using QuantumToolkit;
using QuantumToolkit.Type;
using System;
using System.Numerics;
using static QuantumToolkit.QuantumAlgorithm;

namespace UnitTest
{
    class TestSuperMatrixTrans
    {
        static public void TestMethod()
        {
            var matrixArray = new Matrix[2];
            Complex[,] array0 = { {1,0},
                                  {0,0} };
            matrixArray[0] = (Matrix)Matrix.Build.DenseOfArray(array0);
            Complex[,] array1 = { {0,0},
                                  {0,1} };
            matrixArray[1] = (Matrix)Matrix.Build.DenseOfArray(array1);
            var superOperator = new SuperOperator(matrixArray);
            Complex[,] array2 = { {0.5,0.5},
                                  {0.5,0.5} };
            var pureDensityOperator = new DensityOperator((Matrix)Matrix.Build.DenseOfArray(array2));
            SuperMatrixTrans(pureDensityOperator, superOperator);
            Console.WriteLine($"pureDensityOperator = {pureDensityOperator.Value.ToComplexString()}");
        }
    }
}
