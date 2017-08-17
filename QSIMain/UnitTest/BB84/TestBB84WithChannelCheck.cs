using MathNet.Numerics.LinearAlgebra.Complex;
using MathNet.Numerics.Random;
using QuantumToolkit;
using QuantumToolkit.Type;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading;
using static QuantumToolkit.QuantumAlgorithm;
using static System.Math;

namespace UnitTest
{


    class TestBB84WithChannelCheck
    {     


        static public void TestMethod()
        {
     


        //初始化部分，zeroOneMeasure是个MeasureMatrixH类型，里面有两个元素{1 0;0 0}, {0 0;0 1}
        //plusminusMeasure是个MeasureMatrixH类型，里面有两个元素{1/2 1/2;1/2 1/2}, {1/2 -1/2;-1/2 1/2}
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

            //-------------------------check length----------------
            Console.Write("Input Check length percentage:");
            var checkLengthPercentStr = Console.ReadLine();
            int checkLengthPercent;
            if (!int.TryParse(checkLengthPercentStr, out checkLengthPercent))
            {
                Console.WriteLine("Must input integer, use default(50)");
                checkLengthPercent = 50;
            }




            var RandomGen = new CryptoRandomSource();
            //生成一个rawKeyArray[10],是bool数组类型,每一个item,从0,1这两个中选择
            //Produce raw key array 0,1
            var rawKeyArray = new byte[arrayLength];

            //生成一个basisRawArray[10]，是bool数组类型,每一个item,从0,1这两个中选择
            //0表示0,1基，1表示{+,-}基
            var basisRawArray = new byte[arrayLength];

            //生成一个ketEncArray[10]是ket数组类型.每一位由rawKeyArray和basisRawArray共同决定。
            //if basisRawArray里面的值是0，rawKeyArray里面的值是0，则 ketEncArray里面的值是ket(0)
            //if basisRawArray里面的值是0，rawKeyArray里面的值是1，则 ketEncArray里面的值是ket(1)
            // if basisRawArray里面的值是1，rawKeyArray里面的值是0，则 ketEncArray里面的值是ket(1/Sqrt(2),1/Sqrt(2))
            // if basisRawArray里面的值是1，rawKeyArray里面的值是1，则 ketEncArray里面的值是ket(1/Sqrt(2),-1/Sqrt(2))
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
            //Bob 生成一个measureRawArray，是bool数组类型随机{0,1}------------
            var measureRawArray = new byte[arrayLength];


            //生成结果数组 resultMeasureArray,是bool数组类型，其中的值由  measureRawArray和 densityopEncArray共同决定
            // foreach item in densityopEncArray
            //if measureRawArray[index]=0则用 zeroOneMeasure 作为调用的参数传递到    densityopEncArray的本位,ket[index].MeasuHResultIndex(zeroOneMeasure )
            //if measureRawArray[index]=1则用 plusminusMeasure 作为调用的参数传递到 densityopEncArray的本位,ket[index].MeasuHResultIndex(plusminusMeasure )
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


            //--------------公共信道广播不做--------


            //--------------公共信道结束----------



            // --------------ALice begin---------------
            //生成应答数组,correctBroadArray,bool数组类型。
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


            // 生成finalAliceKey数组是bool数组类型，
            // foreach item in  correctBroadArray
            // if correctBroadArray[index]==1 , push(rawKeyArray[index]),即把  correctBroadArray[index]==1的那些位置的  rawKeyArray[index]取出来放一起finalAliceKey。
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


            //------------公共信道开始-----------------------
            //-----------公共信道结束------------------------

            //--------------Bob开始-------------------------
            //生成finalBobKey数组是bool数组类型
            // foreach item in  correctBroadArray
            // if correctBroadArray[index]==1 , push(resultMeasureArray[index]),即把  correctBroadArray[index]==1的那些位置的  resultMeasureArray[index]取出来放一起存到finalBobKey。
            var finalBobKeyList = new List<byte>();
            for (var i = 0; i < arrayLength; i++)
            {
                if (correctBroadArray[i] != 0)
                {
                    finalBobKeyList.Add(resultMeasureArray[i]);
                }
            }
            var finalBobKey = finalBobKeyList.ToArray();

            //----------------Bob结束----------------------




            //Check start
            //Check the alice and bob's length is equal
            var check = true;
            if (finalAliceKey.Length != finalBobKey.Length)
            {
                check = false;
            }
            if (check)
            {
                Console.WriteLine("Length: Success");
            }
            else
            {
                Console.WriteLine("Length: Failed");
            }

            //Sampling start
            var samplingLength = (checkLengthPercent * finalAliceKey.Length) / 100;
            Console.WriteLine($"Sampling {samplingLength} bits");
            var samplingAliceKey = new List<byte>(finalAliceKey);
            var samplingBobKey = new List<byte>(finalBobKey);
            var samplingAliceKeySample = new List<byte>();
            var samplingBobKeySample = new List<byte>();
            for (var i = 0; i < samplingLength; i++)
            {
                var position = RandomGen.Next(samplingAliceKey.Count);  //shuffle here, sample from agreement key and then delete it
                samplingAliceKeySample.Add(samplingAliceKey[position]);
                samplingAliceKey.RemoveAt(position);
                samplingBobKeySample.Add(samplingBobKey[position]);
                samplingBobKey.RemoveAt(position);
            }
            for (var i = 0; i < samplingLength; i++)
            {
                if (samplingAliceKeySample[i] != samplingBobKeySample[i])
                {
                    check = false;
                }
            }

            if (check)
            {
                Console.WriteLine("Sampling process: Success");
            }
            else
            {
                Console.WriteLine("Sampling process: Failed");
            }


            Console.WriteLine("keySample");
            Console.Write("Alice:\t");
            foreach (var b in samplingAliceKeySample)
            {
                Console.Write(b);
            }
            Console.WriteLine();
            Console.Write("Bob:\t");
            foreach (var b in samplingBobKeySample)
            {
                Console.Write(b);
            }
            Console.WriteLine();
            Console.WriteLine("keyRemain");
            Console.Write("Alice:\t");
            foreach (var b in samplingAliceKey)
            {
                Console.Write(b);
            }
            Console.WriteLine();
            Console.Write("Bob:\t");
            foreach (var b in samplingBobKey)
            {
                Console.Write(b);
            }
            Console.WriteLine();

            //Sampling end

            Console.WriteLine($"zeroOneMeasure:\n{zeroOneMeasure.Value[0]}{zeroOneMeasure.Value[1].ToComplexString()}");
            Console.WriteLine($"plusminusMeasure:\n{plusminusMeasure.Value[0]}{plusminusMeasure.Value[1].ToComplexString()}");
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

            /*for (var i = 0; i < densityopEncArray.Length; i++)
            {
                Console.Write(densityopEncArray{i});
                Console.Write(densityopEncArray[i].Value.ToComplexString());
            } */
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
            Console.ReadKey(true);
            alice.Release();
        }

        static  void ByteSwap(byte a, byte b)
        {
            byte temp = a;
            a = b;
            b = temp;
        }


        static void QuantumChannelProcess(DensityOperator[] densityopEncArray)
        {

            // E0,E1 for Kraus Opearator,Bit Flip channel
            // E0= (|0><0| + |1><1|)/sqrt(2),E1=  (|0><1|+|1><0|)/sqrt(2)
            var matrixArrayE = new Matrix[2];
            Complex[,] arrayTemp0 = { {1/Sqrt(2),0},
                                  {0,1/Sqrt(2)} };
            matrixArrayE[0] = (Matrix)Matrix.Build.DenseOfArray(arrayTemp0);
            Complex[,] arrayTemp1 = { {0,1/Sqrt(2)},
                                  {1/Sqrt(2),0} };
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
                                        Console.Write(b);
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
