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

            for (int j = 1; j <= Console.WindowWidth; j++)
            {
                Console.Write("=");
            }
            Console.Write("\n");


            Console.SetCursorPosition((Console.WindowWidth / 2) - 30, 2);
            Console.WriteLine("Welcome to UTS:QSI Quantum Programming Environment!");

            Console.SetCursorPosition((Console.WindowWidth / 2) - 30, 3);
            Console.WriteLine("Version: Build 04.07.17 ( .net Framework: 4.6.2 )");

            Console.SetCursorPosition((Console.WindowWidth / 2) - 30, 4);
            Console.WriteLine("Stage: Alpha\n");


            for (int j = 1; j <= Console.WindowWidth; j++)
            {
                Console.Write("=");
            }
            Console.Write("\n");



            while (true)
            {
                //Console.WriteLine("\n1: TestQuantumConv0(XGate) without SK decomposition without termination.\n" +
                //"11:TestQuantumConv0(XGate) without SK decomposition with termination.\n" +
                //"12:TestQuantumConv0(XGate) with SK decomposition with termination.\n" +
                //"13:TestQuantumConv6(HGate) with SK decomposition with termination.\n" +
                //"2: TestQuantumConv5 with SK decomposition without termination.\n" +
                //"21:TestQuantumConv5 with SK decomposition with termination.\n" +
                //"3: TestQuantumConv1(HGate at last gate, test for running model) with SK decomposition with termination.\n" +
                //"4: The most simple BB84, without statistics without channel.\n" +
                //"41:The multi-clients simple BB84, without statistics. \n" +
                //"42:The BB84 without statistics with channel, Bit flip channel, p=0.5.\n" +
                //"43:The BB84 with statistics and channel\n" +
                //"5: Grover Search, the oracle has been set answer the position 3.\n" +
                //"51:Grover Search, without automatic parameters.\n" +
                //"52:Grover Search, Automatic parameters.(Should close the DEBUG FLAG)\n" +
                //"53:Grover Search, Search multi-objects, unfortunately the algorithm is wrong. It may blow tiny errors.\n" +
                //"Default(0) is quantum Google algorithm, contributed by JiGuan" +
                //"\n Please input a number, we will show the case");

                //For meeting with:
                Console.WriteLine("\n 1:  Termination analysis, Example 1 (xGate).\n" +
                    " 2:  Termination analysis, Example 2 (hGate).\n" +
                    " 3:  Quantum Google PageRank.\n" +
                    " 4:  Quantum Teleportation with QASM.\n"+
                    " 5:  Simple BB84.\n"+
                    " 51: The multi-clients protocol for simple BB84, without statistics.\n"+
                    " 52: The BB84 protocol with channel. Bit flip channel, p=0.5.\n"+
                    " 53: The BB84 with statistics and channel\n"+
                    " 6:  Grover Search, the oracle has been set answer the position 3.\n"+
                    " 61: Standard Grover Search.\n"+
                    " 62: Automatic toolkits Grover, search 2^4, answer from 0-15.(DEBUG close)\n"+
                    " 63: Search multi-objects Grover. It is WRONG.\n"+
                    " 7:  CNOT gate. Inputs are |+> and |0>. Run 1000 times and show entangled.\n"+
                    " 8:  A comprehensive Quantum Teleporation. Termination and Decomposition.\n"+
                    " 9:  For test termination general cases."+
                    " \nPress <Enter> to exit...\n"
                    );

                Console.WriteLine("Please select a case number :");

                var funcNumStr = Console.ReadLine();
                if (!int.TryParse(funcNumStr, out int funcNum))
                {
                    Console.WriteLine("Thank you for using UTS:QSI, Application exit!");
                    System.Threading.Thread.Sleep(2000);
                    Environment.Exit(0);
                }


                //switch (funcNum)
                //{
                //    case 1:
                //        //QLOOP(TestQuantumConv0) without SK decomposition without termination
                //        TestSecCode.TestMethod(1);
                //        break;
                //    case 11:
                //        //QLOOP(TestQuantumConv0) without SK decomposition with termination
                //        TestSecCode.TestMethod(11);
                //        break;
                //    case 12:
                //        //QLOOP(TestQuantumConv0) with SK decomposition with termination
                //        TestSecCode.TestMethod(12);
                //        break;
                //    case 13:
                //        //QLOOP(TestQuantumConv6) without SK decomposition with termination
                //        TestSecCode.TestMethod(13);
                //        break;
                //    case 2:
                //        //QLOOP(TestQuantumConv5) with SK decomposition without termination
                //        TestSecCode.TestMethod(2);
                //        break;
                //    case 21:
                //        //QLOOP(TestQuantumConv5) with SK decomposition with termination
                //        TestSecCode.TestMethod(21);
                //        break;
                //    case 3:
                //        //QLOOP(TestQuantumConv1) with SK decomposition with termination
                //        TestSecCode.TestMethod(3);
                //        break;
                //    case 4:
                //        //The most simple BB84, without statistics without channel
                //        Console.WriteLine("In basis,0 is {|0>,|1>} measurement and 1 is {|+>,|->} measurement\n");
                //        TestBB84.TestMethod();
                //        break;
                //    case 41:
                //        //The multi-clients simple BB84, without statistics 
                //        TestBB84.TestMethod2();
                //        break;
                //    case 42:
                //        //The BB84 without statistics with channel, Bit flip channel, p=0.5
                //        Console.WriteLine("In basis,0 is {|0>,|1>} measurement and 1 is {|+>,|->} measurement\n");
                //        TestBB84WithChannel.TestMethod();
                //        break;
                //    case 43:
                //        //The BB84 with statistics and channel
                //        TestBB84Main.TestMethod();
                //        break;
                //    //k is package length;from 32bit to 512bit
                //    //i is threshold for sucess communication, from 10% to 100%
                //    //every experiment, different package length and different thredshold, runs for 100 times and find the success time
                //    case 5:
                //        //Grover Search, the oracle has been set answer the position 3.
                //        TestGrover.TestMethod();
                //        break;
                //    case 51:
                //        TestGroverH.TestMethod(0, 0, false);
                //        break;
                //    case 52:
                //        //Search 2^4 space and test the answer from 0 to 15
                //        //Debug flag should be close
                //        TestGroverH.TestMethod2();
                //        break;
                //    case 53:
                //        //Search multi-objects, unfortunately the algorithm is wrong. It may blow tiny errors. 
                //        TestGroverHMuti.TestMethod(0, 0, false);
                //        break;
                //    case 6:
                //        TestSecCode.TestMethod(6);
                //        break;
                //    case 7:
                //        TestSecCode.TestMethod(7);
                //        break;
                //    default:
                //        TestPageRank.TestMethod();
                //        break;

                //}

                switch (funcNum)
                {
                    case 1:
                        TestSecCode.TestMethod(1);
                        break;
                    case 2:
                        TestSecCode.TestMethod(11);
                        break;
                    case 3://PageRank
                        TestPageRank2.TestMethod();
                        break;
                    case 4://Teleportation
                        TestSecCode.TestMethod(7);
                        break;
                    case 5:  //The most simple BB84, without statistics without channel
                        Console.WriteLine("In basis,0 is {|0>,|1>} measurement and 1 is {|+>,|->} measurement\n");
                        TestBB84.TestMethod();
                        break;
                    case 51:
                        //The multi-clients simple BB84, without statistics 
                        TestBB84.TestMethod2();
                        break;
                    case 52:
                         //The BB84 without statistics with channel, Bit flip channel, p=0.5
                        Console.WriteLine("In basis,0 is {|0>,|1>} measurement and 1 is {|+>,|->} measurement\n");
                        TestBB84WithChannel.TestMethod();
                        break;
                    case 53:
                        //The BB84 with statistics and channel
                        TestBB84Main.TestMethod();
                        break;                   
                    case 6:
                        //Grover Search, the oracle has been set answer the position 3.
                        TestGrover.TestMethod();
                        break;
                    case 61:
                        TestGroverH.TestMethod(0, 0, false);
                        break;
                    case 62://Automatic toolkits for test Grover
                        //Search 2^4 space and test the answer from 0 to 15
                        //Debug flag should be close
                        TestGroverH.TestMethod2();
                        break;
                    case 63:
                        //Search multi-objects, unfortunately the algorithm is wrong. It may blow tiny errors. 
                        TestGroverHMuti.TestMethod(0, 0, false);
                        break;
                    case 7://CNOT statistics
                        TestSecCode.TestMethod(6);
                        break;
                    case 8://Quantum Teleporation
                        TestSecCode.TestMethod(8);
                        break;
                    case 9://Quantum Teleporation
                        TestTerimination.TestMethod();
                        break;

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
