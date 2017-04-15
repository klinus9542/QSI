using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra.Complex;
using static QuantumToolkit.InherentAssembly;
using static QuantumToolkit.QuantumAlgorithm;
using QuantumToolkit.Type;
//using static QuantumToolkit.Define;

namespace UnitTest
{
    class TestGrover
    {
        static public void TestMethod()
        {
            Console.WriteLine("\nThe default oracle position is '3'.");
            Console.WriteLine("Grover Search algorithm begins:");

            Ket q1 = new Ket(2, 0);
            Ket q2 = new Ket(2, 0);
            Ket q = new Ket(2, 0);

            q.UnitaryTrans(XGate.Value);

            q1.UnitaryTrans(HGate.Value);
            q2.UnitaryTrans(HGate.Value);
            q.UnitaryTrans(HGate.Value);

            //Prepare tensor product
            Ket tempTensorProduct = new Ket((Matrix)q1.Value.KroneckerProduct(q2.Value));



            int r = 1;
            while (r <= 1)
            {
                oracle(ref tempTensorProduct, ref q);
                tensorH(ref tempTensorProduct);
                ph(ref tempTensorProduct);
                tensorH(ref tempTensorProduct);
                r++;
            }
            MeasureMatrixH mMH = new MeasureMatrixH(measureMatrixComput());

           
            Console.WriteLine("The result number is {0}.\n", tempTensorProduct.MeasuHResultIndex(mMH)); ;


        }

        static public void oracle(ref Ket oracleMatrixStorage, ref Ket q)
        {
   
            //|11> is the answer
            Ket qbit1 = new Ket(2, 1);
            Ket qbit2 = new Ket(2, 1);
            Ket oracleFlipBit = new Ket((Matrix)qbit1.Value.KroneckerProduct(qbit2.Value));
            oracleFlipBit.Value = (Matrix)(new Complex(-1, 0) * oracleFlipBit.Value);

            //对 oracleFlipBit.value 里面的每个值检测，如果非0，则把oracleMatrixStorage对应的位置的值，更换为oracleFlipBit 的值
            for (int j = 0; j < oracleFlipBit.Value.RowCount; j++)
            {
                if (Math.Abs(oracleFlipBit.Value[j, 0].Real) >= 0.0001)
                {
                    oracleMatrixStorage.Value[j, 0] = -1 * oracleMatrixStorage.Value[j, 0];//assignment
                }
            }
        }

        static public void tensorH(ref Ket tempTensorProduct)
        {
            tempTensorProduct.Value = (Matrix)((Matrix)HGate.Value.KroneckerProduct(HGate.Value) * tempTensorProduct.Value);
        }

        static public void ph(ref Ket tempTensorProduct)
        {
            Complex[,] phArray =
            {
                {1, 0,0,0},
                {0, -1,0,0},
                { 0,0,-1,0},
                { 0,0,0,-1}
            };
            Matrix phMatrix = (Matrix)Matrix.Build.DenseOfArray(phArray);
            tempTensorProduct.Value = (Matrix)(phMatrix * tempTensorProduct.Value);
        }

        static public Matrix[] measureMatrixComput()
        {
            Matrix[] matrixSet = new Matrix[4];
            Complex[,] c0 =
            {
                {1, 0, 0, 0},
                {0, 0, 0, 0},
                {0, 0, 0, 0},
                {0, 0, 0, 0}
            };
            matrixSet[0] = (Matrix)Matrix.Build.DenseOfArray(c0);

            Complex[,] c1 =
            {
                {0, 0, 0, 0},
                {0, 1, 0, 0},
                {0, 0, 0, 0},
                {0, 0, 0, 0}
            };
            matrixSet[1] = (Matrix)Matrix.Build.DenseOfArray(c1);

            Complex[,] c2 =
            {
                {0, 0, 0, 0},
                {0, 0, 0, 0},
                {0, 0, 1, 0},
                {0, 0, 0, 0}
            };
            matrixSet[2] = (Matrix)Matrix.Build.DenseOfArray(c2);

            Complex[,] c3 =
            {
                {0, 0, 0, 0},
                {0, 0, 0, 0},
                {0, 0, 0, 0},
                {0, 0, 0, 1}
            };
            matrixSet[3] = (Matrix)Matrix.Build.DenseOfArray(c3);

            return matrixSet;
        }

    }
}
