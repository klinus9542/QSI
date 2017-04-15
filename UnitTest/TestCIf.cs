using MathNet.Numerics.LinearAlgebra.Complex;
using QuantumToolkit;
using QuantumToolkit.Type;
using System;
using System.Numerics;
using static QuantumToolkit.InherentAssembly;
using static QuantumToolkit.QuantumAlgorithm;

namespace UnitTest
{
    public class TestCIf
    {
        static public void TestMethod()
        {
            var qbit = new QBit(false);
            Console.WriteLine("QBit value");
            Console.WriteLine(qbit.Value.ToComplexString());
            var matrixArray = new Matrix[2];
            Complex[,] array0 = { {0.5,0.5},
                                  {0.5,0.5} };
            matrixArray[0] = (Matrix)Matrix.Build.DenseOfArray(array0);
            Complex[,] array1 = { {0.5,-0.5},
                                  {-0.5,0.5} };
            matrixArray[1] = (Matrix)Matrix.Build.DenseOfArray(array1);
            var measureMatrix2 = new MeasureMatrix2(matrixArray);
            Console.WriteLine("Measurement matrix 0");
            Console.WriteLine(measureMatrix2.Value[0].ToComplexString());
            Console.WriteLine("Measurement matrix 1");
            Console.WriteLine(measureMatrix2.Value[1].ToComplexString());
            int count_0 = 0, count_1 = 0;
            for (var i = 0; i < 1000; i++)
            {
                qif(qbit, measureMatrix2,
                   () =>
                   {
                       count_0++;
                   },
                   () =>
                   {
                       count_1++;
                   }
                   );
                qbit.UnitaryTrans2(HGate.Value);
            }
            Console.WriteLine($"count_0 = {count_0}");
            Console.WriteLine($"count_1 = {count_1}");
        }
    }
}
