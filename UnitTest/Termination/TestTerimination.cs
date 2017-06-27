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
            generator.MatRepANDAnalysis(false);
            //Console.WriteLine(generator.OperatorGenerator);

            //var exeDir = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            //var inputFile = Path.Combine(exeDir, @"..\..\SecCode\TestQuantumConv.cs");
            //var generator = new Generator(File.ReadAllText(inputFile));
            //generator.Parse("TestQuantumConv6");
            //generator.MatrixRepresentation(false);
            //Console.WriteLine(generator.OperatorGenerator);
        }
    }
}
