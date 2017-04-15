using MathNet.Numerics.LinearAlgebra.Complex;
using QuantumToolkit;
using QuantumToolkit.Type;
using System;
using System.Collections.Generic;
using System.Numerics;
using static QuantumToolkit.InherentAssembly;
using static QuantumToolkit.QuantumAlgorithm;

namespace UnitTest
{
    public class TestCWhile
    {
        static public void TestMethod()
        {
            Console.WriteLine("QBit value");
            Console.WriteLine(new QBit(false).Value.ToComplexString());
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
            int count;
            var countNumber = new SortedDictionary<int, int>();

           // var qbit = new QBit(false);
            for (var i = 0; i < 1000; i++)
            {
                var qbit = new QBit(false);
                count = 0;


                qwhile(qbit, measureMatrix2, 1,
                    () =>
                    {
                        qbit.UnitaryTrans2(HGate.Value);
                        count++;
                        if (count > 1000)
                        {
                            return CWHILEFLOW.BREAK;
                        }
                        return CWHILEFLOW.CONTINUE;
                    }
                    );
                if (countNumber.ContainsKey(count))
                {
                    countNumber[count]++;
                }
                else
                {
                    countNumber[count] = 1;
                }

            }

            foreach (var pair in countNumber)
            {
                Console.WriteLine($"{pair.Key} is {pair.Value}");
            }
        }
    }
}
