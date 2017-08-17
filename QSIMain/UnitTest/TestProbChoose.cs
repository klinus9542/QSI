using System;
using static QuantumToolkit.ClassicalAlgorithm;

namespace UnitTest
{
    class TestProbChoose
    {
        static public void TestMethod()
        {
            for(;;)
            {
                var probability = new double[] { 0, 0.1, 0.2, 0.3, 0.4 };
                var ret = new int[probability.Length];
                for (var i = 0; i < 1000000; i++)
                {
                    ret[ProbChoose(probability)]++;
                }
                for (var i = 0; i < ret.Length; i++)
                {
                    Console.WriteLine($"{probability[i]} is {ret[i]}");
                }
            }
        }
    }
}
