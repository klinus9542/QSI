using MathNet.Numerics.LinearAlgebra.Complex;
using MathNet.Numerics.Random;
using QuantumToolkit;
using QuantumToolkit.Type;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using static QuantumToolkit.QuantumAlgorithm;
using static System.Math;

namespace UnitTest
{
    class TestBB84WithChannel
    {
        static public void TestMethod()
        {
            //Initilization，zeroOneMeasureis a MeasureMatrixH class. {1 0;0 0}, {0 0;0 1}
            //plusminusMeasure is MeasureMatrixH class，{1/2 1/2;1/2 1/2}, {1/2 -1/2;-1/2 1/2}
            var matrixArray0 = new Matrix[2];
            Complex[,] array0 =
            {
                {1, 0},
                {0, 0}
            };
            matrixArray0[0] = (Matrix)Matrix.Build.DenseOfArray(array0);
            Complex[,] array1 =
            {
                {0, 0},
                {0, 1}
            };
            matrixArray0[1] = (Matrix)Matrix.Build.DenseOfArray(array1);
            var zeroOneMeasure = new MeasureMatrixH(matrixArray0);
            var matrixArray1 = new Matrix[2];
            Complex[,] array2 =
            {
                {0.5, 0.5},
                {0.5, 0.5}
            };
            matrixArray1[0] = (Matrix)Matrix.Build.DenseOfArray(array2);
            Complex[,] array3 =
            {
                {0.5, -0.5},
                {-0.5, 0.5}
            };
            matrixArray1[1] = (Matrix)Matrix.Build.DenseOfArray(array3);
            var plusminusMeasure = new MeasureMatrixH(matrixArray1);


            //-------------Alice----------------------
            Console.Write("Input Array Length:");
            var arrayLengthStr = Console.ReadLine();
            int arrayLength;
            if (!int.TryParse(arrayLengthStr, out arrayLength))
            {
                Console.WriteLine("Must input integer, use default(10)");
                arrayLength = 10;
            }
            var RandomGen = new CryptoRandomSource();
           
            //Produce raw key array 0,1
            var rawKeyArray = new byte[arrayLength];


            //0 is {|0>,|1>}，1 is {|+>,|->} basis.
            var basisRawArray = new byte[arrayLength];

            //Every KetEncArray is decided by rawKeyArray and basisRawArray
            //if basisRawArray is 0，rawKeyArray is 0，then ketEncArray is ket(0)
            //if basisRawArray is 0，rawKeyArray is 1，then ketEncArray is ket(1)
            // if basisRawArray is 1，rawKeyArray is 0，then ketEncArray is ket(1/Sqrt(2),1/Sqrt(2))
            // if basisRawArray is 1，rawKeyArray is 1，then ketEncArray ket(1/Sqrt(2),-1/Sqrt(2))
            var ketEncArray = new Ket[arrayLength];
            var complexArray = new Complex[2];
            for (var i = 0; i < arrayLength; i++)
            {
                rawKeyArray[i] = (byte)RandomGen.Next(2);
                basisRawArray[i] = (byte)RandomGen.Next(2);
                if (basisRawArray[i] == 0 && rawKeyArray[i] == 0)
                {
                    complexArray[0] = new Complex(1, 0);
                    complexArray[1] = new Complex(0, 0);
                    ketEncArray[i] = new Ket(complexArray);
                }
                else if (basisRawArray[i] == 0 && rawKeyArray[i] == 1)
                {
                    complexArray[0] = new Complex(0, 0);
                    complexArray[1] = new Complex(1, 0);
                    ketEncArray[i] = new Ket(complexArray);
                }
                else if (basisRawArray[i] == 1 && rawKeyArray[i] == 0)
                {
                    complexArray[0] = new Complex(1 / Sqrt(2), 0);
                    complexArray[1] = new Complex(1 / Sqrt(2), 0);
                    ketEncArray[i] = new Ket(complexArray);
                }
                else if (basisRawArray[i] == 1 && rawKeyArray[i] == 1)
                {
                    complexArray[0] = new Complex(1 / Sqrt(2), 0);
                    complexArray[1] = new Complex(-1 / Sqrt(2), 0);
                    ketEncArray[i] = new Ket(complexArray);
                }
            }

            // -----------alice end---------------------

            ////--------------Quantum Channel begins, ignoring EVE ----------

            var densityopEncArray = new DensityOperator[arrayLength];
            for (var i = 0; i < arrayLength; i++)
            {
                densityopEncArray[i] = new DensityOperator(ketEncArray[i], new Bra(ketEncArray[i]));
            }
            QuantumChannelProcess(densityopEncArray);


            //---------------Quantum Channel End-------Eve end---------------------

            //---------------------Bob begin--------------------
            //Bob produces measureRawArray，randomly {0,1}------------
            var measureRawArray = new byte[arrayLength];


            // resultMeasureArray is bool array. The value is decided by measureRawArray and ketEncArray.
            // foreach item in ketEncArray
            //if measureRawArray[index]=0 then zeroOneMeasure would be transferred to ketEncArray ,ket[index].MeasuHResultIndex(zeroOneMeasure )
            //if measureRawArray[index]=1 then plusminusMeasure would be transferred to ketEncArray,ket[index].MeasuHResultIndex(plusminusMeasure )
            var resultMeasureArray = new byte[arrayLength];
            for (var i = 0; i < arrayLength; i++)
            {
                measureRawArray[i] = (byte)RandomGen.Next(2);
                if (measureRawArray[i] == 0)
                {
                    if (densityopEncArray[i].MeasuHResultIndex(zeroOneMeasure) != 0)
                    {
                        resultMeasureArray[i] = 1;
                    }
                }
                else
                {
                    if (densityopEncArray[i].MeasuHResultIndex(plusminusMeasure) != 0)
                    {
                        resultMeasureArray[i] = 1;
                    }
                }
            }


            // ---------------------Bob end--------------------




            // --------------ALice begin---------------
            //Produce answer array,correctBroadArray,bool array class.
            //foreach item in measureRawArray
            //if measureRawArray[index]== basisRawArray[index], correctBroadArray[index]==1. else correctBroadArray[index]==0
            var correctBroadArray = new byte[arrayLength];
            for (var i = 0; i < arrayLength; i++)
            {
                if (measureRawArray[i] == basisRawArray[i])
                {
                    correctBroadArray[i] = 1;
                }
            }


            // foreach item in  correctBroadArray
            // if correctBroadArray[index]==1 , push(rawKeyArray[index]),i.e.  correctBroadArray[index]==1 position  rawKeyArray[index]is taken out and storage into finalAliceKey.            
            var finalAliceKeyList = new List<byte>();
            for (var i = 0; i < arrayLength; i++)
            {
                if (correctBroadArray[i] != 0)
                {
                    finalAliceKeyList.Add(rawKeyArray[i]);
                }
            }
            var finalAliceKey = finalAliceKeyList.ToArray();

            //------------Alice end--------------------




            //--------------Bob begins-------------------------
            //Produce finalBobKey array is bool array
            // foreach item in  correctBroadArray
            // if correctBroadArray[index]==1 , push(resultMeasureArray[index]),i.e.  correctBroadArray[index]==1 position,  resultMeasureArray[index] is taken out and storage into finalBobKey.
            var finalBobKeyList = new List<byte>();
            for (var i = 0; i < arrayLength; i++)
            {
                if (correctBroadArray[i] != 0)
                {
                    finalBobKeyList.Add(resultMeasureArray[i]);
                }
            }
            var finalBobKey = finalBobKeyList.ToArray();

            //----------------Bob END----------------------




            //Check Begin
            //check finalAliceKey is equal to finalBobKey
            bool check = true;
            if (finalAliceKey.Length != finalBobKey.Length)
            {
                check = false;
            }
            for (var i = 0; i < finalAliceKey.Length; i++)
            {
                if (finalAliceKey[i] != finalBobKey[i])
                {
                    check = false;
                }
            }
            //if (check)
            //{
            //    Console.WriteLine("Success");
            //}
            //else
            //{
            //    Console.WriteLine("Failed");
            //}
            ////check end

            Console.WriteLine($"zeroOneMeasure:\n{zeroOneMeasure.Value[0].ToComplexString()}\n{zeroOneMeasure.Value[1].ToComplexString()}");
            Console.WriteLine($"plusMinusMeasure:\n{plusminusMeasure.Value[0].ToComplexString()}\n{plusminusMeasure.Value[1].ToComplexString()}");
            Console.Write("rawKeyArray\t");
            foreach (var b in rawKeyArray)
            {
                Console.Write(b);
            }
            Console.WriteLine();
            Console.Write("basisRawArray\t");
            foreach (var b in basisRawArray)
            {
                Console.Write(b);
            }
            Console.WriteLine();

          
            Console.WriteLine();

            Console.Write("measureRawArray\t\t");
            foreach (var b in measureRawArray)
            {
                Console.Write(b);
            }
            Console.WriteLine();
            Console.Write("resultMeasureArray\t");
            foreach (var b in resultMeasureArray)
            {
                Console.Write(b);
            }
            Console.WriteLine();
            Console.Write("correctBroadArray\t");
            foreach (var b in correctBroadArray)
            {
                Console.Write(b);
            }
            Console.WriteLine();
            Console.Write("finalAliceKey\t\t");
            foreach (var b in finalAliceKey)
            {
                Console.Write(b);
            }
            Console.WriteLine();
            Console.Write("finalBobKey\t\t");
            foreach (var b in finalBobKey)
            {
                Console.Write(b);
            }
            if (check)
            {
                Console.WriteLine("\nThe protocol: Success");
            }
            else
            {
                Console.WriteLine("\nThe protocol: Failed");
            }
            //check end
            Console.WriteLine();
        }

