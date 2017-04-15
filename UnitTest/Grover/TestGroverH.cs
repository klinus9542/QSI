#define DEBUG
//#undef DEBUG


using System;
using System.Numerics;
using System.Threading;
using MathNet.Numerics.LinearAlgebra.Complex;
using static QuantumToolkit.InherentAssembly;
using static QuantumToolkit.QuantumAlgorithm;
using QuantumToolkit.Type;
//using static QuantumToolkit.Define;

namespace UnitTest
{
    class TestGroverH
    {   //testMedthod2() is an automatic test function, ensuring the only one answer situation is correct when in this algorithm
        //f({0},{1},{2})
        //{0} is the search space, 2^n large
        //{1} is oracle answer
        //{2} is the automatic running switch, if open, it will run using {0} and {1}, else  print and accept inputs
        static public void TestMethod2()
        {
            for (int j = 0; j < 16; j++)
            {
                TestMethod(4, j, true);
            }


        }
        //User can call the Testmethod running for once directly,using ({0},{1},{2}).
        //However when {2} is false, {0} and {1} will be ignored.
        static public void TestMethod(int realSpaceLength, int realAnsIndex, bool autoOpen)
        {
            //Figure out the space 2^n
            int spaceLength = 2;
            int ansIndex = 1;

            if (autoOpen)
            {
                spaceLength = realSpaceLength;
                ansIndex = realAnsIndex;
            }
            else
            {

            }





#if DEBUG
            Console.WriteLine("How large the space(2^n) do you want to search?");
            var spaceLengthStr = Console.ReadLine();

            if (!int.TryParse(spaceLengthStr, out spaceLength))
            {
                Console.WriteLine("Must input integer, use default(4)");
                spaceLength = 4;
            }


#endif
            int binSpaceLength = Convert.ToInt32(Math.Pow(2, spaceLength));
#if DEBUG
            Console.WriteLine("You want to search {0} numbers", binSpaceLength);

            //Figure out the oracle answer
            //In this stage, only one correct answer
            Console.WriteLine("Intialling the oracle......,where is the correct answers(From 0 to n-1)?");
            var ansIndexStr = Console.ReadLine();

            if (!int.TryParse(ansIndexStr, out ansIndex))
            {
                Console.WriteLine("Must input integer to initial the oracle, use default (1)");
                ansIndex = 1;
            }
#endif
            if (ansIndex >= binSpaceLength || spaceLength > 10)
            {
                Console.WriteLine("You target is more than search space or space is more than 2^10");
                while (true)
                {
                    Thread.Sleep(Timeout.Infinite);
                }

            }

            Ket[] orginKetSets = new Ket[spaceLength];
            for (int j = 0; j < spaceLength; j++)
            {
                Matrix value = (Matrix)Matrix.Build.Dense(2, 1, Complex.Zero);
                value[0, 0] = Complex.One;
                orginKetSets[j] = new Ket(value);
            }

            for (int j = 0; j < spaceLength; j++)
            {
                orginKetSets[j].UnitaryTrans(HGate.Value);
            }


            //Prepare tensor product
            Matrix tempMatrix = (Matrix)Matrix.Build.Dense(1, 1, Complex.One);
            for (int j = 0; j < spaceLength; j++)
            {

                tempMatrix = (Matrix)tempMatrix.KroneckerProduct(orginKetSets[j].Value);
            }
            Ket tempTensorProduct = new Ket(tempMatrix);



            int limit = (int)(Math.PI / 4.0 * Math.Sqrt(binSpaceLength));
            int r = 1;


            while (r <= limit)
            {
                oracle(ref tempTensorProduct, ansIndex);
                tensorH(ref tempTensorProduct, spaceLength);
                ph(ref tempTensorProduct, binSpaceLength);
                tensorH(ref tempTensorProduct, spaceLength);
                r++;
            }
            MeasureMatrixH mMH = new MeasureMatrixH(measureMatrixComput(binSpaceLength));

#if DEBUG
            Console.WriteLine("The loop time is {0}.", r - 1);
        //   Console.WriteLine("TempTensorProduct is {0}", tempTensorProduct.Value);
#endif
            Console.WriteLine("Target is {0}, the final result number is {1}.\n", ansIndex, tempTensorProduct.MeasuHResultIndex(mMH)); ;


        }

        static public void oracle(ref Ket oracleMatrixStorage, int ansIndex)// ansIndex from 0 to n-1
        {
#if DEBUG
            Console.WriteLine("Call oracle up");
#endif
            oracleMatrixStorage.Value[ansIndex, 0] = -1 * oracleMatrixStorage.Value[ansIndex, 0];
        }

        static public void tensorH(ref Ket tempTensorProduct, int spaceLength)
        {
            //H only needs to tensor n-1 times
            Matrix HTemp = (Matrix)Matrix.Build.Dense(1, 1, 1);
            for (int j = 0; j < spaceLength; j++)
            {
                HTemp = (Matrix)HTemp.KroneckerProduct(HGate.Value);
                //tempTensorProduct.Value = (Matrix)((Matrix)HGate.Value.KroneckerProduct(HGate.Value) * tempTensorProduct.Value);
            }
            tempTensorProduct.Value = (Matrix)(HTemp * tempTensorProduct.Value);

        }

        static public void ph(ref Ket tempTensorProduct, int binSpaceLength)
        {
            Matrix phMatrix = (Matrix)Matrix.Build.DenseDiagonal(binSpaceLength, binSpaceLength, new Complex(-1, 0));
            phMatrix[0, 0] = Complex.One;
            //Complex[,] phArray =
            //{
            //    {1, 0,0,0},
            //    {0, -1,0,0},
            //    { 0,0,-1,0},
            //    { 0,0,0,-1}
            //};
            tempTensorProduct.Value = (Matrix)(phMatrix * tempTensorProduct.Value);
        }

        static public Matrix[] measureMatrixComput(int binSpaceLength)
        {
            Matrix[] matrixSet = new Matrix[binSpaceLength];

            for (int j = 0; j < matrixSet.Length; j++)
            {
                matrixSet[j] = (Matrix)Matrix.Build.Dense(binSpaceLength, binSpaceLength, 0);
                matrixSet[j][j, j] = Complex.One;                                                                 //m[0]=|0><0|,m[1]=|1><1|
            }

            return matrixSet;
        }

    }
}
