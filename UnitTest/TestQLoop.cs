using MathNet.Numerics.LinearAlgebra.Complex;
using QuantumToolkit.Type;
using System;
using System.Collections.Generic;
using System.Numerics;
using static QuantumToolkit.InherentAssembly;
using static QuantumToolkit.QuantumAlgorithm;
using static System.Math;
using QuantumToolkit.Runtime;

namespace UnitTest
{
    class TestQloop
    {
        static public void TestMethodUnitary2(int loopCount)
        {
            int count;
            var countNumber = new SortedDictionary<int, int>();

            //array2 is a pure state |+><+|
            Complex[,] array2 = { {0.5,0.5},
                                  {0.5,0.5} }; //can be concentrated to assembly

            for (var i = 0; i < loopCount; i++)
            {
                var rou = new PureDensityOperator((Matrix) Matrix.Build.DenseOfArray(array2));
                count = 0;
                rou.UnitaryTransH(HGate.Value);

                qwhile(rou, MeasureMatrixZeroOne,1,
                    () =>
                    {
                        rou.UnitaryTransH(HGate.Value);
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

        static public void TestMethod(int loopCount)
        {
            // E0,E1 for Kraus Opearator
            // E0= (|0><0| + |1><1|)/sqrt(2),E1=  (|0><1|)/sqrt(2)
            var matrixArrayE = new Matrix[2];
            Complex[,] array0 = { {1,0},
                                  {0,1/Sqrt(2)} };
            matrixArrayE[0] = (Matrix)Matrix.Build.DenseOfArray(array0);
            Complex[,] array1 = { {0,1/Sqrt(2)},
                                  {0,0} };
            matrixArrayE[1] = (Matrix)Matrix.Build.DenseOfArray(array1);
            var superE = new SuperOperator(matrixArrayE);

            //rou for main state, it is a pure state|+><+|
            Complex[,] array2 = { {0.5,0.5},
                                  {0.5,0.5} };
            //var pureDensityOperator = new PureDensityOperator((Matrix)Matrix.Build.DenseOfArray(array2));

          

            int count;
            var countNumber = new SortedDictionary<int, int>();
            for (var i = 0; i < loopCount; i++)
            {
                var rou = new PureDensityOperator((Matrix)Matrix.Build.DenseOfArray(array2));
                count = 0;
                SuperMatrixTrans(rou, superE);

                qwhile(rou, MeasureMatrixZeroOne, 1,
                    () =>
                    {
                        rou.UnitaryTransH(HGate.Value);
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

            // Console.WriteLine($"pureDensityOperator = {rou.Value}");
        }

        static public void TestMethod()
        {
            for (var i = 0; i < 99; i++)
            {
                RecordOperatorSequence.Start();
                TestMethod(1);
                RecordOperatorSequence.FindBasicPath();
                if (RecordOperatorSequence.Steps.Count != RecordOperatorSequence.RetSteps.Count)
                {
                    Console.WriteLine("Original");
                    RecordOperatorSequence.DisplayRecords(RecordOperatorSequence.Steps);
                    Console.WriteLine("\n\n\n");
                    Console.WriteLine("BasicPath");
                    RecordOperatorSequence.DisplayRecords(RecordOperatorSequence.RetSteps);
                    Console.WriteLine("\n\n\n");
                }
            }
        }
    }
}