        static public void TestMethod2()
        {
            Console.Write("Input Array Length:");
            var arrayLengthStr = Console.ReadLine();
            int arrayLength;
            if (!int.TryParse(arrayLengthStr, out arrayLength))
            {
                Console.WriteLine("Must input integer, use default(10)");
                arrayLength = 10;
            }
            Console.Write("Input Client Count:");
            var clientCountStr = Console.ReadLine();
            int clientCount;
            if (!int.TryParse(clientCountStr, out clientCount))
            {
                Console.WriteLine("Must input integer, use default(10)");
                clientCount = 10;
            }

            var alice = new Alice(arrayLength);
            for (var i = 0; i < clientCount; i++)
            {
                new Bob(alice);
            }
            //Console.ReadKey(true);
           // System.Threading.Thread.Sleep(4000);
            alice.Release();
            System.Threading.Thread.Sleep(4000);
        }

        static void QuantumChannelProcess(DensityOperator[] densityopEncArray)
        {

            // E0,E1 for Kraus Opearator,Bit Flip channel
            // E0= (|0><0| + |1><1|)/sqrt(2),E1=  (|0><1|+|1><0|)/sqrt(2)
            var matrixArrayE = new Matrix[2];
            Complex[,] arrayTemp0 = { {Sqrt(0.9),0},
                                  {0,Sqrt(0.9)} };
            matrixArrayE[0] = (Matrix)Matrix.Build.DenseOfArray(arrayTemp0);
            Complex[,] arrayTemp1 = { {0,Sqrt(0.1)},
                                  {Sqrt(0.1),0} };
            matrixArrayE[1] = (Matrix)Matrix.Build.DenseOfArray(arrayTemp1);
            var superE = new SuperOperator(matrixArrayE);
            //-----------------------------Bit Flip channel Ends-----------------

            // E0,E1 for Kraus Opearator,I channel
            // E0= (|0><0| + |1><1|)/sqrt(2),E1=  (|0><0|+|1><1|)/sqrt(2)
            var matrixArrayE2 = new Matrix[2];
            Complex[,] arrayTemp0a = { {1/Sqrt(2),0},
                                  {0,1/Sqrt(2)} };
            matrixArrayE2[0] = (Matrix)Matrix.Build.DenseOfArray(arrayTemp0a);
            Complex[,] arrayTemp1a = { {1/Sqrt(2),0},
                                  {0,1/Sqrt(2)} };
            matrixArrayE2[1] = (Matrix)Matrix.Build.DenseOfArray(arrayTemp1a);
            var superE2 = new SuperOperator(matrixArrayE2);
            //-----------------------------I channel Ends-----------------





            foreach (var densityopMember in densityopEncArray)
            {
                SuperMatrixTrans(densityopMember, superE);   //This needs a densityoperator, we only provide ket.In channel, only accept densityoperator
                // SuperMatrixTrans(densityopMember, superE2);
            }
        }

