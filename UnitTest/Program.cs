using MathNet.Numerics;
using System;

namespace UnitTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Control.UseNativeMKL();//Control.UseManaged();
            //Console.WriteLine(Control.LinearAlgebraProvider);

            while (true)
            {
                Console.Clear();
                for (int j = 1; j <= Console.WindowWidth; j++)
                {
                    Console.Write("=");
                }
                Console.Write("\n");


                Console.SetCursorPosition((Console.WindowWidth / 2) - 30, 2);
                Console.WriteLine("Welcome to QSI: Quantum Programming Environment!");

                Console.SetCursorPosition((Console.WindowWidth / 2) - 30, 3);
                Console.WriteLine("Version: Build 07.30.17 ( .net Framework: 4.6.1 )");

                Console.SetCursorPosition((Console.WindowWidth / 2) - 30, 4);
                Console.WriteLine("Stage: Alpha\n");


                for (int j = 1; j <= Console.WindowWidth; j++)
                {
                    Console.Write("=");
                }
                Console.Write("\n");

         
                //For meeting with:
                Console.WriteLine(
                    "\n \t1:  CNOT gate. Inputs are |+> and |0>. Run 1000 times. \n" +
                    " \t2:  Termination analysis, Example 1 (xGate).\n" +
                    " \t3:  Termination analysis, Example 2 (hGate).\n" +
                    " \t4:  Simple BB84.\n" +
                    " \t41: The multi-clients protocol for simple BB84, without statistics.\n" +
                    " \t42: The BB84 protocol with channel. Bit flip channel, p=0.1.\n" +
                    " \t43: The BB84 with statistics and channel.\n" +
                    " \t5:  Quantum Teleportation with QASM.\n" +
                    " \t6:  Quantum Google PageRank.\n" +
                    " \t7:  Grover Search, the oracle has been set answer the position 3.\n" +
                    " \t71: Standard Grover Search.\n" +
                    " \t72: Automatic toolkits Grover, search 2^4, answer from 0-15.(DEBUG close)\n" +
                    " \t73: Search multi-objects Grover. It is WRONG.\n" +
                    " \t8:  A comprehensive Quantum Teleporation. Termination and Decomposition.\n" +
                    //" 9:  For test termination general cases." +
                    " \n\tPress <Enter> to exit...\n"
                    );

                Console.Write("\tPlease select a case number:\t");

                var funcNumStr = Console.ReadLine();
                if (!int.TryParse(funcNumStr, out int funcNum))
                {
                    Console.WriteLine("Thank you for using UTS:QSI, Application exit!");
                    System.Threading.Thread.Sleep(2000);
                    Environment.Exit(0);
                }


                switch (funcNum)
                {
                    case 1://CNOT statistic
                        TestSecCode.TestMethod(6);
                        break;
                    case 2://termination
                        TestSecCode.TestMethod(1);
                        break;
                    case 3://termination
                        TestSecCode.TestMethod(11);
                        break;
                    case 4:  //The most simple BB84, without statistics without channel
                        Console.WriteLine("In basis,0 is {|0>,|1>} measurement and 1 is {|+>,|->} measurement\n");
                        TestBB84.TestMethod();
                        break;
                    case 41:
                        //The multi-clients simple BB84, without statistics 
                        TestBB84.TestMethod2();
                        break;
                    case 42:
                        //The BB84 without statistics with channel, Bit flip channel, p=0.5
                        Console.WriteLine("In basis,0 is {|0>,|1>} measurement and 1 is {|+>,|->} measurement\n");
                        TestBB84WithChannel.TestMethod();
                        break;
                    case 43:
                        //The BB84 with statistics and channel
                        TestBB84Main.TestMethod();
                        break;
                    case 5://Teleportation
                        TestSecCode.TestMethod(7);
                        break;
                    case 6://PageRank
                        TestPageRank2.TestMethod();
                        break;
                    case 7:
                        //Grover Search, the oracle has been set answer the position 3.
                        TestGrover.TestMethod();
                        break;
                    case 71:
                        TestGroverH.TestMethod(0, 0, false);
                        break;
                    case 72://Automatic toolkits for test Grover
                        //Search 2^4 space and test the answer from 0 to 15
                        //Debug flag should be close
                        TestGroverH.TestMethod2();
                        break;
                    case 73:
                        //Search multi-objects, unfortunately the algorithm is wrong. It may blow tiny errors. 
                        TestGroverHMuti.TestMethod(0, 0, false);
                        break;
                    case 8://Quantum Teleporation
                        TestSecCode.TestMethod(8);
                        break;
                   // case 9://Quantum Teleporation
                    //    TestTerimination.TestMethod();
                   //     break;

                    default:
                        Console.WriteLine(" Input number is not correct or missing.\n" +
                        " Please try again.\n\n");
                        //TestSecCode.TestMethod(8);
                        break;



                }

                for (int j = 1; j <= Console.WindowWidth; j++)
                {
                    Console.Write("+");
                }
                Console.Write("\n");

                Console.ReadKey(true);
            }
        }
    }
}
