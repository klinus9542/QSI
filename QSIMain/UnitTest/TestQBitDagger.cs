using MathNet.Numerics.LinearAlgebra.Complex;
using QuantumToolkit;
using QuantumToolkit.Type;
using System;
using System.Numerics;

namespace UnitTest
{
    class TestQBitBra
    {
        static public void TestMethod()
        {
            var qBit1 = new QBit(new Complex(1, 1), new Complex(1, -1));
            var qBitBra1 = new QBitBra(qBit1);
            Console.WriteLine(qBit1.Value.ToComplexString());
            Console.WriteLine("QBitBra");
            Console.WriteLine(qBitBra1.Value.ToComplexString());
        }
    }
}
