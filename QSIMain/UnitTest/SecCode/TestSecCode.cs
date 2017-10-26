using QuantumRuntime;
using QuantumToolkit;
using QuantumToolkit.Parser;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace UnitTest
{
    class TestSecCode
    {
        static public void TestMethod(int funcNum)
        {
            switch (funcNum)
            {
                case 1://QLOOP(TestQuantumConv0) without SK decomposition without termination
                    {
                        var exeDir = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
                        var inputFile = Path.Combine(exeDir, @"..\..\SecCode\TestQuantumConv.cs");
                        var generator = new Generator(File.ReadAllText(inputFile));
                        generator.Parse("TestQuantumConv0");
                        QAsm.Generate("TestQuantumConv0", 0, 1, QuantumMath.PreSKMethod.OrginalQR, generator.OperatorGenerator.OperatorTree);

                        QAsm.WriteQAsmText(false);

                        generator.MatRepANDAnalysis(false);
                        //Console.WriteLine(generator.OperatorGenerator);

                        var test = QEnv.CreateQEnv<TestQuantumConv0>();
                        test.DisplayRegisterSet = false;

                        var countNumber = new SortedDictionary<int, int>();
                        for (int i = 1; i < 101; i++)
                        {
                            test.Run();
                            test.q1 = test.qOutput.Value;

                            if (countNumber.ContainsKey(test.CounterWhile))
                            {
                                countNumber[test.CounterWhile]++;
                            }
                            else
                            {
                                countNumber[test.CounterWhile] = 1;
                            }
                            test.CounterWhile = 0;

                        }
                        Console.WriteLine("The experiment program runs about 100 times:");
                        foreach (var pair in countNumber)
                        {
                            Console.WriteLine($"Loops runs for {pair.Key} cycles is {pair.Value} times.");
                        }
                        break;
                    }
                case 11://QLOOP(TestQuantumConv1) without SK decomposition with termination
                    {
                        var exeDir = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
                        var inputFile = Path.Combine(exeDir, @"..\..\SecCode\TestQuantumConv.cs");
                        var generator = new Generator(File.ReadAllText(inputFile));
                        generator.Parse("TestQuantumConv1");
                        QAsm.Generate("TestQuantumConv1", 0, 1, QuantumMath.PreSKMethod.OrginalQSD, generator.OperatorGenerator.OperatorTree);
                        QAsm.WriteQAsmText(false);
                        generator.MatRepANDAnalysis(false);
                        //Console.WriteLine(generator.OperatorGenerator);


                        var test = QEnv.CreateQEnv<TestQuantumConv1>();
                        test.DisplayRegisterSet = false;
                        var countNumber = new SortedDictionary<int, int>();
                        for (int i = 1; i < 101; i++)
                        {
                            test.Run();
                            test.q1 = test.qOutput.Value;

                            if (countNumber.ContainsKey(test.CounterWhile))
                            {
                                countNumber[test.CounterWhile]++;
                            }
                            else
                            {
                                countNumber[test.CounterWhile] = 1;
                            }
                            test.CounterWhile = 0;

                        }

                        Console.WriteLine("The experiment program runs about 100 times:");
                        foreach (var pair in countNumber)
                        {
                            Console.WriteLine($"Loops runs for {pair.Key} cycles is {pair.Value} times.");
                        }
                        break;
                    }
                case 12://QLOOP(TestQuantumConv0) with SK decomposition with termination
                    {
                        var exeDir = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
                        var inputFile = Path.Combine(exeDir, @"..\..\SecCode\TestQuantumConv.cs");
                        var generator = new Generator(File.ReadAllText(inputFile));
                        generator.Parse("TestQuantumConv0");
                        QAsm.Generate("TestQuantumConv0", 0, 1, QuantumMath.PreSKMethod.OrginalQSD, generator.OperatorGenerator.OperatorTree);
                        QAsm.WriteQAsmText(true);
                        //QAsm.WriteDgmlFull();
                        //QAsm.WriteDgmlSimple();
                        generator.MatRepANDAnalysis(false);
                        //Console.WriteLine(generator.OperatorGenerator);

                        var test = QEnv.CreateQEnv<TestQuantumConv0>();
                        test.DisplayRegisterSet = true;
                        for (int i = 1; i < 10; i++)
                        {
                            test.Run();
                            test.q1 = test.qOutput.Value;
                        }
                        break;
                    }
                case 13://QLOOP(TestQuantumConv6) with SK decomposition with termination
                    {
                        var exeDir = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
                        var inputFile = Path.Combine(exeDir, @"..\..\SecCode\TestQuantumConv.cs");
                        var generator = new Generator(File.ReadAllText(inputFile));
                        generator.Parse("TestQuantumConv6");
                        QAsm.Generate("TestQuantumConv6", 0, 1, QuantumMath.PreSKMethod.OrginalQSD, generator.OperatorGenerator.OperatorTree);
                        QAsm.WriteQAsmText(true);
                        //QAsm.WriteDgmlFull();
                        //QAsm.WriteDgmlSimple();
                        generator.MatRepANDAnalysis(false);
                        //Console.WriteLine(generator.OperatorGenerator);

                        var test = QEnv.CreateQEnv<TestQuantumConv6>();
                        test.DisplayRegisterSet = true;
                        for (int i = 1; i < 10; i++)
                        {
                            test.Run();
                            test.q1 = test.qOutput.Value;
                        }
                        break;
                    }
                case 2:
                    {
                        //QLOOP(TestQuantumConv5) with SK decomposition without termination
                        var exeDir = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
                        var inputFile = Path.Combine(exeDir, @"..\..\SecCode\TestQuantumConv.cs");
                        var generator = new Generator(File.ReadAllText(inputFile));
                        generator.Parse("TestQuantumConv5");
                        QAsm.Generate("TestQuantumConv5", 0, 1, QuantumMath.PreSKMethod.OrginalQSD, generator.OperatorGenerator.OperatorTree);
                        QAsm.WriteQAsmText(true);
                        //QAsm.WriteDgmlFull();
                        //QAsm.WriteDgmlSimple();
                        //generator.MatrixRepresentation();
                        ////Console.WriteLine(generator.OperatorGenerator);

                        var test = QEnv.CreateQEnv<TestQuantumConv5>();
                        test.DisplayRegisterSet = true;
                        for (int i = 1; i < 10; i++)
                        {
                            test.Run();

                            test.q1 = test.qOutput.Value;
                        }
                        break;
                    }
                case 21:
                    {
                        //QLOOP(TestQuantumConv5) with SK decomposition with termination
                        var exeDir = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
                        var inputFile = Path.Combine(exeDir, @"..\..\SecCode\TestQuantumConv.cs");
                        var generator = new Generator(File.ReadAllText(inputFile));
                        generator.Parse("TestQuantumConv5");
                        QAsm.Generate("TestQuantumConv5", 0, 1, QuantumMath.PreSKMethod.OrginalQSD, generator.OperatorGenerator.OperatorTree);
                        QAsm.WriteQAsmText(true);
                        //QAsm.WriteDgmlFull();
                        //QAsm.WriteDgmlSimple();
                        generator.MatRepANDAnalysis(false);
                        //Console.WriteLine(generator.OperatorGenerator);

                        var test = QEnv.CreateQEnv<TestQuantumConv5>();
                        test.DisplayRegisterSet = true;
                        for (int i = 1; i < 10; i++)
                        {
                            test.Run();
                            test.q1 = test.qOutput.Value;
                        }
                        break;
                    }
                case 3:
                    {
                        //QLOOP(TestQuantumConv1) with SK decomposition with termination
                        var exeDir = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
                        var inputFile = Path.Combine(exeDir, @"..\..\SecCode\TestQuantumConv.cs");
                        var generator = new Generator(File.ReadAllText(inputFile));
                        generator.Parse("TestQuantumConv1");
                        QAsm.Generate("TestQuantumConv1", 0, 1, QuantumMath.PreSKMethod.OrginalQSD, generator.OperatorGenerator.OperatorTree);
                        QAsm.WriteQAsmText(true);
                        //QAsm.WriteDgmlFull();
                        //QAsm.WriteDgmlSimple();
                        generator.MatRepANDAnalysis(false);
                        //Console.WriteLine(generator.OperatorGenerator);

                        var test = QEnv.CreateQEnv<TestQuantumConv1>();
                        test.DisplayRegisterSet = true;
                        for (int i = 1; i < 10; i++)
                        {
                            test.Run();
                            test.q1 = test.qOutput.Value;
                        }
                        break;
                    }
                case 6:
                    {

                        var exeDir = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
                        var inputFile = Path.Combine(exeDir, @"..\..\SecCode\TestQuantMulti.cs");
                        var generator = new Generator(File.ReadAllText(inputFile));
                        Console.WriteLine("\nThe experiment will generate |0> and |+> states and perform a CNOT gate on these states. Bell state should be generated. Now begins.....");
                        generator.Parse("TestQuantMulti0");

                        //generator.MatrixRepresentation(false);
                        //Console.WriteLine(generator.OperatorGenerator);

                        var test = QEnv.CreateQEnv<TestQuantMulti0>();
                        test.DisplayRegisterSet = false;
                        int NumberOfZeroOfr1 = 0;
                        int NumberOfOneOfr1 = 0;
                        int NumberOfZeroOfr2 = 0;
                        int NumberOfOneOfr2 = 0;

                        for (int i = 0; i < 1000; i++)
                        {
                            test.Run();

                            if (test.r1.Value == 0)
                                NumberOfZeroOfr1++;
                            else
                                NumberOfOneOfr1++;

                            if (test.r2.Value == 0)
                                NumberOfZeroOfr2++;
                            else
                                NumberOfOneOfr2++;

                        }
                        Console.WriteLine("Reg 1 : 0's: {0},1's: {1}", NumberOfZeroOfr1, NumberOfOneOfr1);
                        Console.WriteLine("Reg 2 : 0's: {0},1's: {1}", NumberOfZeroOfr2, NumberOfOneOfr2);

                        break;
                    }
                case 7:
                    {
                        var exeDir = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
                        var inputFile = Path.Combine(exeDir, @"..\..\SecCode\TestQuantMulti.cs");
                        var generator = new Generator(File.ReadAllText(inputFile));

                        Console.WriteLine("Quantum Teleportation begins...The experiment would repeat 1000 times.");

                        generator.Parse("TestQuantMulti1");
                        QAsm.Generate("TestQuantMulti1", 0, 1, QuantumMath.PreSKMethod.OrginalQR, generator.OperatorGenerator.OperatorTree);
                        QAsm.WriteQAsmText(true);

                        var test = QEnv.CreateQEnv<TestQuantMulti1>();
                        test.DisplayRegisterSet = false;
                        //int NumberOfZero = 0;
                        int NumberOfOne = 0;


                        test.Init();

                        Console.WriteLine($"The state before teleportation is\n {test.Alice.DensityOperator.Value.ToComplexString()}\n");
                        Console.WriteLine("After that, we repeat the protocol about 1000 times to teleport the states.\n ");
                        Console.WriteLine("Finally, in every experiment, we perform a computational basis measurement and denote the time we get 'ONE'\n");

                        for (int i = 0; i < 1000; i++)
                        {

                            test.Run();

                            if (test.r3.Value == 1)
                                NumberOfOne++;
                            test.InitSuperRegister();

                        }
                        test.InitSuperRegister();
                        Console.WriteLine("Count number of ONE is {0}\n", NumberOfOne);
                        break;
                    }
                case 8:
                    {

                        var exeDir = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
                        var inputFile = Path.Combine(exeDir, @"..\..\SecCode\TestQuantMulti.cs");
                        var generator = new Generator(File.ReadAllText(inputFile));
                        generator.Parse("TestQuantMulti3");
                        generator.MatRepANDAnalysis(false);
                        //Console.WriteLine(generator.OperatorGenerator);

                        QAsm.Generate("TestQuantMulti3", 0, 1, QuantumMath.PreSKMethod.OrginalQSD, generator.OperatorGenerator.OperatorTree);
                        QAsm.WriteQAsmText(true);

                        var test = QEnv.CreateQEnv<TestQuantMulti3>();
                        test.DisplayRegisterSet = false;
                        //int NumberOfZero = 0;
                        int NumberOfOne = 0;


                        test.Init();

                        Console.WriteLine($"The state before teleportation is\n {test.Alice.DensityOperator.Value.ToComplexString()}\n");
                        Console.WriteLine("After that, we repeat the protocol about 1000 times to teleport the states.\n ");
                        Console.WriteLine("Finally, in every experiment, we perform a computational basis measurement and denote the time we get 'ONE'\n");

                        for (int i = 0; i < 1000; i++)
                        {

                            test.Run();

                            if (test.r3.Value == 1)
                                NumberOfOne++;
                            test.InitSuperRegister();

                        }
                        test.InitSuperRegister();
                        Console.WriteLine("Count number of ONE is {0}\n", NumberOfOne);
                        break;
                    }
            }
        }
    }
}
