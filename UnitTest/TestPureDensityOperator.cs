using MathNet.Numerics.LinearAlgebra.Complex;
using QuantumToolkit.Type;
using System;
using System.Numerics;

namespace UnitTest
{
    class TestPureDensityOperator
    {
        static public void TestMethod()
        {
            var matrixArray = new Matrix[2];
            Complex[,] array0 = { {0.5,0.5},
                                  {0.5,0.5} };
            matrixArray[0] = (Matrix)Matrix.Build.DenseOfArray(array0);
            Complex[,] array1 = { {0.5,-0.5},
                                  {-0.5,0.5} };
            matrixArray[1] = (Matrix)Matrix.Build.DenseOfArray(array1);
            var measureMatrixH = new MeasureMatrixH(matrixArray);
            Console.WriteLine("Measurement matrix 0");
            Console.WriteLine($"{measureMatrixH.Value[0]}");
            Console.WriteLine("Measurement matrix 1");
            Console.WriteLine($"{measureMatrixH.Value[1]}");
            int count_0 = 0, count_1 = 0;
            for (var i = 0; i < 100000; i++)
            {
                var pureDensityOperator = new PureDensityOperator(new QBit(false), new QBitBra(false));
                switch (pureDensityOperator.MeasuHResultIndex(measureMatrixH))
                {
                    case 0:
                        count_0++;
                        break;
                    case 1:
                        count_1++;
                        break;
                }
            }
            Console.WriteLine($"count_0 = {count_0}");
            Console.WriteLine($"count_1 = {count_1}");
        }
    }
}