        class Package
        {
            public enum PackageType
            {
                RequestKetEncArray,
                ResponseKetEncArray,
                RequestCorrectBroadArray,
                ResponseCorrectBroadArray
            }
            public PackageType Type;

            public Ket[] KetEncArray;
            public byte[] MeasureRawArray, CorrectBroadArray;
            public int ArrayLength;
        }

        interface ChannelToServer
        {
            Package ServerReceive();

            void ServerSend(Package package);
        }

        interface ChannelToClient
        {
            Package ClientReceive();

            void ClientSend(Package package);
        }

        class PackageFilter : ChannelToServer, ChannelToClient
        {
            public ConcurrentQueue<Package> Up = new ConcurrentQueue<Package>(),
                Down = new ConcurrentQueue<Package>();

            public Package ServerReceive()
            {
                Package package;
                Up.TryDequeue(out package);
                return package;
            }

            public void ServerSend(Package package)
            {
                Down.Enqueue(package);
            }

            public Package ClientReceive()
            {
                Package package;
                Down.TryDequeue(out package);
                return package;
            }

            public void ClientSend(Package package)
            {
                Up.Enqueue(package);
            }
        }

        class Connection
        {
            public ChannelToServer Channel;
            public byte[] RawKeyArray, BasisRawArray;
        }

