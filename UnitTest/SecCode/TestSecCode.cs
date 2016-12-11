using QuantumRuntime;
using QuantumToolkit.Parser;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace UnitTest
{
    class TestSecCode
    {
        static public void TestMethod()
        {
            var exeDir = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            var inputFile = Path.Combine(exeDir, @"..\..\SecCode\TestQuantumConv.cs");
            var generator = new Generator(File.ReadAllText(inputFile));
            generator.Parse("TestQuantumConv0");
            QAsm.Generate(generator.OperatorGenerator.OperatorTree, true);
            QAsm.WriteQAsmText();
            QAsm.WriteDgmlFull();
            QAsm.WriteDgmlSimple();

            var test = new TestQuantumConv0();
            test.DisplayRegisterSet = true;
            for (int i = 1; i < 10; i++)
            {
                test.Run();

                test.q1 = test.qOutput.Value;
            }
        }
    }
}
