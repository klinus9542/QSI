using QuantumRuntime;
using QuantumToolkit;
using QuantumToolkit.Parser;
using System;
using System.Diagnostics;
using System.IO;

namespace QSI
{
    class Program
    {
        static void Main(string[] args)
        {
            var exeDir = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            var inputFile = Path.Combine(exeDir, @"..\..\QSI_Code\Test.cs");
            var generator = new Generator(File.ReadAllText(inputFile));
            generator.Parse("Test");
            generator.MatRepANDAnalysis(false);
            //Console.WriteLine(generator.OperatorGenerator);

            QAsm.Generate("Test", 0, Matlab.PreSKMethod.OrginalQSD, generator.OperatorGenerator.OperatorTree);
            QAsm.WriteQAsmText(true);

            var test = QEnv.CreateQEnv<QSI_Code.Test>();
            test.DisplayRegisterSet = false;
           


            test.Init();
            test.Run();


            //    test.InitSuperRegister();

        
            test.InitSuperRegister();
            Console.WriteLine("Count number of ONE is {0}\n", test.r1.Value);
            Console.ReadKey(true);
        }
    }
}
