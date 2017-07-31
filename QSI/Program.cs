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

            //------------------Tell the program the entry to be analysed----------
            var exeDir = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);//exe parth
            var inputFile = Path.Combine(exeDir, @"..\..\QSI_Code\Test.cs");//The target file
            var generator = new Generator(File.ReadAllText(inputFile));//Generate the exAST

            //-----------------Termination Analysis-----------------
            generator.Parse("Test");//Construct
            generator.MatRepANDAnalysis(false);//Termination analysis
            //Console.WriteLine(generator.OperatorGenerator);// Process print

            //----------------Generate f-QASM-----------------------
            QAsm.Generate("Test", 0, Matlab.PreSKMethod.OrginalQSD, generator.OperatorGenerator.OperatorTree);
            QAsm.WriteQAsmText(true);//write a file

            //----------------Core for executing
            var test = QEnv.CreateQEnv<QSI_Code.Test>();
            test.DisplayRegisterSet = false; //Show the Quantum State?
           


            test.Init(); //Remember Init() before executing the program
            
            while (true)
            {
                test.Run();//Clear and Run 
                Console.WriteLine("The value of r1 is {0}", test.r1.Value);
                Console.WriteLine("The value of r2 is {0}", test.r2.Value);
                Console.WriteLine("The value of r3 is {0}", test.r3.Value);
                Console.WriteLine("The value of r4 is {0}", test.r4.Value);
                Console.WriteLine("\r\n");
               
                Console.ReadKey(true);
            }
        }
    }
}