        class Alice
        {
            public Alice(int arrayLength)
            {
                this.arrayLength = arrayLength;
                thread = new Thread(Loop);
                thread.Start();
            }

            void Loop()
            {
                for (;;)
                {
                    foreach (var pair in clients)
                    {
                        var package = pair.Value.Channel.ServerReceive();
                        if (package == null)
                        {
                            continue;
                        }
                        switch (package.Type)
                        {
                            case Package.PackageType.RequestKetEncArray:
                                {
                                    package.Type = Package.PackageType.ResponseKetEncArray;
                                    package.ArrayLength = arrayLength;
                                    package.KetEncArray = new Ket[arrayLength];

                                    pair.Value.RawKeyArray = new byte[arrayLength];
                                    pair.Value.BasisRawArray = new byte[arrayLength];
                                    package.KetEncArray = new Ket[arrayLength];
                                    var complexArray = new Complex[2];
                                    var RandomGen = new SystemRandomSource(true);
                                    for (var i = 0; i < arrayLength; i++)
                                    {
                                        pair.Value.RawKeyArray[i] = (byte)RandomGen.Next(2);
                                        pair.Value.BasisRawArray[i] = (byte)RandomGen.Next(2);
                                        if (pair.Value.BasisRawArray[i] == 0 && pair.Value.RawKeyArray[i] == 0)
                                        {
                                            complexArray[0] = new Complex(1, 0);
                                            complexArray[1] = new Complex(0, 0);
                                            package.KetEncArray[i] = new Ket(complexArray);
                                        }
                                        else if (pair.Value.BasisRawArray[i] == 0 && pair.Value.RawKeyArray[i] == 1)
                                        {
                                            complexArray[0] = new Complex(0, 0);
                                            complexArray[1] = new Complex(1, 0);
                                            package.KetEncArray[i] = new Ket(complexArray);
                                        }
                                        else if (pair.Value.BasisRawArray[i] == 1 && pair.Value.RawKeyArray[i] == 0)
                                        {
                                            complexArray[0] = new Complex(1 / Sqrt(2), 0);
                                            complexArray[1] = new Complex(1 / Sqrt(2), 0);
                                            package.KetEncArray[i] = new Ket(complexArray);
                                        }
                                        else if (pair.Value.BasisRawArray[i] == 1 && pair.Value.RawKeyArray[i] == 1)
                                        {
                                            complexArray[0] = new Complex(1 / Sqrt(2), 0);
                                            complexArray[1] = new Complex(-1 / Sqrt(2), 0);
                                            package.KetEncArray[i] = new Ket(complexArray);
                                        }
                                    }
                                    pair.Value.Channel.ServerSend(package);
                                }
                                break;
                            case Package.PackageType.RequestCorrectBroadArray:
                                {
                                    package.Type = Package.PackageType.ResponseCorrectBroadArray;
                                    package.CorrectBroadArray = new byte[arrayLength];
                                    for (var i = 0; i < arrayLength; i++)
                                    {
                                        if (package.MeasureRawArray[i] == pair.Value.BasisRawArray[i])
                                        {
                                            package.CorrectBroadArray[i] = 1;
                                        }
                                    }
                                    pair.Value.Channel.ServerSend(package);

                                    var finalAliceKeyList = new List<byte>();
                                    for (var i = 0; i < arrayLength; i++)
                                    {
                                        if (package.CorrectBroadArray[i] != 0)
                                        {
                                            finalAliceKeyList.Add(pair.Value.RawKeyArray[i]);
                                        }
                                    }
                                    var finalAliceKey = finalAliceKeyList.ToArray();
                                    lock (this)
                                    {
                                        Console.Write($"FinalAliceKey for {pair.Key}\t");
                                        foreach (var b in finalAliceKey)
                                        {
                                            Console.Write(b);
                                        }
                                        Console.WriteLine();
                                    }
                                }
                                break;
                        }
                    }
                    if (exitFlag)
                    {
                        return;
                    }
                    Thread.Sleep(100);
                }
            }

