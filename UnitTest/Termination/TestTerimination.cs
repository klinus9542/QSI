using QuantumToolkit.Parser;
using System.Diagnostics;
using System.IO;
using System;

namespace UnitTest
{
    class TestTerimination
    {
        static public void TestMethod()
        {
            var exeDir = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            var inputFile = Path.Combine(exeDir, @"..\..\Termination\TestQuantumMiddle.cs");
            var generator = new Generator(File.ReadAllText(inputFile));
            generator.Parse("TestQuantumMid11");
            generator.MatrixRepresentation();
            Console.WriteLine($"{generator.OperatorGenerator}");
        }
    }
}