            public void Release()
            {
                exitFlag = true;
            }

            public ChannelToClient Connect(int id)
            {
                var filter = new PackageFilter();
                clients[id] = new Connection { Channel = filter };
                return filter;
            }

            public void Disconnect(int id)
            {
                Connection channel;
                clients.TryRemove(id, out channel);
            }

            int arrayLength;
            ConcurrentDictionary<int, Connection> clients = new ConcurrentDictionary<int, Connection>();
            Thread thread;
            bool exitFlag;
        }

        class Bob
        {
            static Bob()
            {
                var matrixArray0 = new Matrix[2];
                Complex[,] array0 = { {1,0},
                                  {0,0} };
                matrixArray0[0] = (Matrix)Matrix.Build.DenseOfArray(array0);
                Complex[,] array1 = { {0,0},
                                  {0,1} };
                matrixArray0[1] = (Matrix)Matrix.Build.DenseOfArray(array1);
                zeroOneMeasure = new MeasureMatrixH(matrixArray0);
                var matrixArray1 = new Matrix[2];
                Complex[,] array2 = { {0.5,0.5},
                                  {0.5,0.5} };
                matrixArray1[0] = (Matrix)Matrix.Build.DenseOfArray(array2);
                Complex[,] array3 = { {0.5,-0.5},
                                  {-0.5,0.5} };
                matrixArray1[1] = (Matrix)Matrix.Build.DenseOfArray(array3);
                plusminusMeasure = new MeasureMatrixH(matrixArray1);
            }

            public Bob(Alice alice)
            {
                this.alice = alice;
                thread = new Thread(Loop);
                thread.Start();
            }

            void Loop()
            {
                channel = alice.Connect(Thread.CurrentThread.ManagedThreadId);
                var package = new Package();
                package.Type = Package.PackageType.RequestKetEncArray;
                channel.ClientSend(package);
                for (;;)
                {
                    package = channel.ClientReceive();
                    if (package == null)
                    {
                        continue;
                    }
                    switch (package.Type)
                    {
                        case Package.PackageType.ResponseKetEncArray:
                            {
                                package.Type = Package.PackageType.RequestCorrectBroadArray;
                                arrayLength = package.ArrayLength;
                                package.MeasureRawArray = new byte[arrayLength];
                                resultMeasureArray = new byte[arrayLength];
                                var RandomGen = new SystemRandomSource(true);
                                for (var i = 0; i < arrayLength; i++)
                                {
                                    package.MeasureRawArray[i] = (byte)RandomGen.Next(2);
                                    if (package.MeasureRawArray[i] == 0)
                                    {
                                        if (package.KetEncArray[i].MeasuHResultIndex(zeroOneMeasure) != 0)
                                        {
                                            resultMeasureArray[i] = 1;
                                        }
                                    }
                                    else
                                    {
                                        if (package.KetEncArray[i].MeasuHResultIndex(plusminusMeasure) != 0)
                                        {
                                            resultMeasureArray[i] = 1;
                                        }
                                    }
                                }
                                channel.ClientSend(package);
                            }
                            break;
                        case Package.PackageType.ResponseCorrectBroadArray:
                            {
                                var finalBobKeyList = new List<byte>();
                                for (var i = 0; i < arrayLength; i++)
                                {
                                    if (package.CorrectBroadArray[i] != 0)
                                    {
                                        finalBobKeyList.Add(resultMeasureArray[i]);
                                    }
                                }
                                var finalBobKey = finalBobKeyList.ToArray();
                                lock (alice)
                                {
                                    Console.Write($"FinalBobKey for {Thread.CurrentThread.ManagedThreadId}\t");
                                    foreach (var b in finalBobKey)
                                    {
                                        Console.Write($"{b}");
                                    }
                                    Console.WriteLine();
                                }
                            }
                            alice.Disconnect(Thread.CurrentThread.ManagedThreadId);
                            return;
                    }
                    Thread.Sleep(100);
                }
            }

            static MeasureMatrixH zeroOneMeasure, plusminusMeasure;
            Alice alice;
            ChannelToClient channel;
            int arrayLength;
            byte[] resultMeasureArray;
            Thread thread;
        }
    }
}
